using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class FollowCursorSystem : System
    {
        PathComponent[] pathComponents;
        Transform[] transformComponents;

        AstarSystem aStarSystem;

        void Start()
        {
            List<PathComponent> tmpPathComponents = new List<PathComponent>();
            List<Transform> tmpTransformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<PathComponent>() != null) {
                    tmpPathComponents.Add(e.GetComponent<PathComponent>());
                    tmpTransformComponents.Add(e.GetComponent<Transform>());
                }
            }

            pathComponents = tmpPathComponents.ToArray();
            transformComponents = tmpTransformComponents.ToArray();
            
            aStarSystem = FindObjectOfType<AstarSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1")) {
                Vector2 v2 = Input.mousePosition;
                v2 = Camera.main.ScreenToWorldPoint(v2);

                float time = Time.realtimeSinceStartup;
                for (int index = 0; index < pathComponents.Length; index++) {
                    PathComponent pathComponent = pathComponents[index];
                    Transform transformComponent = transformComponents[index];

                    if(pathComponent.index != 0 && pathComponent.dwarfsSlots[pathComponent.index - 1] != null) {
                        pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsAlreadyIn--;
                    }

                    pathComponent.nodes = aStarSystem.GetPath(transformComponent, v2);
                    pathComponent.index = 0;
                    pathComponent.dwarfsSlots = new DwarfsSlots[pathComponent.nodes.Length];
                }
                Debug.Log("time = " + (Time.realtimeSinceStartup - time));
            }
        }
    }
}

