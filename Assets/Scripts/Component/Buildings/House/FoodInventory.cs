using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class FoodInventory : Component
    {
        public int maxFoodCapacity = 100;
        public int foodStoredIn = 50;

        public int lastConsumptionConsumption = 0;
        public float minuteBeforConsuming = 2;
    }
}