using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingBtn : MonoBehaviour
{
    [SerializeField]
    private GameObject buildingSelected;
    [SerializeField]
    DragNDropSystem dragNDropSystem;

    public void selectBuilding()
    {
        dragNDropSystem.buildingSelected = buildingSelected;
    }
}
