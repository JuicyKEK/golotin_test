using Player;
using Production.View;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Production.Controllers
{
    public class ProductionBuilding : MonoBehaviour, IPlayerInteract
    {
        public string ResourceName => m_ResourceName;
        public int ResourceCount => m_ResourceCount;
        
        [SerializeField] private ProductionBuildingView m_ProductionBuildingView;

        private UnityAction m_Action;
        private string m_ResourceName;
        private int m_ResourceCount;
        
        public void Init(string name, UnityAction action, int count = 0)
        {
            m_ResourceName = name;
            m_ProductionBuildingView.Init(name, count);
            m_Action = action;
            m_ResourceCount = count;
        }

        public void SetResourceCount(int count)
        {
            m_ResourceCount = count;
            m_ProductionBuildingView.SetCount(count);
        }

        public int GetResourceCount()
        {
            return m_ResourceCount;
        }
        
        public void Action()
        {
            m_Action?.Invoke();
        }

    }
}