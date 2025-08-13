using UnityEngine;
using UnityEngine.Events;

namespace InputSystem.Interfaces
{
    public interface ICameraTap
    {
        event UnityAction<Vector2> OnTap;
    }
}