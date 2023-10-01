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
        private ArcGISLocationComponent _locationComponent;

        private void Awake()
        {
            _locationComponent = GetComponent<ArcGISLocationComponent>();
        }

        void Update()
        {
            foreach(Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Moved)
                {

                    _locationComponent.Position = new ArcGISPoint(
                        _locationComponent.Position.X + touch.deltaPosition.x * 10f,
                        _locationComponent.Position.Y + touch.deltaPosition.y * 10f,
                        _locationComponent.Position.Z);

                    _locationComponent.SyncPositionWithHPTransform();
                }
            }
        }
    }
}