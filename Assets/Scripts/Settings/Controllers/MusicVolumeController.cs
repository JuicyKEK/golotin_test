using Data.MusicVolume;
using Settings.Interfaces;
using UnityEngine;

namespace Settings.Controllers
{
    public class MusicVolumeController : MonoBehaviour, IMusicVolume
    {
        [SerializeField] private AudioSource m_AudioSource;
        
        private IMusicVolumePrefs m_MusicVolumePrefs = new MusicVolumePrefs();

        public void OnStart()
        {
            SetAudioSourceVolume();
        }
        
        public void MusicVolumeChange(float volume)
        {
            m_MusicVolumePrefs.MusicVolume = volume;
            SetAudioSourceVolume();
        }

        public float MusicVolumeGet()
        {
            return m_MusicVolumePrefs.MusicVolume;
        }

        private void SetAudioSourceVolume()
        {
            m_AudioSource.volume = m_MusicVolumePrefs.MusicVolume;
        }
    }
}