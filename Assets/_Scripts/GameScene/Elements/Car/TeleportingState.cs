using UnityEngine;

namespace GameScene
{
    public class TeleportingState : IState
    {
        private Car _car;
        private Flag _destinationFlag;
        private Vector3 startPos,flagStartPos;
        private Vector3 endPos,flagEndPos;
        private Quaternion startQ,flagStartQ;
        private Quaternion endQ,flagEndQ;
        private float _duration = 1f;
        private float _elapsedTime;
        private bool _timerActive;
        
        public TeleportingState(Car car, Flag destinationFlag)
        {
            _car = car;
            _destinationFlag = destinationFlag;
        }
        public void Enter()
        {
            TransformData idleData = _car.gameElementTransformation.idleTransformData;
            TransformData startData = _car.gameElementTransformation.playingStartTransformData;
            
            startPos = idleData.position;
            startQ =  Quaternion.Euler(idleData.rotation);
            endQ = Quaternion.Euler(startData.rotation);
            endPos = startData.position;

            TransformData flagIdleData = _destinationFlag.gameElementTransformation.idleTransformData;
            TransformData flagStartData = _destinationFlag.gameElementTransformation.playingStartTransformData;
            
            flagStartPos = flagIdleData.position;
            flagStartQ =  Quaternion.Euler(flagIdleData.rotation);
            flagEndQ = Quaternion.Euler(flagStartData.rotation);
            flagEndPos = flagStartData.position;
            

            StartTimer();
            _timerActive = true;
        }

        public void UpdateState()
        {
            if (_timerActive)
            {
                _car.transform.position = Vector3.Lerp(startPos, endPos, _elapsedTime/_duration);
                _car.transform.rotation = Quaternion.Lerp(startQ, endQ, _elapsedTime/_duration);
                
                _destinationFlag.transform.position = Vector3.Lerp(flagStartPos, flagEndPos, _elapsedTime/_duration);
                _destinationFlag.transform.rotation = Quaternion.Lerp(flagStartQ, flagEndQ, _elapsedTime/_duration);
                
                _elapsedTime += Time.deltaTime;
            }
        
            if (_elapsedTime > _duration)
            {
                StopTimer();
                _car.ChangeState(_car.drivingState);
            }
        }

        public void FixedUpdateState()
        {
            
        }

        public void Exit()
        {
            
        }
        
        void StartTimer()
        {
            _timerActive = true;
            _elapsedTime = 0;
        }

        void StopTimer()
        {
            _timerActive = false;
        }
    }
}