using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Windows.View
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button m_CloseButton;
        [SerializeField] private Slider m_SettingsPanel;
        public void Show(UnityAction onClosed)
        {
            gameObject.SetActive(true);
            m_CloseButton.onClick.RemoveAllListeners();
            m_CloseButton.onClick.AddListener(onClosed);
        }

        public void SetSlider(float value, UnityAction<float> onChangeSlider)
        {
            m_SettingsPanel.value = value;
            m_SettingsPanel.onValueChanged.RemoveAllListeners();
            m_SettingsPanel.onValueChanged.AddListener(onChangeSlider);
        }
        
        public void CloseWindow()
        {
            gameObject.SetActive(false);
        }
    }
}