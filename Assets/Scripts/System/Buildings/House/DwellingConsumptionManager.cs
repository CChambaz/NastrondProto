using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class DwellingConsumptionManager : System {

        private ReceiverComponent[] recieverFoods;
        private DwarfsSlots[] dwellingSlots;
        private FoodConsumer[] foodConsumers;

        private const int FramesPerSecond = 50;
        private const int secondPerMinute = 60;

        private void Start() {
            dwellingSlots = new DwarfsSlots[0];
            recieverFoods = new ReceiverComponent[0];
            foodConsumers = new FoodConsumer[0];
        }

        public void newDwelling(Entity entity)
        {
            addNewDwellingsSlotsAndFoodInventory(entity.GetComponent<ReceiverComponent>(), entity.GetComponent<DwarfsSlots>(), entity.GetComponent<FoodConsumer>());
        }

        private void addNewDwellingsSlotsAndFoodInventory(ReceiverComponent recieverComponent, DwarfsSlots dwarfsSlots, FoodConsumer foodConsumer)
        {
            ReceiverComponent[] tmpFoodReceiever = new ReceiverComponent[recieverFoods.Length + 1];
            DwarfsSlots[] tmpDwarfsSlots = new DwarfsSlots[dwellingSlots.Length + 1];
            FoodConsumer[] tmpFoodConsumers = new FoodConsumer[foodConsumers.Length + 1];



            for (int i = 0; i < recieverFoods.Length; i++)
            {
                tmpFoodReceiever[i] = recieverFoods[i];
                tmpDwarfsSlots[i] = dwellingSlots[i];
                tmpFoodConsumers[i] = foodConsumers[i];
            }

            tmpFoodReceiever[tmpFoodReceiever.Length - 1] = recieverComponent;
            tmpDwarfsSlots[tmpDwarfsSlots.Length - 1] = dwarfsSlots;
            tmpFoodConsumers[tmpFoodConsumers.Length - 1] = foodConsumer;

            recieverFoods = tmpFoodReceiever;
            dwellingSlots = tmpDwarfsSlots;
            foodConsumers = tmpFoodConsumers;
        }



        void FixedUpdate()
        {
            for (int index = 0; index < recieverFoods.Length; index++)
            {
                ConsumeRessources(recieverFoods[index], foodConsumers[index], dwellingSlots[index].dwarfsAlreadyIn);
            }
        }

        private void ConsumeRessources(ReceiverComponent receiverComponent, FoodConsumer foodConsumer, int nmbDwarfIn)
        {
            if(!receiverComponent)
                return;

            if (foodConsumer.timeSinceLastConsumption >= foodConsumer.minuteBeforConsuming * FramesPerSecond * secondPerMinute)
            {
                receiverComponent.amount -= nmbDwarfIn;
                foodConsumer.timeSinceLastConsumption = 0;
            }
            else
            {
                foodConsumer.timeSinceLastConsumption++;
            }
        }
    }
}

