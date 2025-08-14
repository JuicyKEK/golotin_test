using TMPro;
using UnityEngine;

namespace Production.View
{
    public class ProductionBuildingView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_ResourceName;
        [SerializeField] private TMP_Text m_ResourceCount;
        
        public void Init(string name, int count)
        {
            SetName(name);
            SetCount(count);
        }
        
        public void SetCount(int count)
        {
            m_ResourceCount.text = count.ToString();
        }
        
        private void SetName(string name)
        {
            m_ResourceName.text = name;
        }
    }
}