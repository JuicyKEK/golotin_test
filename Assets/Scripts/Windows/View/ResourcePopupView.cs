using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Windows.View
{
    public class ResourcePopupView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_ResourceName;
        [SerializeField] private TMP_Text m_ResourceCount;
        [SerializeField] private Button m_CloseButton;
        
        public void Show(string resourceName, int resourceCurrent, int resourceAdd, UnityAction closeAction)
        {
            gameObject.SetActive(true);
            m_ResourceName.text = resourceName;
            m_ResourceCount.text = resourceCurrent + " + " + resourceAdd;
            m_CloseButton.onClick.RemoveAllListeners();
            m_CloseButton.onClick.AddListener(closeAction);
        }

        public void CloseWindow()
        {
            gameObject.SetActive(false);
        }
    }
}