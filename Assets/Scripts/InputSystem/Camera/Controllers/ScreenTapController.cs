using UnityEngine;
using UnityEngine.Events;
using System;
using InputSystem.CameraControllers.Interfaces;

namespace InputSystem.CameraControllers
{
    public class ScreenTapController : IScreenTapActionAdd, IScreenTapAction, IDisposable
    {
        private event UnityAction<Vector3> m_OnScreenTap;
        
        public void OnScreenTap(Vector3 position)
        {
            m_OnScreenTap?.Invoke(position);
        }
        
        public void OnScreenTapSubscribe(UnityAction<Vector3> tapAction)
        {
            m_OnScreenTap += tapAction;
        }
        
        public void Dispose()
        {
            m_OnScreenTap = null;
        }
    }
}