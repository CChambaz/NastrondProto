using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{

    public class OnMouseOverSystem : System
    {
        List<InventoryComponent> inventoryComponentList;
        List<SpriteRenderer> spriteList;
        private Vector3 tmpLookPos;
        private bool resourceShow = true;

        // Start is called before the first frame update
        void Start()
        {     
            inventoryComponentList = new List<InventoryComponent>();
            spriteList = new List<SpriteRenderer>();

            List<GameObject> tmpEntities = GetEntities();

            foreach (GameObject e in tmpEntities)
            {
                if (e.GetComponent<InventoryComponent>() && e.GetComponent<SpriteRenderer>())
                {
                    inventoryComponentList.Add(e.GetComponent<InventoryComponent>());
                    spriteList.Add(e.GetComponent<SpriteRenderer>());
                }
            }   
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (resourceShow && lookPos != tmpLookPos)
            {
                for (int index = 0; index < spriteList.Count; index++)
                {
                    if (lookPos.x <= spriteList[index].bounds.max.x
                                     && lookPos.x >= spriteList[index].bounds.min.x
                                     && lookPos.y <= spriteList[index].bounds.max.y
                                     && lookPos.y >= spriteList[index].bounds.min.y)
                    {
                        Debug.Log("this inventory contains : " + inventoryComponentList[index].resourceType);
                        Debug.Log("amount : " + inventoryComponentList[index].amount);
                        resourceShow = false;
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                resourceShow = true;
            }
            tmpLookPos = lookPos;
        }
    }
}


