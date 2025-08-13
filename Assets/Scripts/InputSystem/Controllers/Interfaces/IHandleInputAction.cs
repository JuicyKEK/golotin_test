using UnityEngine;

namespace InputSystem.Interfaces
{
    public interface IHandleInputAction
    {
        Vector3 IsTouchStart();
        Vector3 IsTouchEnd();
        Vector3 IsDrag();
        float IsScroll();
    }
}