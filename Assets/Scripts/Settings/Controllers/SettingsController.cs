using Windows.Interfaces;
using Settings.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Settings.Controllers
{
    public class SettingsController : MonoBehaviour
    {
        [SerializeField] private Button m_SettingsButton;
        
        private ISettingsWindowsShow m_SettingsWindowsShow;
        private IMusicVolume m_MusicVolume;
        
        [Inject]
        public void Construction(IMusicVolume musicVolume, ISettingsWindowsShow settingsWindowsShow)
        {
            m_SettingsWindowsShow = settingsWindowsShow;
            m_MusicVolume = musicVolume;
            m_SettingsButton.onClick.AddListener(OpenSettingsWindow);
            m_MusicVolume.OnStart();
        }

        private void OpenSettingsWindow()
        {
            m_SettingsWindowsShow.ShowSettingsWindows(m_MusicVolume);
        }
    }
}