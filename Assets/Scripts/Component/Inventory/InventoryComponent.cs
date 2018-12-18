using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public enum ResourceType
    {
        FOOD,
        STONE
    }
    public class InventoryComponent : Component
    {
        public int amount;
        public int maxCapacity;
        public ResourceType resourceType = ResourceType.FOOD;
    }
}
