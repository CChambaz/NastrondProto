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
        public WorkingSlotIndexComponent[] attributedWorkingsSlotIndexComponent;

        private void Start()
        {
            if (buildingType == BuildingType.DWELLING)
                FindObjectOfType<GrowthSystem>().RegisterDwelling(this);

            attributedDwellingsSlotIndexComponent = new DwellingSlotIndexComponent[maxNumberSlots];
            attributedWorkingsSlotIndexComponent = new WorkingSlotIndexComponent[maxNumberSlots];
            if(buildingType == BuildingType.DWELLING)
                FindObjectOfType<DwellingSlotsManager>().newDwelling(this);
            else if (buildingType == BuildingType.WORKING_PLACE)
                FindObjectOfType<WorkingSlotsManager>().NewWorkingPlace(this);
        }

        private void OnDestroy()
        {
            if(buildingType == BuildingType.DWELLING)
                FindObjectOfType<GrowthSystem>().UnregisterDwelling(this);
        }
    }
}
