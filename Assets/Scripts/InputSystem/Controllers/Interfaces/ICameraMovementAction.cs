using UnityEngine;
using UnityEngine.Events;

namespace InputSystem.Interfaces
{
    public interface ICameraMovementAction
    {
        void OnTouchStartSubscribe(UnityAction<Vector3> touchStartAction);
        void OnTouchEndSubscribe(UnityAction<Vector3> touchEndAction);
        void OnDragSubscribe(UnityAction<Vector3> touchDragAction);
        void OnScrollSubscribe(UnityAction<float> scrollAction);
    }
}