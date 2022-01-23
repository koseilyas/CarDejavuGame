
namespace GameScene
{
    public class CarsController
    {
        private Car[] _cars;
        private GameManager _gameManager;
        private int _lastCarIndex;

        public CarsController(Car[] carsArr, GameManager gameManager)
        {
            _cars = carsArr;
            _gameManager = gameManager;
            Car.OnCarStateReset += NextCar;
        }

        private void NextCar(int carIndex)
        {
            StartCar(carIndex);
        }

        public void StartCar(int carIndex)
        {
            if (carIndex == _cars.Length)
            {
                Car.OnCarStateReset -= NextCar;
                _gameManager.NextLevel();
            }
               
            for (int i = 0; i < _cars.Length; i++)
            {
                Car car = _cars[i];
                if (i < carIndex)
                {
                    car.ChangeState(car.restartState);
                }else if (i == carIndex)
                {
                    car.ChangeState(car.teleportingState);
                }else if (i > carIndex)
                {
                    car.ChangeState(car.idleState);
                }
            }
        }
    }
}
