using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public enum RessourceType
    {
        COAL,
        STONE,
        IRON
    }
    public class InventoryComponent : Component
    {
        public int amount;
        public int maxCapacity;
        public RessourceType ressourceType = RessourceType.COAL;
    }
}
