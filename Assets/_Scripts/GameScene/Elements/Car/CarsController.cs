
namespace GameScene
{
    public class CarsController
    {
        
        public Car[] cars;

        public CarsController(Car[] carsArr)
        {
            cars = carsArr;
            Car.OnCarReachedTarget += NextCar;
        }

        private void NextCar(int carIndex)
        {
            StartCar(carIndex);
        }

        public void StartCar(int carIndex)
        {
            for (int i = 0; i < cars.Length; i++)
            {
                Car car = cars[i];
                if (i < carIndex)
                {
                    car.ChangeState(car.ghostState);
                }else if (i == carIndex)
                {
                    car.ChangeState(car.teleportingState);
                }
            }
        }
    }
}
