using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class FoodProducer : Component
    {
        public int productionPerMinute = 1;
        public int productionEveryXMinutes = 1;
        public int timeSinceLastProduction = 0;

        void Start()
        {

        }
    }

}

