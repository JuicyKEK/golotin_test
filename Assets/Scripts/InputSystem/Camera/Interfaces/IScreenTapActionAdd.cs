using UnityEngine;
using UnityEngine.Events;

namespace InputSystem.CameraControllers.Interfaces
{
    public interface IScreenTapActionAdd
    {
        void OnScreenTapSubscribe(UnityAction<Vector3> tapAction);
    }
}