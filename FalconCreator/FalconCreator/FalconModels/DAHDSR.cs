namespace FalconCreator.FalconModels
{
    public class DAHDSR
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Bypass { get; set; }
        public string DelayTime { get; set; }
        public string AttackTime { get; set; }
        public string AttackCurve { get; set; }
        public string HoldTime { get; set; }
        public string DecayTime { get; set; }
        public string DecayCurve { get; set; }
        public string SustainLevel { get; set; }
        public string ReleaseTime { get; set; }
        public string ReleaseCurve { get; set; }
        public string VelocityAmount { get; set; }
        public string VelocitySens { get; set; }
        public string Retrigger { get; set; }
        public string NoteOffRetrigger { get; set; }

    }
}