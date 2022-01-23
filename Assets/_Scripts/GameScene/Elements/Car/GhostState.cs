using Unity.Mathematics;
using UnityEngine;

namespace GameScene
{
    public class GhostState : IState
    {
        private Car _car;
        private CarRecordData _carRecordData;
        private bool _canGhostsMove;
        private Transform _transform;

        public GhostState(Car car, CarRecordData carRecordData)
        {
            _car = car;
            _carRecordData = carRecordData;
            DrivingState.OnDriveStarted += StartGhostDriving;
        }
        public void Enter()
        {
            _transform = _car.transform;
            ResetToStartPosition();
        }

        private void ResetToStartPosition()
        {
            TransformData startData = _car.gameElementTransformation.playingStartTransformData;
            _transform.position = startData.position;
            _transform.rotation = Quaternion.Euler(startData.rotation);
            _carRecordData.Reset();
            _canGhostsMove = false;
        }

        public void UpdateState()
        {
            
        }

        public void FixedUpdateState()
        {
            if (_canGhostsMove)
            {
                var nextTransformData = _carRecordData.GetNextTransform();
                if (nextTransformData == null)
                {
                    Exit();
                    return;
                }
                _transform.position = nextTransformData.position;
                _transform.rotation = Quaternion.Euler(nextTransformData.rotation);
            }
        }

        public void Exit()
        {
            SetLastLocation();
            StopGhostDriving();
        }

        private void SetLastLocation()
        {
            _car.gameElementTransformation.idleTransformData = new TransformData(
                _transform.position,
                _transform.rotation.eulerAngles
            );
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