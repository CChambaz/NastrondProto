using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Entity for the drag and drop
public class BuildingTypeSelectorEntity : Entity {
    
    public enum BuildingTypes
    {
        None,
        House,
        Forge
    }
    public List<GameObject> buildingPrefabs;

    private BuildingTypes typeSelected;

    //Methode to instantiate a building with the 
	public void CreateBuildingComponent()
    {
        
        GameObject obj=null;
        switch (typeSelected)
        {
            case BuildingTypes.House:
                obj = buildingPrefabs[0];
                break;
            case BuildingTypes.Forge:
                obj = buildingPrefabs[1];
                break;
            default:
                break;
        }
        if(obj!=null)
        {
          
            obj.AddComponent<BuildingTypeComponent>();
           obj.GetComponent<BuildingTypeComponent>().Init(typeSelected);
            Instantiate(obj, transform);
        }
    }

    public BuildingTypes GetSelectedBuildingType()
    {
        return typeSelected;
    }

    private void OnEnable()
    {
        buildingPrefabs.Capacity= System.Enum.GetNames(typeof(BuildingTypes)).Length;
    }
}
