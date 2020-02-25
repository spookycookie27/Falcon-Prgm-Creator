
using System.Collections.Generic;

namespace FalconCreator.FalconModels
{
    public class Program
    {
        public Program()
        {
            Name = "Program";
            Bypass = "0";
            Gain = "1";
            Pan = "0";
            DisplayName = "Alto Expression";
            TransposeOctaves = "0";
            TransposeSemiTones = "0";
            OutputName = "";
            Polyphony = "16";
            NotePolyphony = "0";
            ProgramPath = "/Users/prittim/Desktop/Alto Expression.uvip";
            LoopProgram = "0";
            Streaming = "1";
            BypassInsertFX = "0";
            ControlSignalSources = new List<DAHDSR>();
            Layers = new List<Layer>();
        }

        public string Name { get; set; }
        public string Bypass { get; set; }
        public string Gain { get; set; }
        public string Pan { get; set; }
        public string DisplayName { get; set; }
        public string TransposeOctaves { get; set; }
        public string TransposeSemiTones { get; set; }
        public string OutputName { get; set; }
        public string Polyphony { get; set; }
        public string NotePolyphony { get; set; }
        public string ProgramPath { get; set; }
        public string LoopProgram { get; set; }
        public string Streaming { get; set; }
        public string BypassInsertFX { get; set; }
        public List<DAHDSR> ControlSignalSources { get; set; }
        public List<Layer> Layers { get; set; }
    }
}