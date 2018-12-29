using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{
    public class GiverSystem : System
    {

        List<DwarfToolComponent> giverComponentList;

        // Start is called before the first frame update
        void Start()
        {
            giverComponentList = new List<DwarfToolComponent>();
            List<GameObject> tmpEntities = GetEntities();

            foreach (GameObject e in tmpEntities)
            {
                if (e.GetComponent<DwarfToolComponent>())
                {
                    giverComponentList.Add(e.GetComponent<DwarfToolComponent>());
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int index = 0; index < giverComponentList.Count; index++)
            {
                Debug.Log("giver resource is " + giverComponentList[index].dwarftool);
                Debug.Log("amount : " + giverComponentList[index].durability);
            }
        }
    }
}
