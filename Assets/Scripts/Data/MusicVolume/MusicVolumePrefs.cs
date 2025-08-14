using UnityEngine;

namespace Data.MusicVolume
{
    public class MusicVolumePrefs : IMusicVolumePrefs
    {
        private const string KEY_MUSIC_VOLUME = "MusicVolume";
        private float m_DefaultMusicVolume = 1f;

        public float MusicVolume
        {
            get
            {
                return PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME, m_DefaultMusicVolume);
            }
            set
            {
                PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, value);
            }
        }
    }
}