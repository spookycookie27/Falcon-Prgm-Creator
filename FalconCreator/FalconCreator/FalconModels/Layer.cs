using System.Collections.Generic;

namespace FalconCreator.FalconModels
{
    public class Layer
    {
        public Layer()
        {
            Name = "Layer 0";
            Bypass = "0";
            Gain = "1";
            Pan = "0";
            Mute = "0";
            MidiMute = "0";
            Solo = "0";
            DisplayName = "Layer 1";
            OutputName = "";
            LowKey = "0";
            HighKey = "127";
            CustomPolyphony = "0";
            PlayMode = "0";
            PortamentoTime = "0.029999999";
            PortamentoCurve = "0";
            PortamentoMode = "0";
            NumVoicesPerNote = "1";
            VelocityCurve = "0";
            BypassInsertFX = "0";
        }
        public string Name { get; set; }
        public string Bypass { get; set; }
        public string Gain { get; set; }
        public string Pan { get; set; }
        public string Mute { get; set; }
        public string MidiMute { get; set; }
        public string Solo { get; set; }
        public string DisplayName { get; set; }
        public string OutputName { get; set; }
        public string LowKey { get; set; }
        public string HighKey { get; set; }
        public string CustomPolyphony { get; set; }
        public string PlayMode { get; set; }
        public string PortamentoTime { get; set; }
        public string PortamentoCurve { get; set; }
        public string PortamentoMode { get; set; }
        public string NumVoicesPerNote { get; set; }
        public string VelocityCurve { get; set; }
        public string BypassInsertFX { get; set; }
        public List<KeyGroup> Keygroups { get; set; }

    }
}
