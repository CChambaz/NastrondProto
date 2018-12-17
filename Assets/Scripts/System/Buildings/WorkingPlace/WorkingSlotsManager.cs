using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{
    public class WorkingSlotsManager : System
    {
        private DwarfsSlots[] workingsSlots;
        private DwellingSlotIndexComponent[] dwellingSlotIndexComponents; //CHANGE WITH WORKING SLOT INDEX WHEN THEY WILL BE THERE

        private void Start()
        {
            Entity[] entitys = FindObjectsOfType<Entity>();

            #region Working Slots Component Attribution
            List<DwarfsSlots> tmpDwarfsSlots = new List<DwarfsSlots>();

            foreach (Entity entity in entitys)
            {
                DwarfsSlots tmpDwarfsSlot = entity.GetComponent<DwarfsSlots>();

                if (tmpDwarfsSlot != null && tmpDwarfsSlot.buildingType == DwarfsSlots.BuildingType.WORKING_PLACE)
                {
                    tmpDwarfsSlots.Add(tmpDwarfsSlot);
                }
            }

            int nmbOfDwarfsSlot = tmpDwarfsSlots.Count;

            workingsSlots = new DwarfsSlots[nmbOfDwarfsSlot];

            for (int i = 0; i < nmbOfDwarfsSlot; i++)
            {
                workingsSlots[i] = tmpDwarfsSlots[i];
            }
            #endregion

            #region Dwarf Slot Index Component Attribution
            List<DwellingSlotIndexComponent> tmpsDwellingSlotIndexComponents = new List<DwellingSlotIndexComponent>();

            foreach (Entity entity in entitys)
            {
                DwellingSlotIndexComponent tmpDwellingSlotIndexComponent = entity.GetComponent<DwellingSlotIndexComponent>();

                if (tmpDwellingSlotIndexComponent != null)
                {
                    tmpsDwellingSlotIndexComponents.Add(tmpDwellingSlotIndexComponent);
                }
            }

            int nmbOfDellingSlotIndex = tmpsDwellingSlotIndexComponents.Count;

            dwellingSlotIndexComponents = new DwellingSlotIndexComponent[nmbOfDellingSlotIndex];

            for (int i = 0; i < nmbOfDellingSlotIndex; i++)
            {
                dwellingSlotIndexComponents[i] = tmpsDwellingSlotIndexComponents[i];
            }
            #endregion

        }
    }
}

