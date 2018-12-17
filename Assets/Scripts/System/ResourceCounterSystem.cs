using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class ResourceCounterSystem : System
    {

        List<InventoryComponent> inventoryComponentList;

        // Start is called before the first frame update
        void Start()
        {
            inventoryComponentList = new List<InventoryComponent>();
            List<GameObject> tmpEntities = GetEntities();

            foreach (GameObject e in tmpEntities)
            {
                if (e.GetComponent<InventoryComponent>())
                {
                    inventoryComponentList.Add(e.GetComponent<InventoryComponent>());
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int index = 0; index < inventoryComponentList.Count; index++)
            {
                Debug.Log("Resource is " + inventoryComponentList[index].resourceType);
                Debug.Log("total amount is " + inventoryComponentList[index].amount);
            }
        }
    }
}


//ResourceCounterSystem