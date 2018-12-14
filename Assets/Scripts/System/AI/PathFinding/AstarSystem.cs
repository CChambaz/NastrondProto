using System.Collections;
using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

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

            //TODO faire du a* pour trouver le chemin le plus court
        }

        GraphNodeComponent GetClosestNodeComponent(Transform t) {
            float minDistance = float.MaxValue;
            int indexMin = 0;

            for (int index = 0; index < transformComponents.Count; index++) {
                Transform transformComponent = transformComponents[index];

                float currentDistance = Vector2.Distance(t.position, transformComponent.position);

                if (!(currentDistance < minDistance)) continue;

                minDistance = currentDistance;
                indexMin = index;
            }

            return graphNodeComponents[indexMin];
        }

        GraphNodeComponent[] FindShortestPath(GraphNodeComponent nodeOrigin, GraphNodeComponent nodeTarget)
        {
            List<GraphNodeComponent> path = new List<GraphNodeComponent>();

            SimplePriorityQueue<GraphNodeComponent> openNode = new SimplePriorityQueue<GraphNodeComponent>();

            Dictionary<GraphNodeComponent, float> costSoFar = new Dictionary<GraphNodeComponent, float>();
            Dictionary<GraphNodeComponent, GraphNodeComponent> cameFrom = new Dictionary<GraphNodeComponent, GraphNodeComponent>();

            costSoFar[nodeOrigin] = 0;
            cameFrom[nodeOrigin] = null;

            openNode.Enqueue(nodeOrigin, 0);

            GraphNodeComponent current = null;

            while (openNode.Count > 0) {
                current = openNode.First;

                if (current == nodeTarget) {
                    break;
                }

                foreach (GameObject neighbor in current.neighbors) {
                    GraphNodeComponent currentNeighbor = neighbor.GetComponent<GraphNodeComponent>();
                    float newCost = costSoFar[current] + current.cost;

                    if (!costSoFar.ContainsKey(current) || newCost < costSoFar[current]) {
                        costSoFar[current] = newCost;
                        float priority = newCost + Vector2.Distance(transformComponents[graphNodeComponents.IndexOf(currentNeighbor)].position, transformComponents[graphNodeComponents.IndexOf(current)].position);
                        openNode.Enqueue(currentNeighbor, priority);
                        cameFrom[currentNeighbor] = current;
                    }
                }
            }

            current = nodeTarget;
            while (current != nodeOrigin) {
                path.Add(current);
                current = cameFrom[current];
            }

            path.Add(nodeOrigin);
            path.Reverse();

            return path.ToArray();
        }
    }
}
