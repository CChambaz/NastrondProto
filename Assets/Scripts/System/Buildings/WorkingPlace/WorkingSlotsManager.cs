using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{
    public class WorkingSlotsManager : System
    {
        private DwarfsSlots[] workingsSlots;
        private WorkingSlotIndexComponent[] workingSlotIndexComponents;

        private void Start()
        {
            workingsSlots = new DwarfsSlots[0];
            workingSlotIndexComponents = new WorkingSlotIndexComponent[0];
        }

        public void NewWorkingPlace(DwarfsSlots dwarfsSlots)
        {
            if (dwarfsSlots.buildingType == DwarfsSlots.BuildingType.DWELLING)
                return;

            IncreaseDwellingSlotsComponentSizeAndAttribute(dwarfsSlots);

            foreach (WorkingSlotIndexComponent workingSlotIndexComponent in workingSlotIndexComponents)
            {
                if (workingSlotIndexComponent.dwarfsSlots != null)
                {
                    continue;
                }

                if (!AttributeDwellingToDwarf(workingSlotIndexComponent))
                {
                    break;
                }
            }
        }

        public bool newDwarf(WorkingSlotIndexComponent workingSlotIndexComponent)
        {
            IncreaseWorkingSlotIndexComponentsSizeAndAttribute(workingSlotIndexComponent);

            return AttributeDwellingToDwarf(workingSlotIndexComponent);
        }

        public bool AttributeDwellingToDwarf(WorkingSlotIndexComponent workingSlotIndexComponent)
        {
            foreach (DwarfsSlots workingsSlot in workingsSlots)
            {
                if (workingsSlot.attributedDwarfsNumber >= workingsSlot.maxNumberSlots)
                {
                    continue;
                }

                workingsSlot.attributedDwarfsNumber++;
                workingSlotIndexComponent.dwarfsSlots = workingsSlot;

                for (int j = 0; j < workingsSlot.maxNumberSlots; j++)
                {
                    if (workingsSlot.attributedDwellingsSlotIndexComponent[j] != null)
                    {
                        continue;
                    }

                    workingsSlot.attributedWorkingsSlotIndexComponent[j] = workingSlotIndexComponent;
                    break;
                }

                return true;
            }

            return false;
        }

        private void IncreaseWorkingSlotIndexComponentsSizeAndAttribute(WorkingSlotIndexComponent workingSlotIndexComponent)
        {
            WorkingSlotIndexComponent[] tmpWorkingSlotIndexComponents = new WorkingSlotIndexComponent[workingSlotIndexComponents.Length + 1];

            for (int i = 0; i < workingSlotIndexComponents.Length; i++)
            {
                tmpWorkingSlotIndexComponents[i] = workingSlotIndexComponents[i];
            }

            tmpWorkingSlotIndexComponents[tmpWorkingSlotIndexComponents.Length - 1] = workingSlotIndexComponent;

            workingSlotIndexComponents = tmpWorkingSlotIndexComponents;
        }

        private void IncreaseDwellingSlotsComponentSizeAndAttribute(DwarfsSlots dwarfsSlots)
        {
            DwarfsSlots[] tmpDwarfsSlots = new DwarfsSlots[workingsSlots.Length + 1];

            for (int i = 0; i <workingsSlots.Length; i++)
            {
                tmpDwarfsSlots[i] = workingsSlots[i];
            }
            tmpDwarfsSlots[tmpDwarfsSlots.Length - 1] = dwarfsSlots;

            workingsSlots = tmpDwarfsSlots;
        }
    }
}

