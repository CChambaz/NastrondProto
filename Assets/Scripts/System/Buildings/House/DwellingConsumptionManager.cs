using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwellingConsumptionManager : Nastrond.System
{
    private FoodInventory[] foodInventorys;

    private void Start()
    {
        Entity[] entitys = FindObjectsOfType<Entity>();
        List<FoodInventory> tmpFoodInventorys = new List<FoodInventory>();

        foreach (Entity entity in entitys)
        {
            FoodInventory tmpFoodInventory = entity.GetComponent<FoodInventory>();

            if (tmpFoodInventory != null)
            {
                tmpFoodInventorys.Add(tmpFoodInventory);
            }
        }

        foodInventorys = new FoodInventory[tmpFoodInventorys.Count];

        for (int i = 0; i < tmpFoodInventorys.Count; i++)
        {
            foodInventorys[i] = tmpFoodInventorys[i];
        }
    }
}
