using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class ReceiverComponent : Component {
        public int amount;
        public int maxCapacity;
        public ResourceType resourceType = ResourceType.NULL;
        public int nbDwarfsAttributed;
    }
}

