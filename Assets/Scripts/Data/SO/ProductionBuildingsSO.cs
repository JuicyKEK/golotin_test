using System.Collections.Generic;
using UnityEngine;

namespace Data.SO
{
    [CreateAssetMenu(fileName = "ProductionBuildingsSO", menuName = "Production/ProductionBuildingsSO")]
    public class ProductionBuildingsSO : ScriptableObject
    {
        public List<ProductionBuildingData> ProductionBuildings;
    }
}