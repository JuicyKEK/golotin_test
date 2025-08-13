using InputSystem.Interfaces;
using InputSystem.Services;
using InputSystem.Services.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace InputSystem.Controllers
{
    public class InputController : MonoBehaviour, ICameraMovementAction
    {
        private event UnityAction<Vector3> OnTouchStart;
        private event UnityAction<Vector3> OnTouchEnd;
        private event UnityAction<Vector3> OnDrag;
        private event UnityAction<float> OnScroll;
        
        private IHandleInputAction m_HandleInputAction;
        private IInputDetectionService m_InputDetectionService;
        
        public void Init()
        {
            m_InputDetectionService = new InputDetectionService();
            
            m_HandleInputAction = m_InputDetectionService.IsTouchSupported() 
                ? new TouchHandleInputAction() 
                : new MouseHandleInputAction();
        }
        
        private void Update()
        {
            if (m_HandleInputAction.IsTouchStart() != Vector3.zero)
            {
                OnTouchStart?.Invoke(m_HandleInputAction.IsTouchStart());
            }
            
            if (m_HandleInputAction.IsTouchEnd() != Vector3.zero)
            {
                OnTouchEnd?.Invoke(m_HandleInputAction.IsTouchEnd());
            }
            
            if (m_HandleInputAction.IsDrag() != Vector3.zero)
            {
                OnDrag?.Invoke(m_HandleInputAction.IsDrag());
            }
            
            if (m_HandleInputAction.IsScroll() != 0)
            {
                OnScroll?.Invoke(m_HandleInputAction.IsScroll());
            }
        }
        
        public void OnTouchStartSubscribe(UnityAction<Vector3> touchStartAction)
        {
            OnTouchStart += touchStartAction;
        }

        public void OnTouchEndSubscribe(UnityAction<Vector3> touchEndAction)
        {
            OnTouchEnd += touchEndAction;
        }

        public void OnDragSubscribe(UnityAction<Vector3> touchDragAction)
        {
            OnDrag += touchDragAction;
        }

        public void OnScrollSubscribe(UnityAction<float> scrollAction)
        {
            OnScroll += scrollAction;
        }

        private void OnDestroy()
        {
            OnTouchStart = null;
            OnTouchEnd = null;
            OnDrag = null;
            OnScroll = null;
        }
    }
}