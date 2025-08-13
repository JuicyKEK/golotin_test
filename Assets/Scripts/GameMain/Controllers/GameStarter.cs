using InputSystem.CameraControllers;
using InputSystem.Controllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameMain.Controllers
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private InputController m_InputController;
        [SerializeField] private CameraMover m_CameraMover;
        [SerializeField] private Camera m_Camera;
        
        private void Start()
        {
            m_InputController.Init();
            m_CameraMover.Init(m_Camera, m_InputController);
        }
    }
}