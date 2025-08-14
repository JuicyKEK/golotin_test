using Settings.Interfaces;
using UnityEngine.Events;

namespace Windows.Interfaces
{
    public interface ISettingsWindowsShow
    {
        void ShowSettingsWindows(IMusicVolume musicVolume, UnityAction onClosed = null);
    }
}