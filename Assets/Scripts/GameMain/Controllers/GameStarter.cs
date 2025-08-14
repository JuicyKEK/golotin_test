using InputSystem.Controllers;
using InputSystem.Interfaces;
using Production.Interfaces;
using UnityEngine;
using Zenject;

namespace GameMain.Controllers
{
    public class GameStarter : MonoBehaviour
    {
        [Inject] private IProductionBuildingController m_ProductionBuildingController;
        [Inject] private InputController m_InputController;
        [Inject] private ICameraMover m_CameraMover;

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            m_InputController.Init();
            m_ProductionBuildingController.Init();
            m_CameraMover.Init();
        }
    }
}

