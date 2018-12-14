using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{
    public class StorageSystem : System
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
            int totalAmount = 0;
            for (int index = 0; index < inventoryComponentList.Count; index++)
            {
                Debug.Log(inventoryComponentList[index].ressourceType);
                totalAmount += inventoryComponentList[index].amount;
            }

            Debug.Log(totalAmount);
        }
    }
}
