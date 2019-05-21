using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.UI;

namespace TimeTell
{
    public partial class TimeTellSettings : UserControl
    {
        public Boolean UsePercentOfAttempts { get; set; }
        public Boolean UseFixedAttempts { get; set; }
        public int AttemptCount { get; set; }
        public int MaxPrecision { get; set; }
        public bool AltComputationMode { get; set; }

        public event EventHandler SettingChanged;
        public event EventHandler SettingCompute;

        public TimeTellSettings()
        {
            InitializeComponent();

            UsePercentOfAttempts = true;
            UseFixedAttempts = false;
            AttemptCount = 20;
            MaxPrecision = 800;
            AltComputationMode = false;

            PercentOfAttempts.DataBindings.Add("Checked", this, "UsePercentOfAttempts", true, DataSourceUpdateMode.OnPropertyChanged).BindingComplete += OnSettingChanged;
            FixedAttempts.DataBindings.Add("Checked", this, "UseFixedAttempts", true, DataSourceUpdateMode.OnPropertyChanged).BindingComplete += OnSettingChanged;
            AttemptCountBox.DataBindings.Add("Value", this, "AttemptCount", true, DataSourceUpdateMode.OnPropertyChanged).BindingComplete += OnSettingChanged;
            MaxPrecisionCountBox.DataBindings.Add("Value", this, "MaxPrecision", true, DataSourceUpdateMode.OnPropertyChanged).BindingComplete += OnSettingChanged;
            alt_comp_mode_cb.DataBindings.Add("Checked", this, "AltComputationMode", true, DataSourceUpdateMode.OnPropertyChanged).BindingComplete += OnSettingChanged;
        }

        private void OnSettingChanged(object sender, BindingCompleteEventArgs e)
        {
            SettingChanged?.Invoke(this, e);
        }

        public LayoutMode Mode { get; internal set; }

        internal XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "0.1") ^
                SettingsHelper.CreateSetting(document, parent, "AttemptCount", AttemptCount) ^
                SettingsHelper.CreateSetting(document, parent, "MaxPrecision", MaxPrecision) ^
                SettingsHelper.CreateSetting(document, parent, "UsePercentOfAttempts", UsePercentOfAttempts) ^
                SettingsHelper.CreateSetting(document, parent, "AltComputationMode", AltComputationMode) ^
                SettingsHelper.CreateSetting(document, parent, "UseFixedAttempts", UseFixedAttempts);
        }
        
        internal void SetSettings(XmlNode settings)
        {
            AttemptCount = SettingsHelper.ParseInt(settings["AttemptCount"]);
            MaxPrecision = SettingsHelper.ParseInt(settings["MaxPrecision"]);
            AltComputationMode = SettingsHelper.ParseBool(settings["AltComputationMode"]);
            UsePercentOfAttempts = SettingsHelper.ParseBool(settings["UsePercentOfAttempts"]);
            UseFixedAttempts = SettingsHelper.ParseBool(settings["UseFixedAttempts"]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettingCompute?.Invoke(this, e);
        }
    }
}
