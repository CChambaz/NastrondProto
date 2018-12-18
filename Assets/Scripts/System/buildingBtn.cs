using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class BuildingBtn : MonoBehaviour
{
    
    public GameObject buildingPref;
    public GameObject tmpBuildingRef;
    public GridLayout gridLayout;
    private Coroutine dragCoroutine;
    private bool actionCancelled=false;
    private bool actionValidate = false;
    private bool actionsInProgress = false;
    private DragNDropSystem dragNDrop;

    enum ValidationState
    {
        NONE,
        INVALID,
        VALID
    }

    ValidationState vstate = ValidationState.VALID;


    private void Start()
    {
        dragNDrop = FindObjectOfType<DragNDropSystem>();
    }
    public void SelectBuilding()
    {
        dragCoroutine=StartCoroutine(OnDragBuilding());
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && dragCoroutine != null && vstate == ValidationState.VALID&&actionsInProgress)
        {
            actionValidate = true;
           
        }

        if (Input.GetMouseButtonDown(1)&&dragCoroutine!=null)
        {

            actionCancelled = true;
            
        }
    }
 
    IEnumerator OnDragBuilding()
    {
        
        Vector3 mousePositionInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject tmpBuilding;
        tmpBuilding = Instantiate(buildingPref,mousePositionInWorldPoint,Quaternion.identity);
        tmpBuildingRef=tmpBuilding;
        actionsInProgress = true;
        while (true)
        {

            
            mousePositionInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = (float)System.Math.Round(mousePositionInWorldPoint.x * 2f, System.MidpointRounding.AwayFromZero)/ 2f;//Mathf.RoundToInt(mousePositionInWorldPoint.x);
            float y = (float)System.Math.Round(mousePositionInWorldPoint.y * 2f, System.MidpointRounding.AwayFromZero)/2f-0.25f;
            if (x % 1 != 0)
                y += 0.25f;
            Vector3 cellPosition = gridLayout.LocalToCellInterpolated(new Vector3(x, y, 0));
           
            tmpBuilding.transform.position = gridLayout.CellToLocalInterpolated(cellPosition);
           

            if (dragNDrop.cityBuildings != null)
            {
                BuildingLimits tmpBuildingLimit = tmpBuilding.GetComponent<BuildingLimits>();
                bool collide=false;
                foreach (GameObject item in dragNDrop.cityBuildings)
                {

                    BuildingLimits itemLimit = item.GetComponent<BuildingLimits>();
                   if(isColliding(itemLimit,tmpBuildingLimit))
                        {
                            // collision !
                            collide = true;
                            Debug.Log("Invalid");
                        }
                   

                    
                  
                }
                if(collide)
                    vstate = ValidationState.INVALID;
                else
                    vstate = ValidationState.VALID;
                yield return new WaitForEndOfFrame();
            }
            
            yield return new WaitForEndOfFrame();



            


            if(actionCancelled)
            {
                Destroy(tmpBuilding);
                actionCancelled = false;
                break;
            }

            if (actionValidate)
            {
                dragNDrop.AddCityBuilding(tmpBuildingRef);
                actionValidate = false;
                break;
            }
          
        }
        actionsInProgress = false;
        dragCoroutine = null;
    }

    private bool isColliding(BuildingLimits item1, BuildingLimits item2)
    {
        // Get the distance beetween item 1 and item 2
        float centersDistance = (item1.GetCenter().position - item2.GetCenter().position).magnitude;

        // Get horizontal and vertical half size for item 1 and item 2
        float item1HorizontalHalfSize = (item1.GetTransformR().position - item1.GetCenter().position).magnitude;
        float item1VerticalHalfSize = (item1.GetCenter().position - item1.GetTransformD().position).magnitude;

        float item2HorizontalHalfSize = (item2.GetTransformR().position - item2.GetCenter().position).magnitude;
        float item2VerticalHalfSize = (item2.GetCenter().position- item2.GetTransformD().position).magnitude;

        //Debug.Log("distance : " + centersDistance / 2 + " | midsize : " + midSize + " | " + midSize2);
        Debug.Log("x pos check : " + (item2.GetCenter().position.x != item1.GetCenter().position.x) + " | horizontal check : " + (item2HorizontalHalfSize + item1HorizontalHalfSize > centersDistance * 2) + " | vertical check : " + (item1VerticalHalfSize + item2VerticalHalfSize > centersDistance * 2));

        if (item2.GetCenter().position.x != item1.GetCenter().position.x)
            return item2HorizontalHalfSize + item1HorizontalHalfSize > centersDistance * 2;
        else
            return item1VerticalHalfSize + item2VerticalHalfSize > centersDistance * 1.25;

    }
}
