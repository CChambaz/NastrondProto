﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class DwellingSlotIndexComponent : Component
    {
        public DwarfsSlots dwarfsSlots;

        private void Start()
        {
            FindObjectOfType<DwellingSlotsManager>().newDwarf(this);
        }
    }
}