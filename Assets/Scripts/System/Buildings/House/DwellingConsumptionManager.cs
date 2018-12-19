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
            //Entity[] entitys = FindObjectsOfType<Entity>();

            //List<FoodInventory> tmpFoodInventorys = new List<FoodInventory>();

            //foreach(Entity entity in entitys)
            //{
            //    FoodInventory tmpFoodInventory = entity.GetComponent<FoodInventory>();

            //    if(tmpFoodInventory != null)
            //    {
            //        tmpFoodInventorys.Add(tmpFoodInventory);
            //    }
            //}

            //foodInventorys = new FoodInventory[tmpFoodInventorys.Count];

            //for(int i = 0;i < tmpFoodInventorys.Count;i++)
            //{
            //    foodInventorys[i] = tmpFoodInventorys[i];
            //}
            dwellingSlots = new DwarfsSlots[0];
            foodInventorys = new FoodInventory[0];
        }

        public void newDwelling(Entity entity)
        {
            addNewDwellingsSlotsAndFoodInventory(entity.GetComponent<FoodInventory>(), entity.GetComponent<DwarfsSlots>());
        }

        private void addNewDwellingsSlotsAndFoodInventory(FoodInventory foodInventory, DwarfsSlots dwarfsSlots)
        {
            FoodInventory[] tmpFoodInventory = new FoodInventory[foodInventorys.Length + 1];
            DwarfsSlots[] tmpDwarfsSlots = new DwarfsSlots[dwellingSlots.Length + 1];

            for (int i = 0; i < foodInventorys.Length; i++)
            {
                tmpFoodInventory[i] = foodInventorys[i];
                tmpDwarfsSlots[i] = dwellingSlots[i];
            }

            tmpFoodInventory[tmpFoodInventory.Length - 1] = foodInventory;
            tmpDwarfsSlots[tmpDwarfsSlots.Length - 1] = dwarfsSlots;

            foodInventorys = tmpFoodInventory;
            dwellingSlots = tmpDwarfsSlots;
        }



        void FixedUpdate()
        {
            for (int index = 0; index < foodInventorys.Length; index++)
            {
                ConsumeRessources(foodInventorys[index], dwellingSlots[index].dwarfsAlreadyIn);
            }
        }

        private void ConsumeRessources(FoodInventory foodInventory, int NmbDwarfIn)
        {
            if(!foodInventory)
                return;

            if (foodInventory.timeSinceLastConsumption >= foodInventory.minuteBeforConsuming * FramesPerSecond * secondPerMinute)
            {
                foodInventory.foodStoredIn -= NmbDwarfIn;
                foodInventory.timeSinceLastConsumption = 0;
            }
            else
            {
                foodInventory.timeSinceLastConsumption++;
            }
        }
    }
}

