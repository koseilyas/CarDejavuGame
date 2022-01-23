
using System;
using Unity.Mathematics;
using UnityEngine;

namespace GameScene
{
    public class DrivingState : IState
    {
        public static event Action OnDriveStarted;
        private Car _car;
        private Rigidbody2D _rb;
        private CarRecordData _carRecordData;
        private float _startMoveTime = 1f;
        private bool _canMove = false;
        private int _rotateDirection ;
        private int _rotatePower = 4;
        private int _velocity = 3;
        private Transform _transform;

        public DrivingState(Car car, Rigidbody2D rb, CarRecordData carRecordData)
        {
            _car = car;
            _rb = rb;
            _transform = _rb.transform;
            _carRecordData = carRecordData;
        }
        public void Enter()
        {
            InputManager.OnRotate += RotateInput;
            OnDriveStarted?.Invoke();
        }

        private void RotateInput(bool left, bool right)
        {
            _canMove = true;
            if (left && !right)
                _rotateDirection = 1;
            else if (right && !left)
                _rotateDirection = -1;
            else
                _rotateDirection = 0;
            
        }

        public void UpdateState()
        {
            
        }

        public void FixedUpdateState()
        {
            if (_canMove)
            {
                _rb.velocity = _rb.transform.up * _velocity;
                _rb.rotation += _rotateDirection * _rotatePower;
                _carRecordData.AddRecord(new TransformData(_transform.position,_transform.rotation.eulerAngles));
            }

        }

        public void Exit()
        {
            InputManager.OnRotate -= RotateInput;
            _rb.velocity = Vector2.zero;
            TransformData startData = _car.gameElementTransformation.playingStartTransformData;
            _transform.position = startData.position;
            _transform.rotation = quaternion.Euler(startData.rotation);
            _rb.isKinematic = true;
        }
    }
}