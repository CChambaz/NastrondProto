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
        private GiverComponent[] ironGivers;
        private DwarfsSlots[] workingSlots;

        void Start()
        {
            ironProducers = new IronProducer[0];
            ironGivers = new GiverComponent[0];
            workingSlots = new DwarfsSlots[0];
        }

        public void NewMine(Entity entity)
        {
            addNewMine(entity.GetComponent<IronProducer>(), entity.GetComponent<GiverComponent>(), entity.GetComponent<DwarfsSlots>());
        }

        private void addNewMine(IronProducer ironProducer, GiverComponent ironGiver, DwarfsSlots dwarfsSlots)
        {
            IronProducer[] tmpIronProducers = new IronProducer[ironProducers.Length + 1];
            GiverComponent[] tmpIronGivers = new GiverComponent[ironGivers.Length + 1];
            DwarfsSlots[] tmpDwarfsSlots = new DwarfsSlots[workingSlots.Length + 1];
            
            for (int i = 0; i < ironProducers.Length; i++)
            {
                tmpIronProducers[i] = ironProducers[i];
                tmpIronGivers[i] = ironGivers[i];
                tmpDwarfsSlots[i] = workingSlots[i];
            }

            tmpIronProducers[tmpIronProducers.Length - 1] = ironProducer;
            tmpIronGivers[tmpIronProducers.Length - 1] = ironGiver;
            tmpDwarfsSlots[tmpDwarfsSlots.Length - 1] = dwarfsSlots;

            ironProducers = tmpIronProducers;
            ironGivers = tmpIronGivers;
            workingSlots = tmpDwarfsSlots;
        }

        private void FixedUpdate()
        {
            for (int index = 0; index < ironProducers.Length; index++)
            {
                Produce(ironProducers[index], ironGivers[index], workingSlots[index]);
            }
        }

        private void Produce(IronProducer ironProducer, GiverComponent ironGiver, DwarfsSlots dwarfsSlots)
        {
            if(!ironProducer || !ironGiver || !dwarfsSlots)
                return;

            if (ironProducer.timeSinceLastProduction >=
                ironProducer.productionEveryXMinutes * FramesPerSecond * secondPerMinute ||
                ironGiver.amount < ironGiver.maxCapacity)
            {
                ironGiver.amount += (ironProducer.productionPerMinute * dwarfsSlots.dwarfsAlreadyIn);
                ironProducer.timeSinceLastProduction = 0;
            }
            else
            {
                ironProducer.timeSinceLastProduction++;
            }

        }
    }
}

