using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nastrond
{

    public class ResourceCounterSystem : System
    {
        public Text StoneText;
        public Text FoodText;
        private int counter;

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
            int totalAmountFood = 0;

            int totalAmountStone = 0;

            for (int index = 0; index < inventoryComponentList.Count; index++)
            {
                if (inventoryComponentList[index].resourceType == ResourceType.FOOD)
                {
                    totalAmountFood += inventoryComponentList[index].amount;
                }
                else
                {
                    totalAmountStone += inventoryComponentList[index].amount;
                }
            }

            StoneText.text = "Stone amount : " + totalAmountStone;
            FoodText.text = "Food amount : " + totalAmountFood;
            Debug.Log("total food amount : " + totalAmountFood);
            Debug.Log("total stone amount : " + totalAmountStone);
        }

        public void ShowResources(bool newValue)
        {
            counter++;
            if (counter % 2 == 1)
            {
                StoneText.color = new Color(255f, 0f, 0f, 255f);
                FoodText.color = new Color(255f, 0f, 0f, 255f);
            }
            else
            {
                StoneText.color = new Color(255f, 0f, 0f, 0f);
                FoodText.color = new Color(255f, 0f, 0f, 0f);
            }
        }
    }
}


//ResourceCounterSystem