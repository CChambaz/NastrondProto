using System.Collections.Generic;
using Priority_Queue;
using UnityEngine;

namespace Nastrond {
    public class AstarSystem : System {

        GraphNodeComponent[] graphNodeComponents;
        Transform[] transformComponents;

        // Use this for initialization
        void Start()
        {
            List<GraphNodeComponent> tmpGraphNodeComponents = new List<GraphNodeComponent>();
            List<Transform> tmpTransformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<GraphNodeComponent>() != null) {
                    tmpTransformComponents.Add(e.transform);
                    tmpGraphNodeComponents.Add(e.GetComponent<GraphNodeComponent>());
                }
            }

            transformComponents = tmpTransformComponents.ToArray();
            graphNodeComponents = tmpGraphNodeComponents.ToArray();
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

            for (int index = 0; index < transformComponents.Length; index++) {
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
                    float distance = Vector2.Distance(current.position.position, currentNeighbor.position.position);

                    float newCost = costSoFar[current] + current.cost + distance;

                    if(!costSoFar.ContainsKey(currentNeighbor) || newCost < costSoFar[currentNeighbor]) {
                        float priority = newCost + Vector2.Distance(currentNeighbor.position.position, nodeTarget.position.position);
                        openNode.Enqueue(currentNeighbor, priority);


                        costSoFar[currentNeighbor] = newCost;
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