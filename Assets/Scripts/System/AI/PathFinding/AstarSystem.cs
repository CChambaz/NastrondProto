using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class AstarSystem : System {

        List<GraphNodeComponent> graphNodeComponents;
        List<Transform> transformComponents;

        // Use this for initialization
        void Start() {
            transformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<GraphNodeComponent>()) {
                    transformComponents.Add(e.transform);
                    graphNodeComponents.Add(e.GetComponent<GraphNodeComponent>());
                }
            }
        }

        public void GetPath(Transform transformOrigin, Transform transformTarget) {
            GraphNodeComponent nodeOrigin = GetClosestNodeComponent(transformOrigin);
            GraphNodeComponent nodeTarget = GetClosestNodeComponent(transformTarget);
        }

        GraphNodeComponent GetClosestNodeComponent(Transform t) {
            GraphNodeComponent node = new GraphNodeComponent();

            return node;
        }
    }
}
