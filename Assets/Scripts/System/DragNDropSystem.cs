using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DragNDropSystem : MonoBehaviour,IDragHandler,IEndDragHandler
{

    public GameObject buildingSelected;
    public GameObject tmpBuilding;
    public GridLayout gridLayout;



    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("StartDrag");
        if (buildingSelected != null)
        {
            Debug.Log("BuildingIsSelected");
            tmpBuilding = Instantiate(buildingSelected, Input.mousePosition, Quaternion.identity);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(tmpBuilding!=null)
        {
            Debug.Log("Building exists");
            Vector3Int cellPosition = gridLayout.WorldToCell(new Vector3Int(Mathf.RoundToInt(Input.mousePosition.x), Mathf.RoundToInt(Input.mousePosition.y), Mathf.RoundToInt(Input.mousePosition.z)));
             tmpBuilding.transform.position = gridLayout.CellToWorld(cellPosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
