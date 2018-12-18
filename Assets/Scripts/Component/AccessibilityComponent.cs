using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{
    public enum Accessibility
    {
        TRUE,
        FALSE
    }

    public enum FluidType
    {
        MAGMA,
        WATER
    }

    public class AccessibilityComponent : Component
    {
        public Accessibility accessibility = Accessibility.TRUE;
        public FluidType resourceFluidType = FluidType.MAGMA;
    }
}
