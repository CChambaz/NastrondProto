using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class FoodFactoryManager : MonoBehaviour
    {
        private const int framesPerSecond = 50;
        private const int secondPerMinute = 60;

        private FoodProducer[] foodProducers;
        private GiverComponent[] foodGivers;
        private DwarfsSlots[] workingSlots;

        void Start()
        {
            foodProducers = new FoodProducer[0];
            foodGivers = new GiverComponent[0];
            workingSlots = new DwarfsSlots[0];
        }

        public void NewFoodFactory(Entity entity)
        {
            AddNewFoodFactory(entity.GetComponent<FoodProducer>(), entity.GetComponent<GiverComponent>(), entity.GetComponent<DwarfsSlots>());
        }

        private void AddNewFoodFactory(FoodProducer foodProducer, GiverComponent foodGiver, DwarfsSlots dwarfsSlots)
        {
            FoodProducer[] tmpFoodProducers = new FoodProducer[foodProducers.Length + 1];
            GiverComponent[] tmpFoodGivers = new GiverComponent[foodGivers.Length + 1];
            DwarfsSlots[] tmpDwarfsSlots = new DwarfsSlots[workingSlots.Length + 1];

            for (int i = 0; i < foodProducers.Length; i++)
            {
                tmpFoodProducers[i] = foodProducers[i];
                tmpFoodGivers[i] = foodGivers[i];
                tmpDwarfsSlots[i] = workingSlots[i];
            }

            tmpFoodProducers[tmpFoodProducers.Length - 1] = foodProducer;
            tmpFoodGivers[tmpFoodGivers.Length - 1] = foodGiver;
            tmpDwarfsSlots[tmpDwarfsSlots.Length - 1] = dwarfsSlots;

            foodProducers = tmpFoodProducers;
            foodGivers = tmpFoodGivers;
            workingSlots = tmpDwarfsSlots;
        }



        private void Produce(FoodProducer foodProducer, GiverComponent foodGiver, DwarfsSlots dwarfsSlots)
        {
            if (!foodProducer || !foodGiver || !dwarfsSlots)
                return;

            if (foodProducer.timeSinceLastProduction >=
                foodProducer.productionEveryXMinutes * framesPerSecond * secondPerMinute ||
                foodGiver.amount < foodGiver.maxCapacity)
            {
                foodGiver.amount += (foodProducer.productionPerMinute * dwarfsSlots.dwarfsAlreadyIn);
                foodProducer.timeSinceLastProduction = 0;
            }
            else
            {
                foodProducer.timeSinceLastProduction++;
            }
        }
    }
}

