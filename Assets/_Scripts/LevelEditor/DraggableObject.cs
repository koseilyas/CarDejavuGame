#if UNITY_EDITOR
using System;
using UnityEngine;

namespace LevelEditor
{
    public class DraggableObject : MonoBehaviour
    {

        public static event Action<Transform> OnClicked;
        private Camera _cam;
        private float _speed = 30;

        void Awake() {
            _cam = Camera.main;
        }

        private void OnMouseDown()
        {
            OnClicked?.Invoke(transform);
            EditorLogger.Log(name,false);
        }

        void OnMouseDrag()
        {
            transform.position = GetMousePos();
        }

        Vector3 GetMousePos() {
            Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            return mousePos;
        }

        
    }
}
#endif