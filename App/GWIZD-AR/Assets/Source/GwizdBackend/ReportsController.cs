using System;
using System.Collections;
using System.Collections.Generic;
using Esri.ArcGISMapsSDK.Components;
using Esri.HPFramework;
using Unity.Mathematics;
using UnityEngine;

namespace Source.GwizdBackend
{
    public class ReportsController : MonoBehaviour
    {
        public AnimalReport[] ActiveReports { get; set; }
        public GameObject discPrefab;
        private ArcGISMapComponent _mapComponent;
        private List<GameObject> _activeReportObjects = new List<GameObject>();

        private Coroutine _fetchDbCoroutine;
        private void Start()
        {
            _mapComponent = GetComponent<ArcGISMapComponent>();
            _fetchDbCoroutine = StartCoroutine(FetchDbCoroutine());
        }

        private IEnumerator FetchDbCoroutine()
        {
            while (true)
            {
                FetchDb();
                yield return new WaitForSeconds(5);
            }
        }

        private void OnDestroy()
        {
            StopCoroutine(_fetchDbCoroutine);
        }

        private void FetchDb()
        {
            GwizdBackendService.GetAnimalReports(animalReports =>
            {
                ActiveReports = animalReports;
                UpdateReportsOnMap();
            });
        }

        private void UpdateReportsOnMap()
        {
            for (int i = 0; i < _activeReportObjects.Count; i++)
            {
                Destroy(_activeReportObjects[i]);
            }

            _activeReportObjects.Clear();

            foreach (var report in ActiveReports)
            {
                var animalData = Resources.LoadAsync($"{report.AnimalId}");
                animalData.completed += operation =>
                {
                    var animalData = ((ResourceRequest) operation).asset as AnimalData;
                    var animalObject = Instantiate(animalData.associatedPrefab, _mapComponent.transform);
                    var geo = _mapComponent.View.GeographicToWorld(
                        new Esri.GameEngine.Geometry.ArcGISPoint(report.Latitude, report.Longitude, 0));
                        var arcGisComponent = animalObject.AddComponent<ArcGISLocationComponent>();
                        arcGisComponent.Position = new Esri.GameEngine.Geometry.ArcGISPoint(geo.x, geo.y, geo.z);
                        arcGisComponent.GetComponent<HPTransform>().LocalScale = new float3(40, 40, 40);
                        arcGisComponent.transform.GetChild(0).transform.position += new Vector3(0, 0.7f, 0);
                        Animal animal = arcGisComponent.gameObject.AddComponent<Animal>();
                        animal.AnimalData = animalData;
                        animal.AnimalReport = report;
                        Instantiate(discPrefab, arcGisComponent.transform.GetChild(0));
                    _activeReportObjects.Add(animalObject);
                };
            }
        }
    }
}