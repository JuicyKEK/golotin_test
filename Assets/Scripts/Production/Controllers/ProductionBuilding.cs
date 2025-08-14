using Player;
using Production.View;
using TMPro;
using UnityEngine;

namespace Production.Controllers
{
    public class ProductionBuilding : MonoBehaviour, IPlayerInteract
    {
        [SerializeField] private ProductionBuildingView m_ProductionBuildingView;

        private string m_ResourceName;
        private int m_ResourceCount;
        
        public void Init(string name, int count = 0)
        {
            m_ResourceName = name;
            m_ProductionBuildingView.Init(name, count);
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
            Debug.Log("Action");
        }

    }
}