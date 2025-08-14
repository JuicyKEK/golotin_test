using System.Collections;
using InputSystem.Services;
using InputSystem.Services.Interfaces;
using Performance.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Performance
{
    public class PerformanceOptimizer : MonoBehaviour, IPerformanceOptimizer
    {
        [Header("Performance Settings")]
        [SerializeField] private bool m_OptimizeForMobile = true;
        [SerializeField] private int m_TargetFrameRate = 60;
        [SerializeField] private float m_TimeScale = 1f;
        
        [Header("Quality Settings")]
        [SerializeField] private bool m_DisableVSync = true;
        [SerializeField] private bool m_OptimizePhysics = true;
        [SerializeField] private bool m_OptimizeAnimations = true;
        [SerializeField] private bool m_OptimizeRendering = true;
        
        [Header("Memory Management")]
        [SerializeField] private bool m_EnableGarbageCollection = true;
        [SerializeField] private float m_GCInterval = 30f;
        
        private Coroutine m_PerformanceMonitorCoroutine;
        private Coroutine m_GarbageCollectionCoroutine;

        private IInputDetectionService m_InputDetectionService;
        
        public void InitializePerformanceOptimizations()
        {
            if (!IsNeedCurrentOptimization())
            {
                return;
            }
            
            OptimizeApplicationSettings();
            OptimizeQualitySettings();
            OptimizePhysicsSettings();
            OptimizeRenderingSettings();
            
            if (m_EnableGarbageCollection)
            {
                StartGarbageCollectionRoutine();
            }
            
            StartPerformanceMonitoring();
        }

        private bool IsNeedCurrentOptimization()
        {
            if (m_InputDetectionService == null)
            {
                m_InputDetectionService = new InputDetectionService();
            }

            return m_InputDetectionService.IsTouchSupported();
        }
        
        private void OptimizeApplicationSettings()
        {
            Application.targetFrameRate = m_TargetFrameRate;
            
            if (m_DisableVSync && Application.isMobilePlatform)
            {
                QualitySettings.vSyncCount = 0;
            }
            
            Time.timeScale = m_TimeScale;
            Time.fixedDeltaTime = 1f / m_TargetFrameRate;
        }
        
        private void OptimizeQualitySettings()
        {
            if (!m_OptimizeForMobile) return;
            
            QualitySettings.shadows = ShadowQuality.Disable;
            QualitySettings.shadowResolution = ShadowResolution.Low;
            QualitySettings.shadowDistance = 20f;

            QualitySettings.globalTextureMipmapLimit = 1;
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
            
            QualitySettings.antiAliasing = 0;
            
            QualitySettings.maximumLODLevel = 1;
            QualitySettings.particleRaycastBudget = 16;
        }
        
        private void OptimizePhysicsSettings()
        {
            if (!m_OptimizePhysics)
            {
                return;
            }
            
            Physics.defaultSolverIterations = 4;
            Physics.defaultSolverVelocityIterations = 1;

            Physics.queriesHitBackfaces = false;
            Physics.queriesHitTriggers = false;
        }
        
        private void OptimizeRenderingSettings()
        {
            if (!m_OptimizeRendering)
            {
                return;
            }
            
            if (Application.isMobilePlatform)
            {
                Screen.SetResolution(Screen.width / 2, Screen.height / 2, true);
            }
        }
        
        private void StartGarbageCollectionRoutine()
        {
            if (m_GarbageCollectionCoroutine != null)
            {
                StopCoroutine(m_GarbageCollectionCoroutine);
            }
            
            m_GarbageCollectionCoroutine = StartCoroutine(GarbageCollectionRoutine());
        }
        
        private IEnumerator GarbageCollectionRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(m_GCInterval);
                
                System.GC.Collect();
                Resources.UnloadUnusedAssets();
                
                Debug.Log("[Performance] Garbage collection performed");
            }
        }
        
        private void StartPerformanceMonitoring()
        {
            if (m_PerformanceMonitorCoroutine != null)
            {
                StopCoroutine(m_PerformanceMonitorCoroutine);
            }
            
            m_PerformanceMonitorCoroutine = StartCoroutine(PerformanceMonitorRoutine());
        }
        
        private IEnumerator PerformanceMonitorRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(5f);
                
                float currentFPS = 1f / Time.unscaledDeltaTime;
                
                if (currentFPS < m_TargetFrameRate * 0.7f)
                {
                    PerformEmergencyOptimization();
                }
            }
        }
        
        private void PerformEmergencyOptimization()
        {
            Debug.LogWarning("[Performance] Low FPS detected, performing emergency optimization");
            
            QualitySettings.globalTextureMipmapLimit = 2;
            QualitySettings.shadowDistance = 10f;
            
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
        }
        
        private void OnDestroy()
        {
            if (m_PerformanceMonitorCoroutine != null)
            {
                StopCoroutine(m_PerformanceMonitorCoroutine);
            }
            
            if (m_GarbageCollectionCoroutine != null)
            {
                StopCoroutine(m_GarbageCollectionCoroutine);
            }
        }
        
        public void SetTargetFrameRate(int frameRate)
        {
            m_TargetFrameRate = frameRate;
            Application.targetFrameRate = frameRate;
        }
        
        public void EnableMobileOptimizations(bool enable)
        {
            m_OptimizeForMobile = enable;
            if (enable)
            {
                OptimizeQualitySettings();
            }
        }
        
        public void ForceGarbageCollection()
        {
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
        }
    }
}
