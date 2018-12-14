using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Nastrond
{
    public class PathFollowSystem : System
    {
        List<PathComponent> pathComponents;
        List<MotionComponent> motionComponents;
        List<Transform> transformComponents;

        // Start is called before the first frame update
        void Start()
        {
            pathComponents = new List<PathComponent>();
            motionComponents = new List<MotionComponent>();
            transformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<MotionComponent>() != null && 
                   e.GetComponent<PathComponent>() != null) {
                    motionComponents.Add(e.GetComponent<MotionComponent>());
                    pathComponents.Add(e.GetComponent<PathComponent>());
                    transformComponents.Add(e.transform);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int index = 0; index < motionComponents.Count; index++) {
                MotionComponent motionComponent = motionComponents[index];
                PathComponent pathComponent = pathComponents[index];
                Transform transformComponent = transformComponents[index];

                if (pathComponent.nodes.Length == 0) {
                    continue;
                }

                //Test distance
                if (Vector2.Distance(transformComponent.position,
                        pathComponent.nodes[pathComponent.index].position.position) < 0.5f) {
                    pathComponent.index++;
                    if (pathComponent.index >= pathComponent.nodes.Length) {
                        pathComponent.index = 0;
                        pathComponent.nodes = new GraphNodeComponent[0];
                        motionComponent.direction = Vector2.zero;
                        continue;
                    }
                }

                motionComponent.direction = pathComponent.nodes[pathComponent.index].position.position - transformComponent.position;
            }
        }
    }
}
