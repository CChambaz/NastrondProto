using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{

    public enum DwarfToolQuality
    {
        STANDARD,
        SUPERIOR,
        ADVANCED
    }

    public class DwarfToolComponent : Component
    {
        public int durability;
        public DwarfToolQuality dwarftool = DwarfToolQuality.STANDARD;
    }
}
