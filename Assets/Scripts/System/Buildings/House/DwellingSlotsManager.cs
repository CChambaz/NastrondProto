using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class DwellingSlotsManager:Nastrond.System
    {
        public DwarfsSlots[] dwellingsSlots;
        public DwellingSlotIndexComponent[] dwellingSlotIndexComponents;

        private void Start()
        {
            //Entity[] entitys = FindObjectsOfType<Entity>();

            //#region Dwelling Slot Component Attribution
            //List<DwarfsSlots> tmpDwarfsSlots = new List<DwarfsSlots>();

            //foreach (Entity entity in entitys)
            //{
            //    DwarfsSlots tmpDwarfsSlot = entity.GetComponent<DwarfsSlots>();

            //    if (tmpDwarfsSlot != null && tmpDwarfsSlot.buildingType == DwarfsSlots.BuildingType.DWELLING)
            //    {
            //        tmpDwarfsSlots.Add(tmpDwarfsSlot);
            //    }
            //}

            //int nmbOfDwellingsSlot = tmpDwarfsSlots.Count;

            //dwellingsSlots = new DwarfsSlots[nmbOfDwellingsSlot];

            //for (int i = 0; i < nmbOfDwellingsSlot; i++)
            //{
            //    dwellingsSlots[i] = tmpDwarfsSlots[i];
            //}
            //#endregion

            //#region Dwarf Slot Index Component Attribution
            //List<DwellingSlotIndexComponent> tmpsDwellingSlotIndexComponents = new List<DwellingSlotIndexComponent>();

            //foreach (Entity entity in entitys)
            //{
            //    DwellingSlotIndexComponent tmpDwellingSlotIndexComponent = entity.GetComponent<DwellingSlotIndexComponent>();

            //    if (tmpDwellingSlotIndexComponent != null)
            //    {
            //        tmpsDwellingSlotIndexComponents.Add(tmpDwellingSlotIndexComponent);
            //    }
            //}

            //int nmbOfDellingSlotIndex = tmpsDwellingSlotIndexComponents.Count;

            //dwellingSlotIndexComponents = new DwellingSlotIndexComponent[nmbOfDellingSlotIndex];

            //for (int i = 0; i < nmbOfDellingSlotIndex; i++)
            //{
            //    dwellingSlotIndexComponents[i] = tmpsDwellingSlotIndexComponents[i];
            //}
            //#endregion

            //foreach (DwellingSlotIndexComponent dwellingSlotIndexComponent in dwellingSlotIndexComponents)
            //{
            //    AttributeDwellingToDwarf(dwellingSlotIndexComponent);
            //}

            dwellingsSlots = new DwarfsSlots[0];
            dwellingSlotIndexComponents = new DwellingSlotIndexComponent[0];
        }

        public bool AttributeDwellingToDwarf(DwellingSlotIndexComponent dwellingSlotIndexComponent)
        {
            for (int i = 0; i < dwellingsSlots.Length; i++)
            {
                if (dwellingsSlots[i].attributedDwarfsNumber < dwellingsSlots[i].maxNumberSlots)
                {
                    dwellingsSlots[i].attributedDwarfsNumber++;
                    dwellingSlotIndexComponent.dwarfsSlots = dwellingsSlots[i];

                    for (int j = 0; j < dwellingsSlots.Length; j++)
                    {
                        if (dwellingsSlots[i].attributedDwellingsSlotIndexComponent[j] != null)
                        {
                            dwellingsSlots[i].attributedDwellingsSlotIndexComponent[j] = dwellingSlotIndexComponent;
                            break;
                        }
                    }

                    return true;
                }
            }
            return false;
        }

        public bool newDwarf(DwellingSlotIndexComponent dwellingSlotIndexComponent)
        {
            IncreaseDwellingSlotIndexComponentsSizeAndAttribute(dwellingSlotIndexComponent);

            if (!AttributeDwellingToDwarf(dwellingSlotIndexComponent))
            {
                return false;
            }

            return true;
        }

        public void newDwelling(DwarfsSlots dwarfsSlots)
        {
            IncreaseDwellingSlotsComponentSizeAndAttribute(dwarfsSlots);
        }

        private void IncreaseDwellingSlotIndexComponentsSizeAndAttribute(DwellingSlotIndexComponent dwellingSlotIndexComponent)
        {
            DwellingSlotIndexComponent[] tmpDwellingSlotIndexComponents = new DwellingSlotIndexComponent[dwellingSlotIndexComponents.Length + 1];

            for (int i = 0; i < dwellingSlotIndexComponents.Length; i++)
            {
                tmpDwellingSlotIndexComponents[i] = dwellingSlotIndexComponents[i];
            }

            tmpDwellingSlotIndexComponents[tmpDwellingSlotIndexComponents.Length - 1] = dwellingSlotIndexComponent;

            dwellingSlotIndexComponents = tmpDwellingSlotIndexComponents;
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
