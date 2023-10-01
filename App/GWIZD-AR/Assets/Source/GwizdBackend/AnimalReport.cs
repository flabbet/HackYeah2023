using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Source.GwizdBackend
{
    [Serializable]
    public class AnimalReport
    {
        public string AnimalId { get; set; } = null!;
        public string PhotoBase64 { get; set; } = null!;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public StyleBackground GetTexture()
        {
            byte[] bytes = Convert.FromBase64String(PhotoBase64);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            return new StyleBackground(texture);
        }
    }
}