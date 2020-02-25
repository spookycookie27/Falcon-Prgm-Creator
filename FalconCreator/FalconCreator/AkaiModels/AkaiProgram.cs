﻿using System.Collections.Generic;

namespace FalconCreator.AkaiModels
{
    public class AkaiProgram
    {
        public AkaiProgram()
        {
            Keygroups = new List<Keygroup>();
            PrgValues = new List<string>();
            OutValues = new List<string>();
            TuneValues = new List<string>();
            LfoValues = new List<string>();
            Lfo2Values = new List<string>();
            ModsValues = new List<string>();
        }
        public string Name { get; set; }
        public List<Keygroup> Keygroups { get; set; }
        public List<string> PrgValues { get; set; }
        public List<string> OutValues { get; set; }
        public List<string> TuneValues { get; set; }
        public List<string> LfoValues { get; set; }
        public List<string> Lfo2Values { get; set; }
        public List<string> ModsValues { get; set; }
    }
}
