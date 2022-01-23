using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScene
{
    public class GameManager : MonoBehaviour
    {
        private static int _level = 1;
        private CarsController _carsController;
        public ElementReferences elementReferences;

        private void Awake()
        {
            elementReferences.InitTransforms();
            var levelLoader = new LevelLoader(_level,elementReferences);
            _carsController = new CarsController(elementReferences.cars,this);
        }

        private void Start()
        {
            _carsController.StartCar(0);
        }

        public void NextLevel()
        {
            _level++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}