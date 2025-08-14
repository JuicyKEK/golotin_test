using UnityEngine.Events;

namespace Windows.Interfaces
{
    public interface IResourceWindowsShow
    {
        void ShowResourceWindows(string resourceName, int resourceCurrent, int resourceCountAdd, UnityAction onClosed);
    }
}