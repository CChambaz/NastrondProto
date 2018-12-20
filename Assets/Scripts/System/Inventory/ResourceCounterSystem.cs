using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nastrond
{
    public class ResourceCounterSystem : System
    {
        int foodAmount = 0;
        int stoneAmount = 0;
        int basalteAmount = 0;
        int ironAmount = 0;
        int coalAmount = 0;
        int toolAmount = 0;
 
        List<InventoryComponent> inventoryComponentList = new List<InventoryComponent>();

        // Update is called once per frame
        void Update()
        {
            int totalAmountFood = 0;
            int totalAmountStone = 0;
            int totalAmountBasalte = 0;
            int totalAmountIron = 0;
            int totalAmountCoal = 0;
            int totalAmountTool = 0;

            foreach(InventoryComponent i in inventoryComponentList)
            {
                switch(i.resourceType)
                {
                    case ResourceType.FOOD:
                        totalAmountFood += i.amount;
                        break;
                    case ResourceType.STONE:
                        totalAmountStone += i.amount;
                        break;
                    case ResourceType.IRON:
                        totalAmountIron += i.amount;
                        break;
                    case ResourceType.BASALT:
                        totalAmountBasalte += i.amount;
                        break;
                    case ResourceType.COAL:
                        totalAmountCoal += i.amount;
                        break;
                    case ResourceType.TOOL:
                        totalAmountTool += i.amount;
                        break;
                }
            }

            foodAmount = totalAmountFood;
            stoneAmount = totalAmountStone;
            basalteAmount = totalAmountBasalte;
            ironAmount = totalAmountIron;
            coalAmount = totalAmountCoal;
            toolAmount = totalAmountTool;
        }

        public void RegisterInventory(InventoryComponent i)
        {
            inventoryComponentList.Add(i);
        }

        public void UnregisterInventory(InventoryComponent i)
        {
            inventoryComponentList.Remove(i);
        }

        public int GetFoodAmount()
        {
            return foodAmount;
        }

        public int GetStoneAmount()
        {
            return stoneAmount;
        }

        public int GetBasaltAmount()
        {
            return basalteAmount;
        }

        public int GetIronAmount()
        {
            return ironAmount;
        }

        public int GetCoalAmount()
        {
            return coalAmount;
        }

        public int GetToolAmount()
        {
            return toolAmount;
        }
    }
}