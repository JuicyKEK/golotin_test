using System.Collections.Generic;
using System.Collections;
using Data.SO;
using Production.Interfaces;
using Resource.Interfaces;
using UnityEngine;
using Zenject;

namespace Production.Controllers
{
    public class ProductionBuildingController : MonoBehaviour, IProductionBuildingController
    {
        private const float UPDATE_TIMER_INTERVAL = 0.2f;
        
        [SerializeField] private List<ProductionBuilding> m_ProductionBuildings;
        [SerializeField] private ProductionBuildingsSO m_ProductionBuildingsData;
        
        private Dictionary<string, ProductionBuilding> m_BuildingsByResourceName;
        private Dictionary<string, float> m_ResourceTimers;
        private Dictionary<string, ProductionBuildingData> m_BuildingDataByResourceName;
        private Coroutine m_MainTimerCoroutine;
        private List<string> m_CachedResourceNames;
        private bool m_IsInitialized = false;
        
        private IResourceController m_ResourceController;

        [Inject]
        public void Construct(IResourceController resourceController)
        {
            m_ResourceController = resourceController;
        }
        
        public void Init()
        {
            InitializeBuildingsDictionary();
            CreateProductionBuildings();
            StartMainTimer();
        }

        private void InitializeBuildingsDictionary()
        {
            int buildingCount = m_ProductionBuildingsData.ProductionBuildings.Count;
            m_BuildingsByResourceName = new Dictionary<string, ProductionBuilding>(buildingCount);
            m_ResourceTimers = new Dictionary<string, float>(buildingCount);
            m_BuildingDataByResourceName = new Dictionary<string, ProductionBuildingData>(buildingCount);
        }

        private void StartMainTimer()
        {
            foreach (var buildingData in m_ProductionBuildingsData.ProductionBuildings)
            {
                if (m_BuildingsByResourceName.ContainsKey(buildingData.ResourceName))
                {
                    m_ResourceTimers[buildingData.ResourceName] = buildingData.SecondsBetweenAddResource;
                    m_BuildingDataByResourceName[buildingData.ResourceName] = buildingData;
                }
            }
            
            m_CachedResourceNames = new List<string>(m_ResourceTimers.Keys);
            
            m_MainTimerCoroutine = StartCoroutine(MainTimer());
        }

        private IEnumerator MainTimer()
        {
            var waitForSeconds = new WaitForSeconds(UPDATE_TIMER_INTERVAL);
            
            while (true)
            {
                yield return waitForSeconds;
                
                for (int i = 0; i < m_CachedResourceNames.Count; i++)
                {
                    string resourceName = m_CachedResourceNames[i];
                    m_ResourceTimers[resourceName] -= UPDATE_TIMER_INTERVAL;
                    
                    if (m_ResourceTimers[resourceName] <= 0f)
                    {
                        if (m_BuildingsByResourceName.TryGetValue(resourceName, out var building) &&
                            m_BuildingDataByResourceName.TryGetValue(resourceName, out var buildingData))
                        {
                            var currentCount = building.GetResourceCount();
                            building.SetResourceCount(currentCount + buildingData.ResourceAddCount);
                            m_ResourceTimers[resourceName] = buildingData.SecondsBetweenAddResource;
                        }
                    }
                }
            }
        }

        private void CreateProductionBuildings()
        {
            for (int i = 0; i < m_ProductionBuildingsData.ProductionBuildings.Count; i++)
            {
                if(m_ProductionBuildings.Count <= i)
                {
                    Debug.LogError("ProductionBuildings on scene is less than in ProductionBuildingsData");
                    return;
                }
                
                var resourceName = m_ProductionBuildingsData.ProductionBuildings[i].ResourceName;
                int index = i;
                m_ProductionBuildings[i].Init(resourceName, () => OnBuildingInteract(index));
                m_BuildingsByResourceName[resourceName] = m_ProductionBuildings[i];
            }
            
            if(m_ProductionBuildings.Count > m_ProductionBuildingsData.ProductionBuildings.Count)
            {
                Debug.LogError("ProductionBuildingsData is less than in ProductionBuildings on scene");
            }
        }

        private void OnBuildingInteract(int buildingIndex)
        {
            m_ResourceController.ShowAddResource(m_ProductionBuildings[buildingIndex].ResourceName,
                m_ProductionBuildings[buildingIndex].ResourceCount);
            m_ProductionBuildings[buildingIndex].SetResourceCount(0);
        }

        private void OnDestroy()
        {
            if (m_MainTimerCoroutine != null)
            {
                StopCoroutine(m_MainTimerCoroutine);
                m_MainTimerCoroutine = null;
            }
        }
    }
}