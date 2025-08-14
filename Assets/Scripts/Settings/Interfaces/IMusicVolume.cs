namespace Settings.Interfaces
{
    public interface IMusicVolume
    {
        void OnStart();
        void MusicVolumeChange(float volume);
        float MusicVolumeGet();
    }
}