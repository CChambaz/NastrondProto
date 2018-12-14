using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class FollowCursorSystem : System
    {
        List<PathComponent> pathComponents;
        List<Transform> transformComponents;

        AstarSystem aStarSystem;

        void Start()
        {
            pathComponents = new List<PathComponent>();
            transformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<PathComponent>() != null) {
                    pathComponents.Add(e.GetComponent<PathComponent>());
                    transformComponents.Add(e.GetComponent<Transform>());
                }
            }
            
            aStarSystem = FindObjectOfType<AstarSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire1")) {
                float time = Time.realtimeSinceStartup;
                for (int index = 0; index < pathComponents.Count; index++) {
                    PathComponent pathComponent = pathComponents[index];
                    Transform transformComponent = transformComponents[index];

                    pathComponent.nodes = aStarSystem.GetPath(transformComponent, Vector2.zero);
                    pathComponent.index = 0;
                }
                Debug.Log("time = " + (Time.realtimeSinceStartup - time));
            }
        }
    }
}

