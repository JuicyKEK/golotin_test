using System.Collections.Generic;
using Resource.Interfaces;
using Resource.View;
using UnityEngine;

namespace Resource.Controllers
{
    public class ResourcesViewController : MonoBehaviour, IResourcesViewAdd
    {
        [SerializeField] private ResourcesView m_ResourcesViewPrefab;
        
        private Dictionary<string, ResourcesView> m_Resources = new Dictionary<string, ResourcesView>();
        
        public void UpdateResourceView(string resourceName, int amountAdd)
        {
            if (!m_Resources.ContainsKey(resourceName))
            {
                SpawnResourceView(resourceName);
            }
            
            m_Resources[resourceName].SetCount(amountAdd);
        }
        
        private void SpawnResourceView(string resourceName)
        {
            var resourceView = Instantiate(m_ResourcesViewPrefab, transform);
            resourceView.gameObject.SetActive(true);
            resourceView.SetName(resourceName);
            m_Resources.Add(resourceName, resourceView);
        }
    }
}