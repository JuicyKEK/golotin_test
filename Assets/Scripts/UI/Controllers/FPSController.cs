using System.Collections;
using TMPro;
using UnityEngine;

namespace CustomUI.Controllers
{
    public class FPSController : MonoBehaviour, IFPSController
    {
        [Header("FPS Display Settings")]
        [SerializeField] private TMP_Text m_FPSText;
        [SerializeField] private float m_UpdateInterval = 1f; 
        [SerializeField] private bool m_ShowFPS = true;
        
        [Header("Color Settings")]
        [SerializeField] private Color m_GoodFPSColor = Color.green;
        [SerializeField] private Color m_MediumFPSColor = Color.yellow;
        [SerializeField] private Color m_BadFPSColor = Color.red;
        [SerializeField] private int m_GoodFPSThreshold = 45;
        [SerializeField] private int m_MediumFPSThreshold = 30;

        // Оптимизация: кэширование для избежания частых вычислений
        private float m_FPSAccumulator = 0f;
        private int m_FrameCount = 0;
        private float m_CurrentFPS = 0f;
        private Coroutine m_FPSUpdateCoroutine;
        private WaitForSeconds m_CachedWaitForSeconds;
        private int m_LastDisplayedFPS = -1;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (m_FPSText == null)
            {
                Debug.LogError("FPS Text component is not assigned!");
                return;
            }
            
            m_CachedWaitForSeconds = new WaitForSeconds(m_UpdateInterval);

            if (m_ShowFPS)
            {
                m_FPSUpdateCoroutine = StartCoroutine(UpdateFPSDisplay());
            }
            else
            {
                m_FPSText.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!m_ShowFPS) return;
            
            float deltaTime = Time.unscaledDeltaTime;
            m_FPSAccumulator += deltaTime;
            m_FrameCount++;
        }

        private IEnumerator UpdateFPSDisplay()
        {
            while (m_ShowFPS)
            {
                yield return m_CachedWaitForSeconds;

                if (m_FrameCount > 0)
                {
                    m_CurrentFPS = m_FrameCount / m_FPSAccumulator;
                    UpdateFPSText();
                    
                    m_FPSAccumulator = 0f;
                    m_FrameCount = 0;
                }
            }
        }

        private void UpdateFPSText()
        {
            if (m_FPSText == null) return;

            int fps = Mathf.RoundToInt(m_CurrentFPS);
            
            if (fps == m_LastDisplayedFPS) return;
            
            m_LastDisplayedFPS = fps;
            m_FPSText.text = $"FPS: {fps}";
            
            Color targetColor = fps >= m_GoodFPSThreshold ? m_GoodFPSColor :
                               fps >= m_MediumFPSThreshold ? m_MediumFPSColor : m_BadFPSColor;
            
            m_FPSText.color = targetColor;
        }

        public void ToggleFPSDisplay()
        {
            m_ShowFPS = !m_ShowFPS;
            
            if (m_ShowFPS)
            {
                m_FPSText.gameObject.SetActive(true);
                if (m_FPSUpdateCoroutine == null)
                {
                    m_FPSUpdateCoroutine = StartCoroutine(UpdateFPSDisplay());
                }
            }
            else
            {
                m_FPSText.gameObject.SetActive(false);
                if (m_FPSUpdateCoroutine != null)
                {
                    StopCoroutine(m_FPSUpdateCoroutine);
                    m_FPSUpdateCoroutine = null;
                }
            }
        }

        public void SetShowFPS(bool show)
        {
            if (m_ShowFPS != show)
            {
                ToggleFPSDisplay();
            }
        }

        private void OnDestroy()
        {
            if (m_FPSUpdateCoroutine != null)
            {
                StopCoroutine(m_FPSUpdateCoroutine);
            }
        }
    }
}
