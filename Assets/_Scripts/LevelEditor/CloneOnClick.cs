#if UNITY_EDITOR
using UnityEngine;

namespace LevelEditor
{
    public class CloneOnClick : MonoBehaviour
    {

        private Camera _cam;
        private GameObject _prefab;
        private Transform _parent;

        void Awake() {
            _cam = Camera.main;
        }

        public void Initialize(GameObject prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        private void OnMouseDown()
        {
            Spawn();
        }

        public void Spawn()
        {
            GameObject obstacle = Instantiate(_prefab, _parent);
            obstacle.transform.position = Vector3.zero;
            if (obstacle.TryGetComponent(out CloneOnClick cloneOnClick))
            {
                Destroy(cloneOnClick);
            }

            DraggableObject draggableObject = obstacle.AddComponent<DraggableObject>();
        }
    }
}
#endif
