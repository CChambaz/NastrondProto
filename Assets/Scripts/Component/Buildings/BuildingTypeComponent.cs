using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTypeComponent : Component {

    private int level=0;
    private long price=0;
    //Add ressources needed
    private int size=0;
   
    public void Init(BuildingTypeSelectorEntity.BuildingTypes type)
    {
        switch (type)
        {
            case BuildingTypeSelectorEntity.BuildingTypes.House:
                level = 1;
                price = 100;
                size = 1;
                break;
            case BuildingTypeSelectorEntity.BuildingTypes.Forge:
                level = 1;
                price = 200;
                size = 1;
                break;
            default:
                break;
        }
    }
}
