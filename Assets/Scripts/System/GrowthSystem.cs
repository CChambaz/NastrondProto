﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Nastrond
{
    public class GrowthSystem : Nastrond.System
    {
        List<DwarfsSlots> dwarfSlots = new List<DwarfsSlots>();
        bool dwarfSlotsHasChanged = false;

        int populationCapacity = 0;
        int populationCount = 0;

        public float creatDwarfTimer = 120.0f;
        public int creatDwarfMin = 2;
        public int creatDwarfMax = 7;
        float lastTimeDwarfHasBeenCreated = 0f;        

        void Update()
        {
            GetDwarfSlot();

            if (dwarfSlotsHasChanged)
                SetPopulationCapacity();

            if (Time.time > lastTimeDwarfHasBeenCreated + creatDwarfTimer)
            {
                Random.InitState(Random.Range(0, int.MaxValue));

                SpawnDwarfs(Random.Range(creatDwarfMin, creatDwarfMax));
            }

            SetPopulationCount();
        }

        void GetDwarfSlot()
        {
            Entity[] entitys = FindObjectsOfType<Entity>();

            foreach(Entity e in entitys)
            {
                DwarfsSlots dwarfSlot = e.GetComponent<DwarfsSlots>();

                if (dwarfSlot != null && dwarfSlot.buildingType == DwarfsSlots.BuildingType.DWELLING && !dwarfSlots.Contains(dwarfSlot))
                {
                    dwarfSlots.Add(dwarfSlot);
                    dwarfSlotsHasChanged = true;
                }
            }

            Debug.Log("Count of dwarf slots found : " + dwarfSlots.Count);
        }

        void SetPopulationCapacity()
        {
            dwarfSlotsHasChanged = false;

            int tmpPopCapacity = 0;

            foreach (DwarfsSlots ds in dwarfSlots)
            {
                tmpPopCapacity += ds.maxNumberSlots;
            }

            populationCapacity = tmpPopCapacity;

            Debug.Log("Seted population capacity to " + populationCapacity);
        }

        void SetPopulationCount()
        {
            int tmpPopCount = 0;

            foreach(DwarfsSlots ds in dwarfSlots)
            {
                tmpPopCount += ds.attributedDwarfsNumber;
            }

            populationCount = tmpPopCount;

            Debug.Log("Seted population count to " + populationCount);
        }

        void SpawnDwarfs(int number)
        {
            lastTimeDwarfHasBeenCreated = Time.time;

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

                Debug.Log("Dwarf n°" + i + " has been created!");
            }
        }
    }
}