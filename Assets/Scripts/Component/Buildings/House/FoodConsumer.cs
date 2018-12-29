using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{
    public class FoodConsumer : MonoBehaviour
    {
        public int timeSinceLastConsumption = 0;
        public float minuteBeforConsuming = 2;

        private void Start()
        {
            FindObjectOfType<DwellingConsumptionManager>().newDwelling(GetComponent<Entity>());
        }
    }
}
