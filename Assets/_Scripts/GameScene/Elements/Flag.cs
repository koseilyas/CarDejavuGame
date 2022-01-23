using UnityEngine;

namespace GameScene
{
    public class Flag : MonoBehaviour
    {
        public GameElementTransformation gameElementTransformation;
        [SerializeField] private Collider2D _collider;
        
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

        public void Reached()
        {
            gameObject.SetActive(false);
        }
    }
}