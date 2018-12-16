using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public enum ResourceType
    {
        COAL,
        STONE,
        IRON
    }
    public class InventoryComponent : Component
    {
        public int amount;
        public int maxCapacity;
        public ResourceType resourceType = ResourceType.COAL;
    }
}
