using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class DwellingSlotIndexComponent : Component
    {
        public DwarfsSlots dwarfsSlots;
        private DwellingSlotsManager dwellingSlotsManager;

        private void Start()
        {
            dwellingSlotsManager = FindObjectOfType<DwellingSlotsManager>();
            bool tmp = dwellingSlotsManager.newDwarf(this);
        }
    }
}