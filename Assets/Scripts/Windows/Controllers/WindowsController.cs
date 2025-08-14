using Windows.Interfaces;
using Windows.View;
using UnityEngine;
using UnityEngine.Events;

namespace Windows.Controllers
{
    public class WindowsController :  MonoBehaviour, IResourceWindowsShow, IWindowsController
    {
        public bool IsOpenWindow => m_IsOpenWindow;
        
        [SerializeField] private ResourcePopupView m_ResourcePopupView;

        public bool m_IsOpenWindow;
        
        public void ShowWindows(string resourceName, int resourceCurrent, int resourceCountAdd, UnityAction onClosed)
        {
            m_IsOpenWindow = true;
            m_ResourcePopupView.Show(resourceName, resourceCurrent, resourceCountAdd, () => 
            {
                onClosed?.Invoke();
                m_ResourcePopupView.CloseWindow();
                m_IsOpenWindow = false;
            });
        }
    }
}