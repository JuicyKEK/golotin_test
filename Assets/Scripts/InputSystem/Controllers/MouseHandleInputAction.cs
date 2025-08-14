using InputSystem.Interfaces;
using UnityEngine;

namespace InputSystem.Controllers
{
    public class MouseHandleInputAction : IHandleInputAction
    {
        public Vector3 IsTouchStart()
        {
            if (Input.GetMouseButtonDown(0))
            {
                return Input.mousePosition;
            }
            
            return Vector2.zero;
        }

        public Vector3 IsTouchEnd()
        {
            if (Input.GetMouseButtonUp(0))
            {
                return Input.mousePosition;
            }
            
            return Vector2.zero;
        }

        public Vector3 IsDrag()
        {
            if (Input.GetMouseButton(0))
            {
                return Input.mousePosition;
            }
            
            return Vector2.zero;
        }

        public float IsScroll()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scroll) > 0.01f)
            {
                return scroll;
            }
            
            return 0f;
        }
    }
}