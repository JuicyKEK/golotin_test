namespace Resource.Interfaces
{
    public interface IResourceController 
    {
        void ShowAddResource(string resourceName, int amountAdd);
        void AddResource(string resourceName, int amount);
    }
}