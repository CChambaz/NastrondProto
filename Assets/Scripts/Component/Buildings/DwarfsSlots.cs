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

        public DwellingSlotIndexComponent[] attributedDwellingsSlotIndexComponent;

        private void Start()
        {
            if (buildingType == BuildingType.DWELLING)
                FindObjectOfType<GrowthSystem>().RegisterDwelling(this);

            attributedDwellingsSlotIndexComponent = new DwellingSlotIndexComponent[maxNumberSlots];
            FindObjectOfType<DwellingSlotsManager>().newDwelling(this);
        }

        private void OnDestroy()
        {
            if(buildingType == BuildingType.DWELLING)
                FindObjectOfType<GrowthSystem>().UnregisterDwelling(this);
        }
    }
}
