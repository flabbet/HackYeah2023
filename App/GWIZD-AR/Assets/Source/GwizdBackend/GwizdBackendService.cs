using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Source.GwizdBackend
{
    public static class GwizdBackendService
    {
        private const string BaseUrl = "https://gwizd-hackyeah2023.azurewebsites.net//api/";
        private const string PostAlertUrl = "AnimalReport/animal-report";
        private const string GetAllUrl = "AnimalReport/get-all-reports";

        public static void SendAnimalReport(AnimalReport animalReport)
        {
            UnityWebRequest request = new UnityWebRequest(BaseUrl + PostAlertUrl, "PUT");
            request.SetRequestHeader("Content-Type", "application/json");
            var serialized = JsonConvert.SerializeObject(animalReport);
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(serialized));
            request.SendWebRequest();
        }

        public static void GetAnimalReports(Action<AnimalReport[]> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(BaseUrl + GetAllUrl);
            request.SetRequestHeader("Content-Type", "application/json");
            var requestOperation = request.SendWebRequest();

            requestOperation.completed += operation =>
            {
                if (request.result != UnityWebRequest.Result.Success) return;

                var response = request.downloadHandler.text;
                var animalReports = JsonConvert.DeserializeObject<AnimalReport[]>(response);
                callback(animalReports);
            };
        }
    }
}