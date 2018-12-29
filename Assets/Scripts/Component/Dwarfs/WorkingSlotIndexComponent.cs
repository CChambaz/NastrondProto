using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class WorkingSlotIndexComponent:Component
    {
        public DwarfsSlots dwarfsSlots;

        void Start()
        {
            FindObjectOfType<WorkingSlotsManager>().newDwarf(this);
        }
    }
}