
using System;
using UnityEngine;

namespace GameScene
{
    public class DrivingState : IState
    {
        private Car _car;
        private Rigidbody2D _rb;
        private CarRecordData _carRecordData;
        private SpriteRenderer _spriteRenderer;
        private Sprite _greenCar;
        private float _startMoveTime = 1f;
        private bool _canMove = false;
        private int _rotateDirection ;
        private int _rotatePower = 4;
        private int _velocity = 3;
        private Transform _transform;
        
        public static event Action OnDriveStarted;

        public DrivingState(Car car, Rigidbody2D rb, CarRecordData carRecordData, SpriteRenderer spriteRenderer, Sprite greenCar)
        {
            _car = car;
            _rb = rb;
            _transform = _rb.transform;
            _carRecordData = carRecordData;
            _spriteRenderer = spriteRenderer;
            _greenCar = greenCar;
            InputManager.OnRotate += RotateInput;
        }
        
        public void Enter()
        {
            _spriteRenderer.sprite = _greenCar;
            _canMove = false;
            
        }

        private void RotateInput(bool left, bool right)
        {
            if(!_canMove)
                OnDriveStarted?.Invoke();
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
                MoveFunction();
                RecordTransformStep();
            }
        }

        private void MoveFunction()
        {
            _rb.velocity = _rb.transform.up * _velocity;
            _rb.rotation += _rotateDirection * _rotatePower;
        }
        
        private void RecordTransformStep()
        {
            _carRecordData.AddRecord(new TransformData(_transform.position, _transform.rotation.eulerAngles));
        }

        public void Exit()
        {
            ResetDriveAbility();
        }

        private void ResetIdlePosition()
        {
            _car.gameElementTransformation.idleTransformData = new TransformData(
                _transform.position,
                _transform.rotation.eulerAngles
            );
        }

        private void ResetDriveAbility()
        {
            ResetIdlePosition();
            _canMove = false;
            _rb.velocity = Vector2.zero;
            TransformData startData = _car.gameElementTransformation.playingStartTransformData;
            _transform.position = startData.position;
            _transform.rotation = Quaternion.Euler(startData.rotation);
            _rb.isKinematic = true;
        }
    }
}