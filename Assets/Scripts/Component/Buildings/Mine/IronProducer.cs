using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

namespace Nastrond
{
    public class IronProducer : Component
    {
        public int productionPerMinute = 1;
        public int productionEveryXMinutes = 1;
        public int timeSinceLastProduction = 0;


        void Start()
        {
            FindObjectOfType<MineManager>().NewMine(GetComponent<Entity>());
        }
    }
} 

