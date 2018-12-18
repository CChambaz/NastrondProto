using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class buildingBtn : MonoBehaviour
{
    
    public GameObject buildingPref;
    public GameObject tmpBuildingRef;
    public GridLayout gridLayout;
    private Coroutine dragCoroutine;
 
    private List<GameObject> cityBuildings;

    enum ValidationState
    {
        NONE,
        INVALID,
        VALID
    }

    ValidationState vstate = ValidationState.VALID;

    public void SelectBuilding()
    {
        Debug.Log("BuildingIsSelected");
        dragCoroutine=StartCoroutine(OnDragBuilding());
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && dragCoroutine != null && vstate == ValidationState.VALID)
        {
            StopCoroutine(dragCoroutine);
            if(cityBuildings!=null)
            cityBuildings.Add(tmpBuildingRef);
            else
            {
                cityBuildings = new List<GameObject>();
                cityBuildings.Add(tmpBuildingRef);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(tmpBuildingRef);
            
        }
    }
 
    IEnumerator OnDragBuilding()
    {
        
        Vector3 mousePositionInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject tmpBuilding;
        tmpBuilding = Instantiate(buildingPref,mousePositionInWorldPoint,Quaternion.identity);
        tmpBuildingRef=tmpBuilding;
        Debug.Log("Building exists");
        while (true)
        {
            mousePositionInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = gridLayout.WorldToCell(new Vector3Int(Mathf.RoundToInt(mousePositionInWorldPoint.x), Mathf.RoundToInt(mousePositionInWorldPoint.y), /*Mathf.RoundToInt(mousePositionInWorldPoint.z)*/0));
            
            tmpBuilding.transform.position = gridLayout.CellToWorld(cellPosition);

           

            if (cityBuildings != null)
            {
                bool collide=false;
                foreach (GameObject item in cityBuildings)
                {

                    if (item.transform.position.x < tmpBuilding.transform.position.x + tmpBuilding.GetComponent<SpriteRenderer>().bounds.size.x &&
                         item.transform.position.x + item.GetComponent<SpriteRenderer>().bounds.size.x > tmpBuilding.transform.position.x &&
                        item.transform.position.y < tmpBuilding.transform.position.y + tmpBuilding.GetComponent<SpriteRenderer>().bounds.size.y &&
                       item.GetComponent<SpriteRenderer>().bounds.size.y + item.transform.position.y > tmpBuilding.transform.position.y)
                    {
                        // collision !
                        collide = true;
                        Debug.Log("Invalid");
                    }
                    else
                    {
                        collide = false;
                        Debug.Log("Valid");
                    }

                    
                  
                }
                if(collide)
                    vstate = ValidationState.INVALID;
                else
                    vstate = ValidationState.VALID;
                yield return new WaitForEndOfFrame();
            }
            
            yield return new WaitForEndOfFrame();
        }
    }

    
}
