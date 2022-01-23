using UnityEngine;

namespace GameScene
{
    public class GhostState : IState
    {
        private Car _car;
        private CarRecordData _carRecordData;
        private bool _canGhostsMove;

        public GhostState(Car car, CarRecordData carRecordData)
        {
            _car = car;
            _carRecordData = carRecordData;
        }
        public void Enter()
        {
            DrivingState.OnDriveStarted += StartGhostDriving;
        }

        public void UpdateState()
        {
            
        }

        public void FixedUpdateState()
        {
            if (_canGhostsMove)
            {
                var nextTransformData = _carRecordData.GetNextTransform();
                _car.transform.position = nextTransformData.position;
                _car.transform.rotation = Quaternion.Euler(nextTransformData.rotation);
            }
        }

        public void Exit()
        {
            DrivingState.OnDriveStarted -= StopGhostDriving;
        }

        private void StopGhostDriving()
        {
            _carRecordData.Reset();
            _canGhostsMove = false;
        }
        
        private void StartGhostDriving()
        {
            _canGhostsMove = true;
        }
    }
}