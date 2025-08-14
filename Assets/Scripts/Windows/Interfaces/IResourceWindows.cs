using UnityEngine.Events;

namespace Windows.Interfaces
{
    public interface IResourceWindowsShow
    {
        void ShowWindows(string resourceName, int resourceCurrent, int resourceCountAdd, UnityAction onClosed);
    }
}