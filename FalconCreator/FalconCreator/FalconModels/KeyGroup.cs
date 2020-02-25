using System.Collections.Generic;

namespace FalconCreator.FalconModels
{
    public class KeyGroup
    {
        public string Name { get; set; }
        public string Bypass { get; set; }
        public string Gain { get; set; }
        public string Pan { get; set; }
        public string DisplayName { get; set; }
        public string OutputName { get; set; }
        public string ExclusiveGroup { get; set; }
        public string LowKey { get; set; }
        public string HighKey { get; set; }
        public string LowVelocity { get; set; }
        public string HighVelocity { get; set; }
        public string LowKeyFade { get; set; }
        public string HighKeyFade { get; set; }
        public string LowVelocityFade { get; set; }
        public string HighVelocityFade { get; set; }
        public string FadeCurve { get; set; }
        public string TriggerMode { get; set; }
        public string TriggerSync { get; set; }
        public string TriggerRule { get; set; }
        public List<SignalConnection> Connections { get; set; }
        public List<DAHDSR> ControlSignalSources { get; set; }


    }

}