using System;
using System.Collections;
using UnityEngine;

namespace GameScene
{
    public class Car : StateMachineParent , IDeadly
    {
        private Flag _destinationFlag;
        private CarRecordData _carRecordData;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Collider2D _collider;
        private static int _currentCarIndex;
        
        public GameElementTransformation gameElementTransformation;
        public static event Action<int> OnCarStateReset;
        public GhostState ghostState;
        public DrivingState drivingState;
        public IdleState idleState;
        public RestartState restartState;
        public TeleportingState teleportingState;

        
        private void Awake()
        {
            _carRecordData = new CarRecordData();
            InitializeStates();
        }

        private void OnEnable()
        {
            DrivingState.OnDriveStarted += EnableCollision;
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
                    _carRecordData.Restart();
                    OnCarStateReset?.Invoke(_currentCarIndex);
                }
                if (col.TryGetComponent(out Flag flag))
                {
                    _currentCarIndex++;
                    flag.Reached();
                    OnCarStateReset?.Invoke(_currentCarIndex);
                }
            }
        }
        
        private void InitializeStates()
        {
            idleState = new IdleState(this);
            drivingState = new DrivingState(this, _rb, _carRecordData);
            ghostState = new GhostState(this, _carRecordData);
            teleportingState = new TeleportingState(this, _destinationFlag);
            restartState = new RestartState(this);
        }

        public void SetReachPoint(Flag flag)
        {
            _destinationFlag = flag;
        }
        
        private void EnableCollision()
        {
            _collider.enabled = true;
        }
        
        private void DisableCollision()
        {
            _collider.enabled = false;
        }
    }
}