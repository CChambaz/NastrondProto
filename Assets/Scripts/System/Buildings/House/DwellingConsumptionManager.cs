using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class DwellingConsumptionManager : System {

        private FoodInventory[] foodInventorys;
        private DwarfsSlots[] dwellingSlots;

        private const int FramesPerSecond = 50;
        private const int secondPerMinute = 60;

        private void Start() {
            Entity[] entitys = FindObjectsOfType<Entity>();

            List<FoodInventory> tmpFoodInventorys = new List<FoodInventory>();

            foreach(Entity entity in entitys)
            {
                FoodInventory tmpFoodInventory = entity.GetComponent<FoodInventory>();

                if(tmpFoodInventory != null)
                {
                    tmpFoodInventorys.Add(tmpFoodInventory);
                }
            }

            foodInventorys = new FoodInventory[tmpFoodInventorys.Count];

            for(int i = 0;i < tmpFoodInventorys.Count;i++)
            {
                foodInventorys[i] = tmpFoodInventorys[i];
            }
        }

        void FixedUpdate()
        {
            for (int index = 0; index < foodInventorys.Length; index++)
            {
                ConsumeRessources(foodInventorys[index], dwellingSlots[index].dwarfsAlreadyIn);
            }
        }

        private void ConsumeRessources(FoodInventory foodInventory, float frequencyConsumptionPerSecond)
        {
            if (foodInventory.lastConsumptionConsumption >= foodInventory.minuteBeforConsuming * FramesPerSecond * secondPerMinute)
            {
                foodInventory.foodStoredIn--;
                foodInventory.lastConsumptionConsumption = 0;
            }
            else
            {
                foodInventory.lastConsumptionConsumption++;
            }
        }
    }
}

