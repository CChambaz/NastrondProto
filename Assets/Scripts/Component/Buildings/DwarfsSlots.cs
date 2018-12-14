using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class DwarfsSlots:Component {
        public enum BuildingType {
            DWELLING,
            WORKING_PLACE
        }

        public BuildingType buildingType = BuildingType.WORKING_PLACE;
        public int maxNumberSlots = 0;
        public int attributedDwarfsNumber = 0;
        public int dwarfsAlreadyIn = 0;
    }
}
