using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Nastrond
{
    /* // Comment
     * / This system is currently based on the DwarfsSlots component wich are of the type Dwelling
     * / A problem can happen when the player destroy one of them, it will not show the exact dwarf number
     * / Possible upgrade : base the system on the dwarves themself, should be able to be done when the spawn of them is implemented
     */ 
    public class GrowthSystem : Nastrond.System
    {
        List<DwarfsSlots> dwarfSlots = new List<DwarfsSlots>();

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

            spawnDwarfTimer = Random.Range(spawnDwarfTimerMin, spawnDwarfTimerMax);
        }

        void Update()
        {
            if (Time.time > lastTimeDwarfHasBeenSpawned + spawnDwarfTimer)
                SpawnDwarfs(Random.Range(spawnDwarfMin, spawnDwarfMax));

            SetPopulationCount();
        }

        public void RegisterDwelling(DwarfsSlots ds)
        {
            populationCapacity += ds.maxNumberSlots;
            dwarfSlots.Add(ds);
        }

        public void UnregisterDwelling(DwarfsSlots ds)
        {
            populationCapacity -= ds.maxNumberSlots;
            dwarfSlots.Remove(ds);
        }

        void SetPopulationCount()
        {
            int tmpPopCount = 0;
            
            foreach (DwarfsSlots ds in dwarfSlots)
                tmpPopCount += ds.attributedDwarfsNumber;

            populationCount = tmpPopCount;
        }

        void SpawnDwarfs(int number)
        {
            lastTimeDwarfHasBeenSpawned = Time.time;
            spawnDwarfTimer = Random.Range(spawnDwarfTimerMin, spawnDwarfTimerMax);

            if (populationCount + number > populationCapacity)
                number = populationCapacity - populationCount;

            if (number <= 0)
                return;

            for(int i = 0; i < number; i++)
            {
                /*
                 * // TODO:
                 * // Actually creat a dwarf
                */
            }
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
