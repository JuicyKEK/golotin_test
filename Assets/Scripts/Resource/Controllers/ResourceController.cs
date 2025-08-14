using Windows.Interfaces;
using Player.Interfaces;
using Resource.Interfaces;
using UnityEngine;
using Zenject;

namespace Resource.Controllers
{
    public class ResourceController : MonoBehaviour, IResourceController
    {
        private IResourceWindowsShow m_ResourceWindowsShow;
        private IPlayerInventory m_PlayerInventory;
        
        [Inject]
        public void Construct(IResourceWindowsShow resourceWindowsShow, IPlayerInventory playerInventory)
        {
            m_ResourceWindowsShow = resourceWindowsShow;
            m_PlayerInventory = playerInventory;
        }
        
        public void ShowAddResource(string resourceName, int amountAdd)
        {
            m_ResourceWindowsShow.ShowWindows(resourceName, m_PlayerInventory.GetResourceCount(resourceName),
                amountAdd, () => AddResource(resourceName, amountAdd));
        }

        public void AddResource(string resourceName, int amount)
        {
            m_PlayerInventory.AddResource(resourceName, amount);
        }
    }
}