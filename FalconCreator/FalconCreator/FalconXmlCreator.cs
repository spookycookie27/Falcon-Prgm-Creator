
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using FalconCreator.AkaiModels;

namespace FalconCreator
{
    public class FalconXmlCreator
    {
        private readonly AkaiProgram _program;
        private readonly Dictionary<string, string> _noteMap;
        private string color = "ffbe88cc"; 
        public FalconXmlCreator(AkaiProgram program)
        {
            this._program = program;
            _noteMap = GetNoteMapsDictionary();
        }

        public XElement CreateXml()
        {
            var falconProgram = new XElement("UVI4", Program());
            return falconProgram;
        }

        private XElement Program()
        {
            return new XElement("Program",
                new XAttribute("Name", "Program"),
                new XAttribute("Bypass", "0"),
                new XAttribute("Gain", "1"),
                new XAttribute("an", "0"),
                new XAttribute("DisplayName", $"{_program.Name}"),
                new XAttribute("TransposeOctaves", "0"),
                new XAttribute("TransposeSemiTones", "0"),
                new XAttribute("OutputName", ""),
                new XAttribute("Polyphony", "16"),
                new XAttribute("NotePolyphony", "0"),
                new XAttribute("ProgramPath", $"//{_program.Name}.uvip"),
                new XAttribute("LoopProgram", "0"),
                new XAttribute("Streaming", "1"),
                new XAttribute("BypassInsertFX", "0"),
                new XElement("ControlSignalSources"),
                new XElement("Layers", Layer())
            );
        }

        private XElement Layer()
        {
            return new XElement("Layer",
                new XAttribute("Name", "Layer 0"),
                new XAttribute("Bypass", "0"),
                new XAttribute("Gain", "1"),
                new XAttribute("Pan", "0"),
                new XAttribute("Mute", "0"),
                new XAttribute("MidiMute", "0"),
                new XAttribute("Solo", "0"),
                new XAttribute("DisplayName", "Layer 1"),
                new XAttribute("OutputName", ""),
                new XAttribute("LowKey", "0"),
                new XAttribute("HighKey", "127"),
                new XAttribute("CustomPolyphony", "0"),
                new XAttribute("PlayMode", "0"),
                new XAttribute("PortamentoTime", "0.029999999"),
                new XAttribute("PortamentoCurve", "0"),
                new XAttribute("PortamentoMode", "0"),
                new XAttribute("NumVoicesPerNote", "1"),
                new XAttribute("VelocityCurve", "0"),
                new XAttribute("BypassInsertFX", "0"),
                new XElement("Properties", new XAttribute("Color", color)),
                new XElement("ControlSignalSources"),
                new XElement("BusRouters"),
                Keygroups()
            );
        }

        private XElement Keygroups()
        {
            var keyGroups = new XElement("Keygroups");
            int kgrpCount = 0;
            foreach (var keygroup in _program.Keygroups)
            {
                if (!string.IsNullOrEmpty(keygroup.Zone1[0]))
                {
                    var zone1 = Keygroup(kgrpCount, keygroup.Zone1, keygroup.Klocation);
                    kgrpCount++;
                    keyGroups.Add(zone1);
                }
                if (!string.IsNullOrEmpty(keygroup.Zone2[0]))
                {
                    var zone2 = Keygroup(kgrpCount, keygroup.Zone2, keygroup.Klocation);
                    kgrpCount++;
                    keyGroups.Add(zone2);
                }
                if (!string.IsNullOrEmpty(keygroup.Zone3[0]))
                {
                    var zone3 = Keygroup(kgrpCount, keygroup.Zone3, keygroup.Klocation);
                    kgrpCount++;
                    keyGroups.Add(zone3);
                }
                if (!string.IsNullOrEmpty(keygroup.Zone4[0]))
                {
                    var zone4 = Keygroup(kgrpCount, keygroup.Zone4, keygroup.Klocation);
                    kgrpCount++;
                    keyGroups.Add(zone4);
                }
            }
            return keyGroups;
        }

        private XElement Keygroup(int kgrpCount, List<string> zone, List<string> kloc)
        {
            return new XElement("Keygroup",
                new XAttribute("Name", $"Keygroup {kgrpCount}"),
                new XAttribute("Bypass", "0"),
                new XAttribute("Gain", "1"),
                new XAttribute("Pan", "0"),
                new XAttribute("DisplayName", $"{zone[0]}.WAV"),
                new XAttribute("OutputName", ""),
                new XAttribute("ExclusiveGroup", "0"),
                new XAttribute("LowKey", kloc[4]),
                new XAttribute("HighKey", kloc[5]),
                new XAttribute("LowVelocity", SanitizeLowVelocity(zone[1])),
                new XAttribute("HighVelocity", zone[2]),
                new XAttribute("LowKeyFade", "0"),
                new XAttribute("HighKeyFade", "0"),
                new XAttribute("LowVelocityFade", "0"),
                new XAttribute("HighVelocityFade", "0"),
                new XAttribute("FadeCurve", "2"),
                new XAttribute("TriggerMode", "0"),
                new XAttribute("TriggerSync", "0"),
                new XAttribute("TriggerRule", "0"),
                new XAttribute("LatchTrigger", "0"),
                new XAttribute("FXPostGain", "0"),
                new XAttribute("BypassInsertFX", "0"),
                new XElement("Connections", KeygroupSignalConnection()),
                new XElement("ControlSignalSources", DAHDSR()),
                new XElement("BusRouters"),
                new XElement("Oscillators", SamplePlayer(zone))
            );
        }

        private string SanitizeLowVelocity(string value)
        {
            return value == "0" ? "1" : value;
        }

        private XElement SamplePlayer(List<string> zone)
        {
            var problems = new[]
            {
                "TNR SW5 C#5C", "TPT FF B4 +", "TPT FF D5+", "TPT FF F5+", "TPT FF G5+", "TPT FF A#5+", "TPT PP F3B"
            };
            var sampleName = zone[0];
            string nameForNoteNumber = "";
            string key = "";
            if (problems.Contains(sampleName) || sampleName.EndsWith("L"))
            {
                nameForNoteNumber = sampleName.TrimEnd(sampleName[^1]).Trim();
            }
            else
            {
                nameForNoteNumber = sampleName.Trim();
            }

            key = nameForNoteNumber.Substring(nameForNoteNumber.Length - 3).Replace(" ", "");
            
            if (!_noteMap.ContainsKey(key))
            {
                // problem here
            }

            var element = new XElement("SamplePlayer",
                new XAttribute("Name", "Oscillator"),
                new XAttribute("Bypass", "0"),
                new XAttribute("CoarseTune", GetCoarseTune(zone[4])),
                new XAttribute("FineTune", zone[3]),
                new XAttribute("Gain", "1"),
                new XAttribute("Pitch", "0"),
                new XAttribute("NoteTracking", "1"),
                new XAttribute("BaseNote", GetNoteNumber(key)),
                new XAttribute("DisplayName", "Oscillator 1"),
                new XAttribute("SampleStart", "0"),
                new XAttribute("InterpolationMode", "1"),
                new XAttribute("AllowStreaming", "1"),
                new XAttribute("SamplePath", $"./Samples/{sampleName}.WAV"),
                new XAttribute("SamplePurged", "0"),
                new XElement("Connections", SamplePlayerSignalConnection())
            );

            return element;
        }

        private string GetCoarseTune(string akaiValue)
        {
            int value = int.Parse(akaiValue);
            int realValue = value > 127 ? 0 - (256 - value) : value;
            return realValue.ToString();
        }

        private XElement KeygroupSignalConnection()
        {
            return new XElement("SignalConnection",
                new XAttribute("Name", "AmpEnvMod"),
                new XAttribute("Ratio", "1"),
                new XAttribute("Source", "Amp. Env"),
                new XAttribute("Destination", "Gain"),
                new XAttribute("Mapper", ""),
                new XAttribute("ConnectionMode", "0"),
                new XAttribute("Bypass", "0"),
                new XAttribute("Inverted", "0")
            );
        }

        private XElement DAHDSR()
        {
            return new XElement("DAHDSR",
                new XAttribute("Name", "Amp. Env"),
                new XAttribute("DisplayName", "Amp. Env"),
                new XAttribute("Bypass", "0"),
                new XAttribute("DelayTime", "0"),
                new XAttribute("AttackTime", "0"),
                new XAttribute("AttackCurve", "0.5"),
                new XAttribute("HoldTime", "0"),
                new XAttribute("DecayTime", "0"),
                new XAttribute("DecayCurve", "-0.97000003"),
                new XAttribute("SustainLevel", "1"),
                new XAttribute("ReleaseTime", "0.050000001"),
                new XAttribute("ReleaseCurve", "-0.97000003"),
                new XAttribute("VelocityAmount", "0.82999998"),
                new XAttribute("VelocitySens", "0.75"),
                new XAttribute("Retrigger", "1"),
                new XAttribute("NoteOffRetrigger", "0")
            );
        }

        private XElement SamplePlayerSignalConnection()
        {
            return new XElement("SignalConnection",
                new XAttribute("Name", "PitchBendMod"),
                new XAttribute("Ratio", "2"),
                new XAttribute("Source", "@PitchBend"),
                new XAttribute("Destination", "Pitch"),
                new XAttribute("Mapper", ""),
                new XAttribute("ConnectionMode", "0"),
                new XAttribute("Bypass", "0"),
                new XAttribute("Inverted", "0")
            );
        }

        private string GetNoteNumber(string key)
        {
            return _noteMap[key.ToUpper()];
        }

        private static Dictionary<string, string> GetNoteMapsDictionary()
        {
            return new Dictionary<string, string>
            {
                {"F0", "29"},
                {"F#0", "30"},
                {"G0", "31"},
                {"G#0", "32"},
                {"A0", "33"},
                {"A#0", "34"},
                {"B0", "35"},
                {"C1", "36"},
                {"C#1", "37"},
                {"D1", "38"},
                {"D#1", "39"},
                {"E1", "40"},
                {"F1", "41"},
                {"F#1", "42"},
                {"G1", "43"},
                {"G#1", "44"},
                {"A1", "45"},
                {"A#1", "46"},
                {"B1", "47"},
                {"C2", "48"},
                {"C#2", "49"},
                {"D2", "50"},
                {"D#2", "51"},
                {"E2", "52"},
                {"F2", "53"},
                {"F#2", "54"},
                {"G2", "55"},
                {"G#2", "56"},
                {"A2", "57"},
                {"A#2", "58"},
                {"B2", "59"},
                {"C3", "60"},
                {"C#3", "61"},
                {"D3", "62"},
                {"D#3", "63"},
                {"E3", "64"},
                {"F3", "65"},
                {"F#3", "66"},
                {"G3", "67"},
                {"G#3", "68"},
                {"A3", "69"},
                {"A#3", "70"},
                {"B3", "71"},
                {"C4", "72"},
                {"C#4", "73"},
                {"D4", "74"},
                {"D#4", "75"},
                {"E4", "76"},
                {"F4", "77"},
                {"F#4", "78"},
                {"G4", "79"},
                {"G#4", "80"},
                {"A4", "81"},
                {"A#4", "82"},
                {"B4", "83"},
                {"C5", "84"},
                {"C#5", "85"},
                {"D5", "86"},
                {"D#5", "87"},
                {"E5", "88"},
                {"F5", "89"},
                {"F#5", "90"},
                {"G5", "91"},
                {"G#5", "92"},
                {"A5", "93"},
                {"A#5", "94"},
                {"B5", "95"},
                {"C6", "96"},
                {"C6B", "96"},
                {"C#6", "97"},
                {"D6", "98"},
                {"D#6", "99"},
                {"E6", "100"},
                {"F6", "101"},
                {"F#6", "102"},
                {"G6", "103"},
                {"G#6", "104"},
                {"A6", "105"},
                {"A#6", "106"}
            };
        }
    }
}