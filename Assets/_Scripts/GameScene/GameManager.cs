using System;
using UnityEngine;

namespace GameScene
{
    public class GameManager : MonoBehaviour
    {
        private LevelLoader _levelLoader;
        public ElementReferences elementReferences;
        public static GameManager Instance;
        private CarsController _carsController;
        public static event Action OnLevelStart;

        private void Awake()
        {
            Instance = this;
            elementReferences.InitTransforms();
            _levelLoader = new LevelLoader(1,elementReferences);
            _carsController = new CarsController(elementReferences.cars);
        }

        private void Start()
        {
            OnLevelStart?.Invoke();
            _carsController.StartCar(0);
        }
    }
}