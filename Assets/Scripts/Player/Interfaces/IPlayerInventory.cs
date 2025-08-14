using System.Collections.Generic;

namespace Player.Interfaces
{
    public interface IPlayerInventory
    {
        void AddResource(string resourceName, int amount);
        int GetResourceCount(string resourceName);
        Dictionary<string, int> GetAllResources();
    }
}