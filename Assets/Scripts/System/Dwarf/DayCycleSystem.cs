using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class DayCycleSystem : System {
        //Systems
        AstarSystem aStarSystem;

        // Components
        PathComponent[] pathComponents;
        DwellingSlotIndexComponent[] dwellingSlotIndexComponents;
        WorkingSlotIndexComponent[] workingSlotIndexComponents;
        Transform[] dwarfsTransformComponents;

        //TODO Remove to place inside a component
        enum State {
            IDLE,
            HOME,
            WORK
        }

        State state = State.IDLE;
        float timer = 0;

        float idleStateDuration = 1f;
        float homeStateDuration = 60f;
        float workStateDuration = 60f;

        void Start() {
            List<PathComponent> tmpPathComponents = new List<PathComponent>();
            List<DwellingSlotIndexComponent> tmpDwellingSlotIndexComponents = new List<DwellingSlotIndexComponent>();
            List<WorkingSlotIndexComponent> tmpWorkingSlotIndexComponents = new List<WorkingSlotIndexComponent>();
            List<Transform> tmpTransformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<PathComponent>() != null && 
                   e.GetComponent<DwellingSlotIndexComponent>() != null && 
                   e.GetComponent<WorkingSlotIndexComponent>() != null) {
                    tmpPathComponents.Add(e.GetComponent<PathComponent>());
                    tmpDwellingSlotIndexComponents.Add(e.GetComponent<DwellingSlotIndexComponent>());
                    tmpWorkingSlotIndexComponents.Add(e.GetComponent<WorkingSlotIndexComponent>());
                    tmpTransformComponents.Add(e.GetComponent<Transform>());
                }
            }

            pathComponents = tmpPathComponents.ToArray();
            dwellingSlotIndexComponents = tmpDwellingSlotIndexComponents.ToArray();
            workingSlotIndexComponents = tmpWorkingSlotIndexComponents.ToArray();
            dwarfsTransformComponents = tmpTransformComponents.ToArray();

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
                    }
                    break;
                case State.WORK:
                    if(timer > workStateDuration) {
                        state = State.HOME;
                        timer = 0;
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

                if (pathComponent.dwarfsSlotDestination != null) {
                    pathComponent.dwarfsSlotDestination.dwarfsAlreadyIn--;
                }

                pathComponent.nodes = aStarSystem.GetPath(transformComponent, dwellingSlotIndexComponent.dwarfsSlots.transform);
                pathComponent.index = 0;
                pathComponent.dwarfsSlotDestination = dwellingSlotIndexComponent.dwarfsSlots;
            }
        }
    }
}
