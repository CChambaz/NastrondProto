using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class MineManager : System
    {
        private IronProducer[] ironProducer;
        private IronInventory[] ironInventories;
        private DwarfsSlots[] workingSlots;

        void Start()
        {
            ironProducer = new IronProducer[0];
            ironInventories = new IronInventory[0];
            workingSlots = new DwarfsSlots[0];
        }
    }
}

