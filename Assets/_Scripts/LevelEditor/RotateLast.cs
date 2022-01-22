using System;
using UnityEditor;
using UnityEngine;

namespace LevelEditor
{
    public class RotateLast : MonoBehaviour
    {
        private Transform _lastClicked;
        private Vector3 _rotateSpeed = Vector3.back;
        
        private void OnEnable()
        {
            DraggableObject.OnClicked += SetLastClicked;
        }

        private void OnDisable()
        {
            DraggableObject.OnClicked -= SetLastClicked;
        }
        
        private void SetLastClicked(Transform obj)
        {
            _lastClicked = obj;
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
                Rotate(-1);
            else if (Input.GetKey(KeyCode.E))
                Rotate(1);
        }

        private void Rotate(int direction)
        {
            _lastClicked.Rotate(_rotateSpeed * direction);
        }
    }
}
