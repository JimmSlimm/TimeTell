using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;

[assembly: ComponentFactory(typeof(TimeTell.TimeTellFactory))]

namespace TimeTell
{
    class TimeTellFactory : IComponentFactory
    {
        public string ComponentName => "TimeTell";

        public string Description => "Shows what pace should be good enough to continue run.";

        public ComponentCategory Category => ComponentCategory.Information;

        public IComponent Create(LiveSplitState state) => new TimeTellComponent(state);

        public string UpdateName => ComponentName;

        public string XMLURL => "http://livesplit.org/update/Components/update.TimeTell.xml";

        public string UpdateURL => "http://livesplit.org/update/";

        public Version Version => Version.Parse("0.0.1");
    }
}
