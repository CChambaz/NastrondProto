using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DragNDropSystem : MonoBehaviour
{
    public List<GameObject> cityBuildings=new List<GameObject>();

    public void AddCityBuilding(GameObject building)
    {
        cityBuildings.Add(building);
        Debug.Log("Added building: " + building.transform.position);
    }

    public List<GameObject> GetcityBuildings()
    {
        return cityBuildings;
    }
}
