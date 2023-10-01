using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Source.UI
{
    public class AnimalPickerUI : MonoBehaviour
    {
        public static string NextAnimalId { get; private set; } = "deer";
        private UIDocument _uiDocument;

        private VisualElement deerButton;
        private VisualElement boarButton;
        private VisualElement mooseButton;
        private VisualElement dogButton;
        private VisualElement catButton;
        private VisualElement racoonButton;

        private void OnEnable()
        {
            _uiDocument = GetComponent<UIDocument>();
            var root = _uiDocument.rootVisualElement;
            deerButton = root.Q<VisualElement>("DeerButton");
            boarButton = root.Q<VisualElement>("BoarButton");
            mooseButton = root.Q<VisualElement>("MooseButton");
            dogButton = root.Q<VisualElement>("DogButton");
            catButton = root.Q<VisualElement>("CatButton");
            racoonButton = root.Q<VisualElement>("RacoonButton");

            deerButton.RegisterCallback<ClickEvent>(evt => OnAnimalSelected("deer"));
            boarButton.RegisterCallback<ClickEvent>(evt => OnAnimalSelected("boar"));
            mooseButton.RegisterCallback<ClickEvent>(evt => OnAnimalSelected("moose"));
            dogButton.RegisterCallback<ClickEvent>(evt => OnAnimalSelected("dog"));
            catButton.RegisterCallback<ClickEvent>(evt => OnAnimalSelected("cat"));
            racoonButton.RegisterCallback<ClickEvent>(evt => OnAnimalSelected("racoon"));
        }

        private void OnAnimalSelected(string id)
        {
            NextAnimalId = id;
            SceneManager.LoadScene(2);
        }
    }
}