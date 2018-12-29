using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Nastrond
{
    public class PathFollowSystem : System
    {
        PathComponent[] pathComponents;
        MotionComponent[] motionComponents;
        Transform[] transformComponents;
        InventoryComponent[] inventoryComponents;

        // Start is called before the first frame update
        void Start()
        {
            List<PathComponent> tmpPathComponents = new List<PathComponent>();
            List<MotionComponent> tmpMotionComponents = new List<MotionComponent>();
            List<Transform> tmpTransformComponents = new List<Transform>();
            List<InventoryComponent> tmpInventoryComponents = new List<InventoryComponent>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<MotionComponent>() != null && 
                   e.GetComponent<PathComponent>() != null) {
                    tmpMotionComponents.Add(e.GetComponent<MotionComponent>());
                    tmpPathComponents.Add(e.GetComponent<PathComponent>());
                    tmpTransformComponents.Add(e.transform);
                    if (e.GetComponent<InventoryComponent>()) {
                        tmpInventoryComponents.Add(e.GetComponent<InventoryComponent>());
                    }
                    else {
                        tmpInventoryComponents.Add(null);
                    }
                }
            }

            pathComponents = tmpPathComponents.ToArray();
            motionComponents = tmpMotionComponents.ToArray();
            transformComponents = tmpTransformComponents.ToArray();
            inventoryComponents = tmpInventoryComponents.ToArray();
        }

        // Update is called once per frame
        void Update()
        {
            for (int index = 0; index < motionComponents.Length; index++) {
                MotionComponent motionComponent = motionComponents[index];
                PathComponent pathComponent = pathComponents[index];
                Transform transformComponent = transformComponents[index];
                InventoryComponent inventoryComponent = inventoryComponents[index];

                if (pathComponent.nodes == null || pathComponent.nodes.Length == 0) {
                    continue;
                }

                //Test distance
                if (Vector2.Distance(transformComponent.position,
                        pathComponent.nodes[pathComponent.index].position.position) < 0.2f) {

                    if(pathComponent.index != 0 && pathComponent.dwarfsSlots[pathComponent.index - 1] != null) {
                        pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsAlreadyIn--;
                        if(inventoryComponent != null)
                        pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsInside.Remove(inventoryComponent);
                    }
                    if(pathComponent.index != 0 && pathComponent.dwarfsSlots[pathComponent.index] != null) {
                        pathComponent.dwarfsSlots[pathComponent.index].dwarfsAlreadyIn++;
                        if (inventoryComponent != null) {

                            pathComponent.dwarfsSlots[pathComponent.index].dwarfsInside.Add(inventoryComponent);
                        }
                    }
                    pathComponent.index++;
                    if (pathComponent.index >= pathComponent.nodes.Length) {
                        pathComponent.nodes = new GraphNodeComponent[0];
                        motionComponent.direction = Vector2.zero;
                        continue;
                    }
                }

                motionComponent.direction = (pathComponent.nodes[pathComponent.index].position.position - transformComponent.position).normalized;

                float maxCost = 5;
                float speed = motionComponent.maxSpeed / ((Mathf.Lerp(0, maxCost, pathComponent.nodes[pathComponent.index].cost) / maxCost) * 2);

                if (pathComponent.index > 0) {
                    if (Vector2.Distance(transformComponent.position,
                        pathComponent.nodes[pathComponent.index].position.position) > 
                        Vector2.Distance(transformComponent.position, 
                        pathComponent.nodes[pathComponent.index - 1].position.position)) {
                        speed = motionComponent.maxSpeed / ((Mathf.Lerp(0, maxCost, pathComponent.nodes[pathComponent.index - 1].cost) / maxCost) * 2);
                    }
                    else
                    {
                        speed = motionComponent.maxSpeed / ((Mathf.Lerp(0, maxCost, pathComponent.nodes[pathComponent.index].cost) / maxCost) * 2);
                    }
                } else {
                    speed = motionComponent.maxSpeed / ((Mathf.Lerp(0, maxCost, pathComponent.nodes[pathComponent.index].cost) / maxCost) * 2);
                }
                
                motionComponent.speed = Mathf.Clamp(speed, 0, motionComponent.maxSpeed);
            }
        }

        public void AddEntity(GameObject entity)
        {
            List<PathComponent> newPathList = pathComponents.ToList();
            if (entity.GetComponent<PathComponent>()) {
                newPathList.Add(entity.GetComponent<PathComponent>());
            }

            pathComponents = newPathList.ToArray();

            List<MotionComponent> newMotionList = motionComponents.ToList();
            if(entity.GetComponent<PathComponent>()) {
                newMotionList.Add(entity.GetComponent<MotionComponent>());
            }

            motionComponents = newMotionList.ToArray();

            List<Transform> newTransformList = transformComponents.ToList();
            if(entity.GetComponent<Transform>()) {
                newTransformList.Add(entity.GetComponent<Transform>());
            }

            transformComponents = newTransformList.ToArray();

            List<InventoryComponent> newInventoryList = inventoryComponents.ToList();
            if(entity.GetComponent<InventoryComponent>()) {
                newInventoryList.Add(entity.GetComponent<InventoryComponent>());
            }

            inventoryComponents = newInventoryList.ToArray();
        }
    }
}
