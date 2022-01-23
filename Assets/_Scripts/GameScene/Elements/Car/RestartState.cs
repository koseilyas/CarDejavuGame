using UnityEngine;

namespace GameScene
{
    public class RestartState : IState
    {
        private Car _car;
        private Vector3 startPos,flagStartPos;
        private Vector3 endPos,flagEndPos;
        private Quaternion startQ,flagStartQ;
        private Quaternion endQ,flagEndQ;
        private float _duration = 1f;
        private float _elapsedTime;
        private bool _timerActive;
        
        public RestartState(Car car)
        {
            _car = car;
        }
        public void Enter()
        {
            SetStartPosition();
            StartTimer();
        }

        private void SetStartPosition()
        {
            TransformData idleData = _car.gameElementTransformation.idleTransformData;
            TransformData startData = _car.gameElementTransformation.playingStartTransformData;

            startPos = idleData.position;
            startQ = Quaternion.Euler(idleData.rotation);
            endQ = Quaternion.Euler(startData.rotation);
            endPos = startData.position;
        }

        public void UpdateState()
        {
            if (_timerActive)
            {
                Move();
            }
        
            if (_elapsedTime > _duration)
            {
                StopTimer();
                _car.ChangeState(_car.ghostState);
            }
        }

        private void Move()
        {
            _car.transform.position = Vector3.Lerp(startPos, endPos, _elapsedTime / _duration);
            _car.transform.rotation = Quaternion.Lerp(startQ, endQ, _elapsedTime / _duration);

            _elapsedTime += Time.deltaTime;
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