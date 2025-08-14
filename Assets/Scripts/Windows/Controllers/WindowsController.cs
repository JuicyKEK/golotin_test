using Windows.Interfaces;
using Windows.View;
using Settings.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Windows.Controllers
{
    public class WindowsController :  MonoBehaviour, IResourceWindowsShow, IWindowsController, ISettingsWindowsShow
    {
        public bool IsOpenWindow => m_IsOpenWindow;
        
        [SerializeField] private ResourcePopupView m_ResourcePopupView;
        [SerializeField] private SettingsView m_SettingsView;

        private bool m_IsOpenWindow;
        
        public void ShowResourceWindows(string resourceName, int resourceCurrent, int resourceCountAdd, UnityAction onClosed)
        {
            m_IsOpenWindow = true;
            m_ResourcePopupView.Show(resourceName, resourceCurrent, resourceCountAdd, () => 
            {
                onClosed?.Invoke();
                m_ResourcePopupView.CloseWindow();
                m_IsOpenWindow = false;
            });
        }

        public void ShowSettingsWindows(IMusicVolume musicVolume, UnityAction onClosed = null)
        {
            m_IsOpenWindow = true;
            m_SettingsView.SetSlider(musicVolume.MusicVolumeGet(), musicVolume.MusicVolumeChange);
            m_SettingsView.Show(() => 
            {
                onClosed?.Invoke();
                m_SettingsView.CloseWindow();
                m_IsOpenWindow = false;
            });
        }
    }
}