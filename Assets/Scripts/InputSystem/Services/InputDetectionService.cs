using InputSystem.Services.Interfaces;
using UnityEngine;

namespace InputSystem.Services
{
    public class InputDetectionService : IInputDetectionService
    {
        public bool IsTouchSupported()
        {
#if UNITY_EDITOR
            return false;
#elif UNITY_ANDROID || UNITY_IOS 
            return true;
#elif UNITY_WEBGL && !UNITY_EDITOR
            return Input.touchSupported;
#else
            return Input.touchSupported && Application.isMobilePlatform;
#endif
        }
    }
}