using Player;
using UnityEngine;

namespace Production.Controllers
{
    public class ProductionBuilding : MonoBehaviour, IPlayerInteract
    {
        public void Action()
        {
            Debug.Log("Action");
        }
    }
}