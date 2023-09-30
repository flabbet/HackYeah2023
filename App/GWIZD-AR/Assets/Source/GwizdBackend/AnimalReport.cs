using System;

namespace Source.GwizdBackend
{
    [Serializable]
    public class AnimalReport
    {
        public string AnimalId { get; set; } = null!;
        public string PhotoBase64 { get; set; } = null!;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}