using Windows.Controllers;
using Windows.Interfaces;
using InputSystem.CameraControllers;
using InputSystem.CameraControllers.Interfaces;
using InputSystem.Controllers;
using InputSystem.Interfaces;
using Performance;
using Performance.Interfaces;
using Player;
using Player.Controllers;
using Player.Interfaces;
using Production.Controllers;
using Production.Interfaces;
using Resource.Controllers;
using Resource.Interfaces;
using UnityEngine;
using Zenject;

namespace GameMain.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ProductionBuildingController m_ProductionBuildingController;
        [SerializeField] private PlayerAnimationController m_PlayerAnimationController;
        [SerializeField] private ResourceController m_ResourceController;
        [SerializeField] private WindowsController m_WindowsController;
        [SerializeField] private InputController m_InputController;
        [SerializeField] private PlayerInventory m_PlayerInventory;
        [SerializeField] private CameraMover m_CameraMover;
        [SerializeField] private PlayerMover m_PlayerMover;
        [SerializeField] private PerformanceOptimizer m_PerformanceOptimizer;
        [SerializeField] private Camera m_Camera;

        public override void InstallBindings()
        {
            Container.Bind<IProductionBuildingController>().To<ProductionBuildingController>().FromInstance(m_ProductionBuildingController).AsSingle();
            Container.Bind<ICharacterAnimationController>().To<PlayerAnimationController>().FromInstance(m_PlayerAnimationController).AsSingle();
            Container.Bind<IResourceController>().To<ResourceController>().FromInstance(m_ResourceController).AsSingle();
            Container.Bind<IPlayerInventory>().To<PlayerInventory>().FromInstance(m_PlayerInventory).AsSingle();
            Container.Bind<ICameraMover>().To<CameraMover>().FromInstance(m_CameraMover).AsSingle();
            Container.Bind<Camera>().FromInstance(m_Camera).AsSingle();
            Container.Bind<IPerformanceOptimizer>().To<PerformanceOptimizer>().FromInstance(m_PerformanceOptimizer).AsSingle();
            
            Container.BindInterfacesAndSelfTo<WindowsController>().FromInstance(m_WindowsController).AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenTapController>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<InputController>().FromInstance(m_InputController).AsSingle();
        }
    }
}
