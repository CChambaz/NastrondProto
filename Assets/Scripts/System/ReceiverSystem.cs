using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{
    public class ReceiverSystem : System
    {

        List<DwarfToolComponent> receiverComponentList;

        // Start is called before the first frame update
        void Start()
        {
            receiverComponentList = new List<DwarfToolComponent>();
            List<GameObject> tmpEntities = GetEntities();

            foreach (GameObject e in tmpEntities)
            {
                if (e.GetComponent<DwarfToolComponent>())
                {
                    receiverComponentList.Add(e.GetComponent<DwarfToolComponent>());
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int index = 0; index < receiverComponentList.Count; index++)
            {
                Debug.Log("receiver resource is " + receiverComponentList[index].dwarftool);
                Debug.Log("amount : " + receiverComponentList[index].durability);
            }
        }
    }
}


