
namespace GameScene
{
    public class CarsController
    {
        private Car[] _cars;
        private int _lastCarIndex;

        public CarsController(Car[] carsArr)
        {
            _cars = carsArr;
            Car.OnCarStateReset += NextCar;
        }

        private void NextCar(int carIndex)
        {
            StartCar(carIndex);
        }

        public void StartCar(int carIndex)
        {
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
