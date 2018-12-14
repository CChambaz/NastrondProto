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
        void Start()
        {
            graphNodeComponents = new List<GraphNodeComponent>();
            transformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<GraphNodeComponent>() != null) {
                    transformComponents.Add(e.transform);
                    graphNodeComponents.Add(e.GetComponent<GraphNodeComponent>());
                }
            }
        }

        public GraphNodeComponent[] GetPath(Transform transformOrigin, Transform transformTarget) {
            GraphNodeComponent nodeOrigin = GetClosestNodeComponent(transformOrigin.position);
            GraphNodeComponent nodeTarget = GetClosestNodeComponent(transformTarget.position);

            return FindShortestPath(nodeOrigin, nodeTarget);
        }

        public GraphNodeComponent[] GetPath(Transform transformOrigin, Vector2 positionTarget) {
            GraphNodeComponent nodeOrigin = GetClosestNodeComponent(transformOrigin.position);
            GraphNodeComponent nodeTarget = GetClosestNodeComponent(positionTarget);

            return FindShortestPath(nodeOrigin, nodeTarget);
        }

        GraphNodeComponent GetClosestNodeComponent(Vector2 p) {
            float minDistance = float.MaxValue;
            int indexMin = 0;

            for (int index = 0; index < transformComponents.Count; index++) {
                Transform transformComponent = transformComponents[index];

                float currentDistance = Vector2.Distance(p, transformComponent.position);

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

            while(openNode.Count > 0) {
                current = openNode.Dequeue();

                if(current == nodeTarget) {
                    break;
                }

                foreach(GameObject neighbor in current.neighbors) {
                    GraphNodeComponent currentNeighbor = neighbor.GetComponent<GraphNodeComponent>();
                    float newCost = costSoFar[current] + current.cost;

                    if(!costSoFar.ContainsKey(currentNeighbor) || newCost < costSoFar[currentNeighbor]) {
                        costSoFar[currentNeighbor] = newCost;
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
