using System;
using UnityEngine;

namespace GameScene
{
    public class InputManager : MonoBehaviour
    {
        public static event Action<bool,bool> OnRotate;
        private bool _leftPressed, _rightPressed;
        public float halfScreenWidth = Screen.width/2f;
        private void Update()
        {
            #if UNITY_STANDALONE || UNITY_EDITOR
                CheckKeyboardInput();
            #elif UNITY_ANDROID || UNITY_IOS
                CheckTouchInput();
            #endif
        }

        private void CheckKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _leftPressed = false;
                _rightPressed = true;
                OnRotate?.Invoke(_leftPressed,_rightPressed);
            }else if (Input.GetKeyDown(KeyCode.A))
            {
                _rightPressed = false;
                _leftPressed = true;
                OnRotate?.Invoke(_leftPressed,_rightPressed);
            }
            
            if (Input.GetKeyUp(KeyCode.D))
            {
                _rightPressed = false;
                OnRotate?.Invoke(_leftPressed,_rightPressed);
            }else if (Input.GetKeyUp(KeyCode.A))
            {
                _leftPressed = false;
                OnRotate?.Invoke(_leftPressed,_rightPressed);
            }
        }

        private void CheckTouchInput()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.position.x < halfScreenWidth)
                {
                    OnRotate?.Invoke(_leftPressed,_rightPressed);
                }
                else if (touch.position.x > halfScreenWidth)
                {
                    OnRotate?.Invoke(_leftPressed,_rightPressed);
                }
            }
        }
    }
}