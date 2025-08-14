using InputSystem.Interfaces;
using UnityEngine;

namespace InputSystem.Controllers
{
    public class TouchHandleInputAction : IHandleInputAction
    {
        private const float ZoomSpeedCoefficient = 0.01f;
        
        private TouchPhase m_LastTouchPhase = TouchPhase.Canceled;
        private Vector3 m_LastTouchPosition = Vector3.zero;
        private bool m_HasValidTouch = false;
        
        public Vector3 IsTouchStart()
        {
            if (!UpdateTouchState()) return Vector3.zero;
            
            return m_LastTouchPhase == TouchPhase.Began ? m_LastTouchPosition : Vector3.zero;
        }

        public Vector3 IsTouchEnd()
        {
            if (!UpdateTouchState()) return Vector3.zero;
            
            return m_LastTouchPhase == TouchPhase.Ended ? m_LastTouchPosition : Vector3.zero;
        }

        public Vector3 IsDrag()
        {
            if (!UpdateTouchState()) return Vector3.zero;
            
            return m_LastTouchPhase == TouchPhase.Moved ? m_LastTouchPosition : Vector3.zero;
        }
        
        private bool UpdateTouchState()
        {
            if (Input.touchCount <= 0)
            {
                m_HasValidTouch = false;
                return false;
            }

            Touch touch = Input.GetTouch(0);
            m_LastTouchPhase = touch.phase;
            m_LastTouchPosition = touch.position;
            m_HasValidTouch = true;
            
            return true;
        }

        public float IsScroll()
        {
            if (Input.touchCount == 2)
            {
                return HandlePinchZoom();
            }
            
            return 0f;
        }
        
        private float HandlePinchZoom()
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            
            float currentDistance = Vector2.Distance(touch1.position, touch2.position);
            
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;
            float prevDistance = Vector2.Distance(touch1PrevPos, touch2PrevPos);
            
            float deltaDistance = currentDistance - prevDistance;
            
            return deltaDistance * ZoomSpeedCoefficient * -1;
        }
    }
}