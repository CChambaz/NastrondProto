using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{
    public class BonusSystem : System
    {

        List<DwarfToolComponent> dwarfTooLComponentList;

        // Start is called before the first frame update
        void Start()
        {
            dwarfTooLComponentList = new List<DwarfToolComponent>();
            List<GameObject> tmpEntities = GetEntities();

            foreach (GameObject e in tmpEntities)
            {
                if (e.GetComponent<DwarfToolComponent>())
                {
                    dwarfTooLComponentList.Add(e.GetComponent<DwarfToolComponent>());
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int index = 0; index < dwarfTooLComponentList.Count; index++)
            {
                Debug.Log("dwarf tool quality is " + dwarfTooLComponentList[index].dwarftool);
                Debug.Log("dwarf tool durability left : " + dwarfTooLComponentList[index].durability);
            }
        }
    }    
}

