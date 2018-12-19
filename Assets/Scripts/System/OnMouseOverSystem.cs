using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Nastrond
{
    public class OnMouseOverSystem : System
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

/*
 
RectTransform map;

public bool isMouseOverMap()
{
    Vector2 mousePosition = Input.mousePosition;
    Vector3[] worldCorners = new Vector3[4];
    map.GetWorldCorners(worldCorners);
    (sprite.getBounds())

    if (mousePosition.x >= worldCorners[0].x && mousePosition.x < worldCorners[2].x
    && mousePosition.y >= worldCorners[0].y && mousePosition.y < worldCorners[2].y)
    {
        return true;
    }
    else
    {
        return false;
    }    
}

 */


