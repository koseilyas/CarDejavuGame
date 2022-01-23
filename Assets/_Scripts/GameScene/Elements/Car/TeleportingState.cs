using UnityEngine;

namespace GameScene
{
    public class TeleportingState : IState
    {
        private Car _car;
        private Flag _destinationFlag;
        private Vector3 _startPos,_flagStartPos;
        private Vector3 _endPos,_flagEndPos;
        private Quaternion _startQ,_flagStartQ;
        private Quaternion _endQ,_flagEndQ;
        private float _duration = 1f;
        private float _elapsedTime;
        private bool _timerActive;
        private bool _isFlagSetOnce;
        
        public TeleportingState(Car car, Flag destinationFlag)
        {
            _car = car;
            _destinationFlag = destinationFlag;
        }
        public void Enter()
        {
            GetCarTransforms();
            GetFlagTransforms();
            StartTimer();
        }

        private void GetFlagTransforms()
        {
            TransformData flagIdleData = _destinationFlag.gameElementTransformation.idleTransformData;
            TransformData flagStartData = _destinationFlag.gameElementTransformation.playingStartTransformData;

            _flagStartPos = flagIdleData.position;
            _flagStartQ = Quaternion.Euler(flagIdleData.rotation);
            _flagEndQ = Quaternion.Euler(flagStartData.rotation);
            _flagEndPos = flagStartData.position;
        }

        private void GetCarTransforms()
        {
            TransformData idleData = _car.gameElementTransformation.idleTransformData;
            TransformData startData = _car.gameElementTransformation.playingStartTransformData;

            _startPos = idleData.position;
            _startQ = Quaternion.Euler(idleData.rotation);
            _endQ = Quaternion.Euler(startData.rotation);
            _endPos = startData.position;
        }

        public void UpdateState()
        {
            if (_timerActive)
            {
                MoveCar();

                if (!_isFlagSetOnce)
                {
                    MoveFlag();
                }
                
                _elapsedTime += Time.deltaTime;
            }
        
            if (_elapsedTime > _duration)
            {
                StopTimer();
                _isFlagSetOnce = true;
                _car.ChangeState(_car.drivingState);
            }
        }

        private void MoveFlag()
        {
            _destinationFlag.transform.position = Vector3.Lerp(_flagStartPos, _flagEndPos, _elapsedTime / _duration);
            _destinationFlag.transform.rotation = Quaternion.Lerp(_flagStartQ, _flagEndQ, _elapsedTime / _duration);
        }

        private void MoveCar()
        {
            _car.transform.position = Vector3.Lerp(_startPos, _endPos, _elapsedTime / _duration);
            _car.transform.rotation = Quaternion.Lerp(_startQ, _endQ, _elapsedTime / _duration);
        }

        public void FixedUpdateState()
        {
            
        }

        public void Exit()
        {
            
        }
        
        void StartTimer()
        {
            _elapsedTime = 0;
            _timerActive = true;
        }

        void StopTimer()
        {
            _timerActive = false;
        }
    }
}