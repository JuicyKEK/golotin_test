using InputSystem.CameraControllers;
using InputSystem.CameraControllers.Interfaces;
using InputSystem.Controllers;
using InputSystem.Interfaces;
using Player;
using Player.Controllers;
using Player.Interfaces;
using Production.Controllers;
using Production.Interfaces;
using UnityEngine;
using Zenject;

namespace GameMain.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Controllers")]
        [SerializeField] private ProductionBuildingController m_ProductionBuildingController;
        [SerializeField] private PlayerAnimationController m_PlayerAnimationController;
        [SerializeField] private InputController m_InputController;
        [SerializeField] private CameraMover m_CameraMover;
        [SerializeField] private PlayerMover m_PlayerMover;
        
        [Header("Camera")]
        [SerializeField] private Camera m_Camera;

        public override void InstallBindings()
        {
            Container.Bind<IProductionBuildingController>().To<ProductionBuildingController>().FromInstance(m_ProductionBuildingController).AsSingle();
            Container.Bind<ICharacterAnimationController>().To<PlayerAnimationController>().FromInstance(m_PlayerAnimationController).AsSingle();
            Container.Bind<ICameraMover>().To<CameraMover>().FromInstance(m_CameraMover).AsSingle();
            Container.Bind<Camera>().FromInstance(m_Camera).AsSingle();
            
            Container.BindInterfacesAndSelfTo<ScreenTapController>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<InputController>().FromInstance(m_InputController).AsSingle();
        }
    }
}
