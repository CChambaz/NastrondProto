using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class DwellingSlotsManager:Nastrond.System
    {
        public DwarfsSlots[] dwellingsSlots;
        public DwellingSlotIndexComponent[] dwellingSlotIndexComponents;

        private void Awake()
        {
            dwellingsSlots = new DwarfsSlots[0];
            dwellingSlotIndexComponents = new DwellingSlotIndexComponent[0];
        }

        public bool AttributeDwellingToDwarf(DwellingSlotIndexComponent dwellingSlotIndexComponent) {
            foreach (DwarfsSlots dwellingSlot in dwellingsSlots) {
                if (dwellingSlot.attributedDwarfsNumber >= dwellingSlot.maxNumberSlots) {
                    continue;
                }

                dwellingSlot.attributedDwarfsNumber++;
                dwellingSlotIndexComponent.dwarfsSlots = dwellingSlot;

                for (int j = 0; j < dwellingSlot.maxNumberSlots; j++)
                {
                    if (dwellingSlot.attributedDwellingsSlotIndexComponent[j] != null) {
                        continue;
                    }

                    dwellingSlot.attributedDwellingsSlotIndexComponent[j] = dwellingSlotIndexComponent;
                    break;
                }

                return true;
            }

            return false;
        }

        public bool newDwarf(DwellingSlotIndexComponent dwellingSlotIndexComponent)
        {
            IncreaseDwellingSlotIndexComponentsSizeAndAttribute(dwellingSlotIndexComponent);
            
            return AttributeDwellingToDwarf(dwellingSlotIndexComponent);
        }

        public void newDwelling(DwarfsSlots dwarfsSlots)
        {
            IncreaseDwellingSlotsComponentSizeAndAttribute(dwarfsSlots);

            foreach(DwellingSlotIndexComponent dwellingSlotIndexComponent in dwellingSlotIndexComponents) {
                if (dwellingSlotIndexComponent.dwarfsSlots != null) {
                    continue;
                }

                if(!AttributeDwellingToDwarf(dwellingSlotIndexComponent)) {
                    break;
                }
            }
        }

        private void IncreaseDwellingSlotIndexComponentsSizeAndAttribute(DwellingSlotIndexComponent dwellingSlotIndexComponent)
        {
            DwellingSlotIndexComponent[] tmpDwellingSlotIndexComponents = new DwellingSlotIndexComponent[dwellingSlotIndexComponents.Length + 1];

            for (int i = 0; i < dwellingSlotIndexComponents.Length; i++)
            {
                tmpDwellingSlotIndexComponents[i] = dwellingSlotIndexComponents[i];
            }

            tmpDwellingSlotIndexComponents[tmpDwellingSlotIndexComponents.Length - 1] = dwellingSlotIndexComponent;

            dwellingSlotIndexComponents = tmpDwellingSlotIndexComponents;;
        }

        private void IncreaseDwellingSlotsComponentSizeAndAttribute(DwarfsSlots dwarfsSlots)
        {
            DwarfsSlots[] tmpDwarfsSlots = new DwarfsSlots[dwellingsSlots.Length + 1];

            for (int i = 0; i < dwellingsSlots.Length; i++)
            {
                tmpDwarfsSlots[i] = dwellingsSlots[i];
            }
            tmpDwarfsSlots[tmpDwarfsSlots.Length - 1] = dwarfsSlots;

            dwellingsSlots = tmpDwarfsSlots;
        }
    }
}
