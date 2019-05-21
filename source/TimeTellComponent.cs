using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace TimeTell
{
    class TimeTellComponent : IComponent
    {
        protected InfoTextComponent InternalComponent { get; set; }
        protected TimeTellSettings Settings { get; set; }
        protected LiveSplitState State;
        protected Random rand;
        protected string category;

        string IComponent.ComponentName => "TimeTell";

        IDictionary<string, Action> IComponent.ContextMenuControls => null;
        float IComponent.HorizontalWidth => InternalComponent.HorizontalWidth;
        float IComponent.MinimumHeight => InternalComponent.MinimumHeight;
        float IComponent.MinimumWidth => InternalComponent.MinimumWidth;
        float IComponent.PaddingBottom => InternalComponent.PaddingBottom;
        float IComponent.PaddingLeft => InternalComponent.PaddingLeft;
        float IComponent.PaddingRight => InternalComponent.PaddingRight;
        float IComponent.PaddingTop => InternalComponent.PaddingTop;
        float IComponent.VerticalHeight => InternalComponent.VerticalHeight;
        List<List<TimeSpan>> splits_ = new List<List<TimeSpan>>();
        List<TimeSpan>[] futureRunHistogram_;
        //List<TimeSpan>[] possiblePaceHistogram_;
        TimeSpan[] bestPace_;
        string[] results_ = new string[1];
        string splitGameName_ = "";
        string splitCatName_ = "";
        System.Data.DataTable dataTable1 = new System.Data.DataTable();

        XmlNode IComponent.GetSettings(XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        Control IComponent.GetSettingsControl(LayoutMode mode)
        {
            Settings.Mode = mode;
            return Settings;
        }

        void IComponent.SetSettings(XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public TimeTellComponent(LiveSplitState state)
        {
            State = state;
            InternalComponent = new InfoTextComponent("TimeTell", "-");
//             InternalComponent = new InfoTextComponent("Continue at pace", "0.0%")
//             {
//                 AlternateNameText = new string[]
//                 {
//                     "Decent pace",
//                     "Go"
//                 }
//             };
            Settings = new TimeTellSettings();
            Settings.SettingChanged += OnSettingChanged;
            Settings.SettingCompute += OnSettingCompute;
            rand = new Random();
            category = State.Run.GameName + State.Run.CategoryName;

            state.OnSplit += OnSplit;
            state.OnReset += OnReset;
            state.OnSkipSplit += OnSkipSplit;
            state.OnUndoSplit += OnUndoSplit;
            state.OnStart += OnStart;
            state.RunManuallyModified += OnRunManuallyModified;

            //dataTable1.Columns.Add("Name", typeof(string), null);
            //dataTable1.Columns.Add("Goal", typeof(string), null);
            Settings.dataGridView1.DataSource = dataTable1;

            //computeResults();
            updateText();
        }

        private void OnSettingCompute(object sender, EventArgs e)
        {
            splitGameName_ = "recompute....";
            computeResults();
            updateText();
        }
        private void OnRunManuallyModified(object sender, EventArgs e)
        {
            updateText();
        }
        private void OnSettingChanged(object sender, EventArgs e)
        {
            updateText();
        }
        private void OnStart(object sender, EventArgs e)
        {
            updateText();
        }
        protected void OnUndoSplit(object sender, EventArgs e)
        {
            updateText();
        }
        protected void OnSkipSplit(object sender, EventArgs e)
        {
            updateText();
        }
        protected void OnReset(object sender, TimerPhase value)
        {
            updateText();
        }
        protected void OnSplit(object sender, EventArgs e)
        {
            updateText();
        }

        List<TimeSpan> computeDistributionsSum(in List<TimeSpan> list_a, in List<TimeSpan> list_b, int numBuckets)
        {
            bool useOptimisticMode = Settings.AltComputationMode;
            List<TimeSpan> list_c = new List<TimeSpan>();
            int jumpGap1 = (int)(Math.Ceiling((double)list_a.Count / numBuckets) + .1);
            for (int a = 0; a < list_a.Count; a += jumpGap1)
            {
                TimeSpan sampledTimeA = list_a[a];
                if (!useOptimisticMode && jumpGap1 > 1)
                {
                    int numMerged = 1;
                    for (int i = a+1; i < a + jumpGap1; i++)
                    {
                        if (i >= list_a.Count)
                            break;
                        numMerged++;
                        sampledTimeA += list_a[i];
                    }
                    if (numMerged > 1)
                        sampledTimeA = new TimeSpan(sampledTimeA.Ticks / numMerged);
                }
                int jumpGap2 = (int)(Math.Ceiling((double)list_b.Count / numBuckets) + .1);
                for (int b = 0; b < list_b.Count; b += jumpGap2)
                {
                    TimeSpan sampledTimeB = list_b[b];
                    if (!useOptimisticMode && jumpGap2 > 1)
                    {
                        int numMerged = 1;
                        for (int i = b+1; i < b + jumpGap2; i++)
                        {
                            if (i >= list_b.Count)
                                break;
                            numMerged++;
                            sampledTimeB += list_b[i];
                        }
                        if (numMerged > 1)
                            sampledTimeB = new TimeSpan(sampledTimeB.Ticks / numMerged);
                    }
                    list_c.Add(sampledTimeA + sampledTimeB);
                }
            }

            list_c.Sort();
            return list_c;
        }

        protected void computeResults()
        {
            //MessageBox.Show("computing "+ Settings.MaxPrecision);
            var numSegments = State.Run.Count;

            // Get the current Personal Best, if it exists
            Time pb = State.Run.Last().PersonalBestSplitTime;
            if (pb[State.CurrentTimingMethod] == TimeSpan.Zero)
            {
                // No personal best, so any run will PB
                dataTable1.Rows.Clear();
                results_ = new string[1];
                results_[0] = "No goal found";
                return;
            }

            // Find the range of attempts to gather times from
            //             int numAttempts = State.Run.AttemptHistory.Count;
            //             int numAttemptsToUse = Settings.AttemptCount;
            //             if (!Settings.UseFixedAttempts)
            //                 numAttemptsToUse = (int)(numAttempts * Settings.AttemptCount / 100.0);
            //             numAttemptsToUse = Math.Max(numAttemptsToUse, 1);

            // Gather split times
            bool shouldResetSplits = false;
            if (numSegments != splits_.Count || State.Run.GameName != splitGameName_ || State.Run.CategoryName != splitCatName_)
            {
                splitGameName_ = State.Run.GameName;
                splitCatName_ = State.Run.CategoryName;
                shouldResetSplits = true;
            }
            if (shouldResetSplits)
            {
                int numBuckets = Settings.MaxPrecision;
                dataTable1.Rows.Clear();
                dataTable1.Columns.Clear();
                dataTable1.Columns.Add("Computing..", typeof(string), null);
                foreach (DataGridViewColumn column in Settings.dataGridView1.Columns)
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                Settings.dataGridView1.Update();
                results_ = new string[1];

                //err1 check
                for (int segment = 0; segment < numSegments; segment++)
                {
                    if (State.Run[segment].SegmentHistory == null || State.Run[segment].SegmentHistory.Count == 0)
                    {
                        results_[0] = "err1";
                        return;
                    }
                }

                splits_.Clear();
                for (int s = 0; s < numSegments; s++)
                {
                    splits_.Add(new List<TimeSpan>());

                    //var segmentHistory = State.Run[s].SegmentHistory;
                    int numSegmentAttempts = State.Run[s].SegmentHistory.Count;
                    int numAttemptsToUse = Settings.AttemptCount;
                    if (!Settings.UseFixedAttempts)
                        numAttemptsToUse = (int)(numSegmentAttempts * Settings.AttemptCount / 100.0);
                    numAttemptsToUse = Math.Max(numAttemptsToUse, 2);

                    foreach (var a in Enumerable.Reverse(State.Run[s].SegmentHistory.Keys))
                    {
                        if (a < 0)
                            continue;
                        if (!(State.Run[s].SegmentHistory[a][State.CurrentTimingMethod] > TimeSpan.Zero))
                            continue;
                        splits_[s].Add(State.Run[s].SegmentHistory[a][State.CurrentTimingMethod].Value);
                        if (splits_[s].Count >= numAttemptsToUse)
                            break;
                    }
                    if (splits_[s].Count == 0)
                    {
                        results_[0] = "Err2";
                        return;
                    }
                    splits_[s].Sort();
                }

                futureRunHistogram_ = new List<TimeSpan>[numSegments];
                for (int s = numSegments - 1; s >= 0; s--)
                {
                    if (s == numSegments - 1)
                    {
                        futureRunHistogram_[s] = new List<TimeSpan>();

                        int jumpGap1 = (int)(Math.Ceiling((double)splits_[s].Count / numBuckets) + .1);
                        for (int a = 0; a < splits_[s].Count; a += jumpGap1)
                        {
                            futureRunHistogram_[s].Add(splits_[s][a]);
                        }
                        futureRunHistogram_[s].Sort();
                    }
                    else
                    {
                        futureRunHistogram_[s] = computeDistributionsSum(splits_[s], futureRunHistogram_[s + 1], numBuckets);
                    }
                }
                //possiblePaceHistogram_ = new List<TimeSpan>[numSegments];
                bestPace_ = new TimeSpan[numSegments];
                for (int s = 0; s < numSegments; s++)
                {
                    bestPace_[s] = splits_[s][0];
                    if (s > 0)
                    {
                        bestPace_[s] += bestPace_[s - 1];
                    }
                    /*if (s == 0)
                    {
                        possiblePaceHistogram_[s] = new List<TimeSpan>();

                        int jumpGap1 = (int)(Math.Ceiling((double)splits_[s].Count / numBuckets) + .1);
                        for (int a = 0; a < splits_[s].Count; a += jumpGap1)
                        {
                            possiblePaceHistogram_[s].Add(splits_[s][a]);
                        }
                        possiblePaceHistogram_[s].Sort();
                    }
                    else
                    {
                        possiblePaceHistogram_[s] = computeDistributionsSum(splits_[s], possiblePaceHistogram_[s - 1], numBuckets);
                    }*/
                }

                double computePBchance(int segmentIndex, TimeSpan curPace)
                {
                    TimeSpan acceptedFuture = pb[State.CurrentTimingMethod].Value - curPace;
                    int numSucceed = 0;
                    int maxCount = futureRunHistogram_[segmentIndex].Count;
                    for (int i = 0; i < maxCount; i++)
                    {
                        if (futureRunHistogram_[segmentIndex][i] <= acceptedFuture)
                            numSucceed++;
                        else
                            break;
                    }
                    return (double)numSucceed / futureRunHistogram_[segmentIndex].Count;
                }

                double pbChanceAtReset = computePBchance(0, TimeSpan.Zero);
                if (pbChanceAtReset <= 0.0000001)
                {
                    dataTable1.Columns.Clear();
                    dataTable1.Columns.Add(":(", typeof(string), null);
                    if (Settings.AltComputationMode)
                    {
                        dataTable1.Rows.Add(new string[] { "PB impossible with these splits! Try:" });
                        dataTable1.Rows.Add(new string[] { "Modify what recent splits to use" });
                    }
                    else
                    {
                        dataTable1.Rows.Add(new string[] { "PB chance too low! Try:" });
                        dataTable1.Rows.Add(new string[] { "1. use higher precision" });
                        dataTable1.Rows.Add(new string[] { "2. modify what recent splits to use" });
                        dataTable1.Rows.Add(new string[] { "3. Enable alternative mode" });
                    }
                    foreach (DataGridViewColumn column in Settings.dataGridView1.Columns)
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    return;
                }

                dataTable1.Columns.Clear();
                dataTable1.Columns.Add("Name", typeof(string), null);
                dataTable1.Columns.Add("Goal", typeof(string), null);
                foreach (DataGridViewColumn column in Settings.dataGridView1.Columns)
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataTable1.Rows.Add(new string[] { "PB chance", (pbChanceAtReset * 100.0).ToString("0.0000") + "%" });

                TimeSpan computeWorstAcceptablePaceFromX(TimeSpan fromPace, int millisecondIncrements, int segment)
                {
                    TimeSpan worstAcceptedPace = fromPace;
                    for (int pp = 0; ; pp++)
                    {
                        TimeSpan sampledPace = fromPace + TimeSpan.FromMilliseconds(pp * millisecondIncrements);
                        TimeSpan estimatedRunTimeLeft = pb[State.CurrentTimingMethod].Value - sampledPace;
                        if (Math.Abs((sampledPace - worstAcceptedPace).TotalSeconds) < .1)
                            continue;
                        double estimatedRunTimeLeftRatio = (double)estimatedRunTimeLeft.Ticks / pb[State.CurrentTimingMethod].Value.Ticks;
                        if (estimatedRunTimeLeftRatio <= 0)
                            break;
                        double acceptedChance = Math.Min(pbChanceAtReset * estimatedRunTimeLeftRatio, .5);
                        double pbChanceWithThisPace = computePBchance(segment + 1, sampledPace);
                        if (pbChanceWithThisPace <= 0) //impossible to pb
                            break;
                        if (pbChanceWithThisPace > acceptedChance)
                        {
                            worstAcceptedPace = sampledPace;
                        }
                        else
                            break;
                    }
                    return worstAcceptedPace;
                }

                //InternalComponent.InformationValue = (pbChanceAtReset * 100.0).ToString("0.0000") + "%"; return;
                results_ = new string[numSegments - 1];
                var myFormatter = new LiveSplit.TimeFormatters.RegularTimeFormatter(LiveSplit.TimeFormatters.TimeAccuracy.Tenths);
                for (int s = 0; s < numSegments-1; s++)
                {
                    TimeSpan worstAcceptedPace10sec = computeWorstAcceptablePaceFromX(bestPace_[s] - TimeSpan.FromSeconds(100), 10000, s);
                    TimeSpan worstAcceptedPace1sec = computeWorstAcceptablePaceFromX(worstAcceptedPace10sec - TimeSpan.FromSeconds(10), 1000, s);
                    TimeSpan worstAcceptedPace100ms = computeWorstAcceptablePaceFromX(worstAcceptedPace1sec - TimeSpan.FromSeconds(1), 100, s);
                    if (worstAcceptedPace100ms <= bestPace_[s])
                    {
                        results_[s] = "---";
                        dataTable1.Rows.Add(new string[2] { State.Run[s].Name, results_[s] });
                        continue;
                    }
                    results_[s] = myFormatter.Format(worstAcceptedPace100ms);
                    dataTable1.Rows.Add(new string[2] { State.Run[s].Name, results_[s] });
                }
            }

            // Calculate probability of PB
            /*int numSimulations = 1000000;
            int numSuccess = 0;
            for (int i = 0; i < numSimulations; i++)
            {
                // Get current time as a baseline
//                 Time test = State.CurrentTime;
//                 if (test[State.CurrentTimingMethod] < TimeSpan.Zero)
//                 {
//                     test[State.CurrentTimingMethod] = TimeSpan.Zero;
//                 }
                Time test = Time.Zero;

                // Add random split times for each remaining segment
                for (int segment = 0; segment < numSegments; segment++)
                {
//                     if (segment < State.CurrentSplitIndex)
//                     {
//                         continue;
//                     }

                    int randomIndex = rand.Next(splits_[segment].Count);
                    test += splits_[segment][randomIndex].Value;
                }

                if (test[State.CurrentTimingMethod] < pb[State.CurrentTimingMethod])
                {
                    numSuccess++;
                }
            }

            double prob = (double)numSuccess / numSimulations;
            string text = (prob * 100.0).ToString("0.0000") + "%";
            InternalComponent.InformationValue = text;*/
        }

        protected void updateText()
        {
            if (results_.Count() > 1)
            {
                if (State.CurrentSplitIndex >= 0 && State.CurrentSplitIndex < results_.Count())
                {
                    InternalComponent.InformationName = State.Run[State.CurrentSplitIndex].Name + " goal";
                    InternalComponent.InformationValue = results_[State.CurrentSplitIndex];
                }
                else
                {
                    InternalComponent.InformationName = "TimeTell";
                    InternalComponent.InformationValue = "-";
                }
            }
            else
            {
                InternalComponent.InformationName = "TimeTell";
                InternalComponent.InformationValue = "COMPUTE IN SETTINGS";
            }
        }

        void IComponent.DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            PrepareDraw(state, LayoutMode.Horizontal);
            InternalComponent.DrawHorizontal(g, state, height, clipRegion);
        }

        void IComponent.DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            InternalComponent.PrepareDraw(state, LayoutMode.Vertical);
            PrepareDraw(state, LayoutMode.Vertical);
            InternalComponent.DrawVertical(g, state, width, clipRegion);
        }

        void PrepareDraw(LiveSplitState state, LayoutMode mode)
        {
            InternalComponent.NameLabel.ForeColor = state.LayoutSettings.TextColor;
            InternalComponent.ValueLabel.ForeColor = state.LayoutSettings.TextColor;
            InternalComponent.PrepareDraw(state, mode);
        }

        void IComponent.Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            string newCategory = State.Run.GameName + State.Run.CategoryName;
            if (newCategory != category)
            {
                updateText();
                category = newCategory;
            }

            InternalComponent.Update(invalidator, state, width, height, mode);
        }

        void IDisposable.Dispose()
        {

        }
    }
}
