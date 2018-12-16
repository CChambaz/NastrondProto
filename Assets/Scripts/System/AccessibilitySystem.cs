using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class AccessibilitySystem : System
    {

        List<AccessibilityComponent> accessibilityComponentList;

        // Start is called before the first frame update
        void Start()
        {
            accessibilityComponentList = new List<AccessibilityComponent>();
            List<GameObject> tmpEntities = GetEntities();

            foreach (GameObject e in tmpEntities)
            {
                if (e.GetComponent<AccessibilityComponent>())
                {
                    accessibilityComponentList.Add(e.GetComponent<AccessibilityComponent>());
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int index = 0; index < accessibilityComponentList.Count; index++)
            {
                Debug.Log("Building resource fluid type is " + accessibilityComponentList[index].resourceFluidType);
                Debug.Log("accessibility is " + accessibilityComponentList[index].accessibility);
            }
        }
    }
}

//AccessibilitySystem
