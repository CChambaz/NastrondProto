using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class MineManager : System
    {
        private const int FramesPerSecond = 50;
        private const int secondPerMinute = 60;

        private IronProducer[] ironProducers;
        private IronInventory[] ironInventories;
        private DwarfsSlots[] workingSlots;

        void Start()
        {
            ironProducers = new IronProducer[0];
            ironInventories = new IronInventory[0];
            workingSlots = new DwarfsSlots[0];
        }

        public void NewMine(Entity entity)
        {
            addNewMine(entity.GetComponent<IronProducer>(), entity.GetComponent<IronInventory>(), entity.GetComponent<DwarfsSlots>());
        }

        private void addNewMine(IronProducer ironProducer, IronInventory ironInventory, DwarfsSlots dwarfsSlots)
        {
            IronProducer[] tmpIronProducers = new IronProducer[ironProducers.Length + 1];
            IronInventory[] tmpIronInventories = new IronInventory[ironInventories.Length + 1];
            DwarfsSlots[] tmpDwarfsSlots = new DwarfsSlots[workingSlots.Length + 1];

            for (int i = 0; i < ironProducers.Length; i++)
            {
                tmpIronProducers[i] = ironProducers[i];
                tmpIronInventories[i] = ironInventories[i];
                tmpDwarfsSlots[i] = workingSlots[i];
            }

            tmpIronProducers[tmpIronProducers.Length - 1] = ironProducer;
            tmpIronInventories[tmpIronProducers.Length - 1] = ironInventory;
            tmpDwarfsSlots[tmpDwarfsSlots.Length - 1] = dwarfsSlots;

            ironProducers = tmpIronProducers;
            ironInventories = tmpIronInventories;
            workingSlots = tmpDwarfsSlots;
        }

        private void FixedUpdate()
        {
            for (int index = 0; index < ironProducers.Length; index++)
            {
                Produce(ironProducers[index], ironInventories[index], workingSlots[index]);
            }
        }

        private void Produce(IronProducer ironProducer, IronInventory ironInventory, DwarfsSlots dwarfsSlots)
        {
            if(!ironProducer || !ironInventory || !dwarfsSlots)
                return;

            if (ironProducer.timeSinceLastProduction >=
                ironProducer.productionEveryXMinutes * FramesPerSecond * secondPerMinute ||
                ironInventory.ironStoredIn < ironInventory.IronMaxCapacity)
            {
                ironInventory.ironStoredIn += (ironProducer.productionPerMinute * dwarfsSlots.dwarfsAlreadyIn);
                ironProducer.timeSinceLastProduction = 0;
            }
            else
            {
                ironProducer.timeSinceLastProduction++;
            }

        }
    }
}

