using System.Collections.Generic;
using UnityEngine;

namespace Player.Controllers
{
    public class PlayerInventory : MonoBehaviour
    {
        private Dictionary<string, int> m_Resources = new Dictionary<string, int>();
    
        public void AddResource(string resourceName, int amount)
        {
            if (m_Resources.ContainsKey(resourceName))
                m_Resources[resourceName] += amount;
            else
                m_Resources[resourceName] = amount;
        }
    
        public int GetResourceCount(string resourceName)
        {
            return m_Resources.ContainsKey(resourceName) ? m_Resources[resourceName] : 0;
        }
    }
}