using System;
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using Esri.HPFramework;
using Unity.Mathematics;
using UnityEngine;

namespace Source.Controls
{
    public class MapTouchController : MonoBehaviour
    {
        private ArcGISMapComponent _mapComponent;
        private ArcGISLocationComponent _locationComponent;
        private HPTransform _transform;
        private GameObject eventSystem;

        private void Awake()
        {
            _mapComponent = transform.parent.GetComponent<ArcGISMapComponent>();
            _transform = GetComponent<HPTransform>();
            _locationComponent = GetComponent<ArcGISLocationComponent>();
        }

        void Update()
        {
            foreach(Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    var pos = _mapComponent.View.GeographicToWorld(
                        new ArcGISPoint(_locationComponent.Position.X,
                            _locationComponent.Position.Y, _locationComponent.Position.Z));
                       pos.x += touch.deltaPosition.x * 0.1f;
                       pos.y += -touch.deltaPosition.y * 0.1f;

                       _transform.UniversePosition = pos;
                }
            }
        }
    }
}