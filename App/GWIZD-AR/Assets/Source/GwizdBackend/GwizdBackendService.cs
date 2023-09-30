using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Source.GwizdBackend
{
    public static class GwizdBackendService
    {
        private const string Url = "https://gwizd-hackyeah2023.azurewebsites.net//api/AnimalReport/animal-report";

        public static void SendAnimalReport(AnimalReport animalReport)
        {
            UnityWebRequest request = new UnityWebRequest(Url, "PUT");
            request.SetRequestHeader("Content-Type", "application/json");
            var serialized = JsonConvert.SerializeObject(animalReport);
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(serialized));
            request.SendWebRequest();
        }
    }
}