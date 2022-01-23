using System;
using System.Collections;
using UnityEngine;

namespace GameScene
{
    public class Car : StateMachineParent , IDeadly
    {
        public GameElementTransformation gameElementTransformation;
        public static event Action<int> OnCarReachedTarget;
        public GhostState ghostState;
        public DrivingState drivingState;
        public IdleState idleState;
        public RestartState restartState;
        public TeleportingState teleportingState;
        private Flag _destinationFlag;
        private CarRecordData _carRecordData;
        private static int _currentCarIndex = 0;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Collider2D _collider;
        

        private void Awake()
        {
            _carRecordData = new CarRecordData();
            idleState = new IdleState(this);
            drivingState = new DrivingState(this,_rb,_carRecordData);
            ghostState = new GhostState(this,_carRecordData);
            teleportingState = new TeleportingState(this,_destinationFlag);
        }

        public void SetReachPoint(Flag flag)
        {
            _destinationFlag = flag;
        }

        private void OnEnable()
        {
            DrivingState.OnDriveStarted += EnableCollision;
        }

        private void EnableCollision()
        {
            _collider.enabled = true;
        }
        
        private void DisableCollision()
        {
            _collider.enabled = false;
        }

        private void OnDisable()
        {
            DrivingState.OnDriveStarted -= EnableCollision;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (CurrentState is DrivingState)
            {
                if (col.TryGetComponent(out IDeadly deadly))
                {
                    ChangeState(teleportingState);
                }
                if (col.TryGetComponent(out Flag flag))
                {
                    _currentCarIndex++;
                    flag.Reached();
                    OnCarReachedTarget?.Invoke(_currentCarIndex);
                }
            }

        }
    }
}