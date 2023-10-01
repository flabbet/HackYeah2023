using System;
using Source.GwizdBackend;
using UnityEngine;
using UnityEngine.UIElements;

namespace Source.UI
{
    public class AnimalPopupController : MonoBehaviour
    {
        public CenterUIController centerUIController;
        public VisualTreeAsset popup;
        private void Update()
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.TryGetComponent<Animal>(out Animal animal))
                        {
                            centerUIController.SetDocument(popup);
                            FillPopup(animal);
                        }
                    }
                }
            }
        }

        private void FillPopup(Animal component)
        {
            centerUIController.document.rootVisualElement.Q<Label>("AnimalName").text = component.AnimalData.animalName;
            centerUIController.document.rootVisualElement.Q<VisualElement>("Image").style.backgroundImage = component.AnimalReport.GetTexture();
            centerUIController.document.rootVisualElement.Q<VisualElement>("PopUp").RegisterCallback<ClickEvent>(evt => OnCloseButtonClicked());
        }

        private void OnCloseButtonClicked()
        {
            centerUIController.SetDocument(null);
        }
    }
}