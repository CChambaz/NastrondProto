using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public enum ResourceType
    {
        NULL,
        FOOD,
        STONE,
        IRON
    }

    public class InventoryComponent : Component
    {
        public int amount;
        public int maxCapacity;
        public ResourceType resourceType = ResourceType.NULL;
    }
}
