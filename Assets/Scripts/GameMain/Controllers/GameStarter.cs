using InputSystem.CameraControllers;
using InputSystem.Controllers;
using Player.Controllers;
using UnityEngine;

namespace GameMain.Controllers
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationController m_PlayerAnimationController;
        [SerializeField] private InputController m_InputController;
        [SerializeField] private CameraMover m_CameraMover;
        [SerializeField] private PlayerMover m_PlayerMover;
        [SerializeField] private Camera m_Camera;

        private ScreenTapController m_ScreenTapController = new ScreenTapController();
        
        private void Start()
        {
            m_InputController.Init();
            m_CameraMover.Init(m_Camera, m_InputController, m_ScreenTapController);
            m_PlayerMover.Init(m_Camera, m_ScreenTapController, m_PlayerAnimationController);
        }
    }
}