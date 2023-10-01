using UnityEngine;
using UnityEngine.UIElements;

namespace Source.GwizdBackend
{
    [CreateAssetMenu(fileName = "AnimalData", menuName = "ScriptableObjects/AnimalData", order = 1)]
    public class AnimalData : ScriptableObject
    {
        public string animalName;
        public GameObject associatedPrefab;
        public string animalId;
    }
}