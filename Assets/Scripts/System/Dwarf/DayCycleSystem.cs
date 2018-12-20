using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nastrond {
    public class DayCycleSystem : System {
        //Systems
        AstarSystem aStarSystem;

        // Components
        PathComponent[] pathComponents;
        DwellingSlotIndexComponent[] dwellingSlotIndexComponents;
        WorkingSlotIndexComponent[] workingSlotIndexComponents;
        InventoryComponent[] inventoryComponents;
        Transform[] dwarfsTransformComponents;

        //TODO Remove to place inside a component
        public enum State {
            IDLE,
            HOME,
            WORK
        }

        public State state = State.IDLE;
        float timer = 0;

        float idleStateDuration = 1f;
        float homeStateDuration = 1f;
        float workStateDuration = 60f;

        void Start() {
            List<PathComponent> tmpPathComponents = new List<PathComponent>();
            List<DwellingSlotIndexComponent> tmpDwellingSlotIndexComponents = new List<DwellingSlotIndexComponent>();
            List<WorkingSlotIndexComponent> tmpWorkingSlotIndexComponents = new List<WorkingSlotIndexComponent>();
            List<Transform> tmpTransformComponents = new List<Transform>();
            List<InventoryComponent> tmpInventoryComponents = new List<InventoryComponent>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<PathComponent>() != null && 
                   e.GetComponent<DwellingSlotIndexComponent>() != null && 
                   e.GetComponent<WorkingSlotIndexComponent>() != null) {
                    tmpPathComponents.Add(e.GetComponent<PathComponent>());
                    tmpDwellingSlotIndexComponents.Add(e.GetComponent<DwellingSlotIndexComponent>());
                    tmpWorkingSlotIndexComponents.Add(e.GetComponent<WorkingSlotIndexComponent>());
                    tmpTransformComponents.Add(e.GetComponent<Transform>());
                    if (e.GetComponent<InventoryComponent>()) {
                        tmpInventoryComponents.Add(e.GetComponent<InventoryComponent>());
                    }
                    else {
                        tmpInventoryComponents.Add(null);
                    }
                }
            }

            pathComponents = tmpPathComponents.ToArray();
            dwellingSlotIndexComponents = tmpDwellingSlotIndexComponents.ToArray();
            workingSlotIndexComponents = tmpWorkingSlotIndexComponents.ToArray();
            dwarfsTransformComponents = tmpTransformComponents.ToArray();
            inventoryComponents = tmpInventoryComponents.ToArray();

            aStarSystem = FindObjectOfType<AstarSystem>();
        }

        void Update() {
            timer += Time.deltaTime;

            switch(state) {
                case State.IDLE:
                    if(timer > idleStateDuration) {
                        state = State.HOME;
                        timer = 0;
                        SendDwarfsToHome();
                    }
                    break;
                case State.HOME:
                    if(timer > homeStateDuration) {
                        state = State.WORK;
                        timer = 0;
                        SendDwarfsToWork();
                    }
                    break;
                case State.WORK:
                    if(timer > workStateDuration) {
                        state = State.HOME;
                        timer = 0;
                        SendDwarfsToHome();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.Log(state);
        }

        void SendDwarfsToHome() {
            for (int index = 0; index < pathComponents.Length; index++) {
                PathComponent pathComponent = pathComponents[index];
                DwellingSlotIndexComponent dwellingSlotIndexComponent = dwellingSlotIndexComponents[index];
                Transform transformComponent = dwarfsTransformComponents[index];
                InventoryComponent inventoryComponent = inventoryComponents[index];

                if (dwellingSlotIndexComponent.dwarfsSlots == null) {
                    continue;
                }

                if (pathComponent.index != 0 && pathComponent.dwarfsSlots[pathComponent.index - 1] != null) {
                    pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsAlreadyIn--;
                    pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsInside
                        .Remove(inventoryComponent);
                }

                pathComponent.nodes = aStarSystem.GetPath(transformComponent, dwellingSlotIndexComponent.dwarfsSlots.transform);
                pathComponent.index = 0;
                pathComponent.dwarfsSlots = new DwarfsSlots[pathComponent.nodes.Length];
                pathComponent.dwarfsSlots[pathComponent.nodes.Length - 1] = dwellingSlotIndexComponent.dwarfsSlots;
            }
        }

        void SendDwarfsToWork() {
            for(int index = 0;index < pathComponents.Length;index++) {
                PathComponent pathComponent = pathComponents[index];
                WorkingSlotIndexComponent workingSlotIndexComponent = workingSlotIndexComponents[index];
                Transform transformComponent = dwarfsTransformComponents[index];
                InventoryComponent inventoryComponent = inventoryComponents[index];

                if(workingSlotIndexComponent.dwarfsSlots == null) {
                    continue;
                }

                if(pathComponent.index != 0 && pathComponent.dwarfsSlots[pathComponent.index - 1] != null) {
                    pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsAlreadyIn--;
                    pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsInside
                        .Remove(inventoryComponent);
                }

                pathComponent.nodes = aStarSystem.GetPath(transformComponent, workingSlotIndexComponent.dwarfsSlots.transform);
                pathComponent.index = 0;
                pathComponent.dwarfsSlots = new DwarfsSlots[pathComponent.nodes.Length];
                pathComponent.dwarfsSlots[pathComponent.nodes.Length -1] = workingSlotIndexComponent.dwarfsSlots;
            }
        }

        public void AddEntity(GameObject entity)
        {
            List<PathComponent> newPathList = pathComponents.ToList();
            if(entity.GetComponent<PathComponent>()) {
                newPathList.Add(entity.GetComponent<PathComponent>());
            }

            pathComponents = newPathList.ToArray();

            List<Transform> newTransformList = dwarfsTransformComponents.ToList();
            if(entity.GetComponent<Transform>()) {
                newTransformList.Add(entity.GetComponent<Transform>());
            }

            dwarfsTransformComponents = newTransformList.ToArray();

            List<InventoryComponent> newInventoryList = inventoryComponents.ToList();
            if(entity.GetComponent<InventoryComponent>()) {
                newInventoryList.Add(entity.GetComponent<InventoryComponent>());
            }

            inventoryComponents = newInventoryList.ToArray();

            List<DwellingSlotIndexComponent> newDeDwellingSlotIndexComponents = dwellingSlotIndexComponents.ToList();
            if (entity.GetComponent<DwellingSlotIndexComponent>()) {
                newDeDwellingSlotIndexComponents.Add(entity.GetComponent<DwellingSlotIndexComponent>());
            }

            dwellingSlotIndexComponents = newDeDwellingSlotIndexComponents.ToArray();

            List<WorkingSlotIndexComponent> newWorkingSlotIndexComponents = workingSlotIndexComponents.ToList();
            if(entity.GetComponent<WorkingSlotIndexComponent>()) {
                newWorkingSlotIndexComponents.Add(entity.GetComponent<WorkingSlotIndexComponent>());
            }

            workingSlotIndexComponents = newWorkingSlotIndexComponents.ToArray();
        }
    }
}
