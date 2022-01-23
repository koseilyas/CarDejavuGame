namespace GameScene
{
    public class IdleState : IState
    {
        private Car _car;

        public IdleState(Car car)
        {
            _car = car;
        }
        public void Enter()
        {
            _car.gameElementTransformation.MoveToIdlePosition();
        }

        public void UpdateState()
        {
            
        }

        public void FixedUpdateState()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}