using System.Collections.Generic;
using Player.Interfaces;
using UnityEngine;

namespace Player.Controllers
{
    public class PlayerInventory : MonoBehaviour, IPlayerInventory
    {
        private Dictionary<string, int> m_Resources = new Dictionary<string, int>();
    
        public void AddResource(string resourceName, int amount)
        {
            if (m_Resources.ContainsKey(resourceName))
                m_Resources[resourceName] += amount;
            else
                m_Resources[resourceName] = amount;
        }

        public Dictionary<string, int> GetAllResources()
        {
            return m_Resources;
        }
        
        public int GetResourceCount(string resourceName)
        {
            return m_Resources.ContainsKey(resourceName) ? m_Resources[resourceName] : 0;
        }
    }
}