using TMPro;
using UnityEngine;

namespace Resource.View
{
    public class ResourcesView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_ResourceName;
        [SerializeField] private TMP_Text m_ResourceCount;
        
        public void SetName(string resourceName)
        {
            m_ResourceName.text = resourceName;
            m_ResourceCount.text = 0.ToString();
        }
        
        public void SetCount(int resourceCount)
        {
            m_ResourceCount.text = resourceCount.ToString();
        }
    }
}