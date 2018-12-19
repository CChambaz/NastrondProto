using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Nastrond
{
    public class GrowthSystem : Nastrond.System
    {
        public int initialPopulation = 8;
        int populationCapacity = 0;
        int populationCount = 0;

        public float spawnDwarfTimerMin = 45f;
        public float spawnDwarfTimerMax = 120f;
        public int spawnDwarfMin = 2;
        public int spawnDwarfMax = 7;

        float spawnDwarfTimer = 0f;
        float lastTimeDwarfHasBeenSpawned = 0f;        

        void Start()
        {
            Random.InitState(Random.Range(0, int.MaxValue));

            SpawnDwarfs(initialPopulation);
        }

        void Update()
        {
            if (Time.time > lastTimeDwarfHasBeenSpawned + spawnDwarfTimer)
                SpawnDwarfs(Random.Range(spawnDwarfMin, spawnDwarfMax));
        }

        void SpawnDwarfs(int number)
        {
            lastTimeDwarfHasBeenSpawned = Time.time;
            spawnDwarfTimer = Random.Range(spawnDwarfTimerMin, spawnDwarfTimerMax);

            if (populationCount + number > populationCapacity)
                number = populationCapacity - populationCount;

            if (number <= 0)
                return;

            for (int i = 0; i < number; i++)
            {
                /*
                 * // TODO:
                 * // Actually creat a dwarf
                */
            }
        }

        public void RegisterDwelling(DwarfsSlots ds)
        {
            populationCapacity += ds.maxNumberSlots;
        }

        public void UnregisterDwelling(DwarfsSlots ds)
        {
            populationCapacity -= ds.maxNumberSlots;
        }

        public void RegisterDwarf()
        {
            populationCount++;
        }

        public void UnregisterDwarf()
        {
            populationCount--;
        }

        public int GetPopulationCount()
        {
            return populationCount;
        }

        public int GetPopulationCapacity()
        {
            return populationCapacity;
        }
    }
}
