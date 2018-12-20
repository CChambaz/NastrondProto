using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nastrond
{
    public class CarrierManager : System {
        //System
        AstarSystem aStarSystem;
        DayCycleSystem dayCycleSystem;

        //Dwarfs
        PathComponent[] pathComponents;
        Transform[] dwarfsTransformComponents;
        InventoryComponent[] dwarfsInventoryComponents;
        DwarfsSlots[] dwarfsInventorySlotComponents;

        //Building Give
        DwarfsSlots[] giverDwarfsSlotComponents;
        Transform[] giverTransformComponents;
        GiverComponent[] giverComponents;

        //Building Receiver
        DwarfsSlots[] receiverDwarfsSlotComponents;
        Transform[] receiverTransformComponents;
        ReceiverComponent[] receiverComponents;

        //Building Storage
        DwarfsSlots[] passiveInventoryDwarfsSlotsComponents;
        Transform[] passiveInventoryTransformsComponents;
        public PassiveInventoryComponent[] passiveInventoryComponents;

        void Start() {
            //Dwarfs
            List<PathComponent> tmpPathComponents = new List<PathComponent>();
            List<Transform> tmpDwarfsTransformComponents = new List<Transform>();
            List<InventoryComponent> tmpDwarfsInventoryComponents = new List<InventoryComponent>();

            //Giver
            List<DwarfsSlots> tmpGiverDwarfsSlotsComponents = new List<DwarfsSlots>();
            List<Transform> tmpGiverBuildingsTransformsComponents = new List<Transform>();
            List<GiverComponent> tmpGiverComponents = new List<GiverComponent>();

            //Receiver
            List<DwarfsSlots> tmpReceiverDwarfsSlotsComponents = new List<DwarfsSlots>();
            List<Transform> tmpReceiverBuildingsTransformsComponents = new List<Transform>();
            List<ReceiverComponent> tmpReceiverComponents = new List<ReceiverComponent>();

            //Storage
            List<DwarfsSlots> tmpPassiveInventoryDwarfsSlotsComponents = new List<DwarfsSlots>();
            List<Transform> tmpPassiveInventoryTransformsComponents = new List<Transform>();
            List<PassiveInventoryComponent> tmpPassiveInventoryComponents = new List<PassiveInventoryComponent>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<PathComponent>() && e.GetComponent<InventoryComponent>()) {
                    tmpPathComponents.Add(e.GetComponent<PathComponent>());
                    tmpDwarfsTransformComponents.Add(e.GetComponent<Transform>());
                    tmpDwarfsInventoryComponents.Add((e.GetComponent<InventoryComponent>()));
                }else if (e.GetComponent<DwarfsSlots>() &&
                          e.GetComponent<GiverComponent>()) {
                    tmpGiverDwarfsSlotsComponents.Add(e.GetComponent<DwarfsSlots>());
                    tmpGiverComponents.Add(e.GetComponent<GiverComponent>());
                    tmpGiverBuildingsTransformsComponents.Add(e.GetComponent<Transform>());
                }else if (e.GetComponent<DwarfsSlots>() &&
                          e.GetComponent<ReceiverComponent>()) {
                    tmpReceiverDwarfsSlotsComponents.Add(e.GetComponent<DwarfsSlots>());
                    tmpReceiverComponents.Add(e.GetComponent<ReceiverComponent>());
                    tmpReceiverBuildingsTransformsComponents.Add(e.GetComponent<Transform>());
                }else if (e.GetComponent<PassiveInventoryComponent>() &&
                          e.GetComponent<DwarfsSlots>()) {
                    foreach (PassiveInventoryComponent passiveInventoryComponent in e.GetComponents<PassiveInventoryComponent>()) {
                        tmpPassiveInventoryComponents.Add(passiveInventoryComponent);
                        tmpPassiveInventoryDwarfsSlotsComponents.Add(e.GetComponent<DwarfsSlots>());
                        tmpPassiveInventoryTransformsComponents.Add(e.GetComponent<Transform>());
                    }
                }
            }

            //Dwarfs
            pathComponents = tmpPathComponents.ToArray();
            dwarfsTransformComponents = tmpDwarfsTransformComponents.ToArray();
            dwarfsInventoryComponents = tmpDwarfsInventoryComponents.ToArray();

            //Giver
            giverDwarfsSlotComponents = tmpGiverDwarfsSlotsComponents.ToArray();
            giverComponents = tmpGiverComponents.ToArray();
            giverTransformComponents = tmpGiverBuildingsTransformsComponents.ToArray();

            //Receiver
            receiverDwarfsSlotComponents = tmpReceiverDwarfsSlotsComponents.ToArray();
            receiverComponents = tmpReceiverComponents.ToArray();
            receiverTransformComponents = tmpReceiverBuildingsTransformsComponents.ToArray();

            //Storage
            passiveInventoryDwarfsSlotsComponents = tmpPassiveInventoryDwarfsSlotsComponents.ToArray();
            passiveInventoryTransformsComponents = tmpPassiveInventoryTransformsComponents.ToArray();
            passiveInventoryComponents = tmpPassiveInventoryComponents.ToArray();

            aStarSystem = FindObjectOfType<AstarSystem>();
            dayCycleSystem = FindObjectOfType<DayCycleSystem>();
        }
        
        void Update()
        {
            if (dayCycleSystem.state != DayCycleSystem.State.WORK) {
                return;
            }
            
            //Get ressource from giver then go the passiveInventory
            for (int index = 0; index < giverComponents.Length; index++) {
                GiverComponent giverComponent = giverComponents[index];
                if (giverComponent.nbDwarfsAttributed >= 1 || giverComponent.amount < 10) continue;
                
                for (int i = 0; i < pathComponents.Length; i++) {
                    PathComponent pathComponent = pathComponents[i];
                    if (pathComponent.nodes.Length != 0) continue;

                    Transform giverBuildingTransformsComponent = giverTransformComponents[index];
                    DwarfsSlots giverDwarfsSlots = giverDwarfsSlotComponents[index];

                    Transform dwarfsTransform = dwarfsTransformComponents[i];

                    //Then go to closet storage
                    int indexPassiveInventory = GetClosestStorageIndexFromBuilding(giverBuildingTransformsComponent.position, giverComponent.resourceType);

                    if (indexPassiveInventory == -1) {
                        continue;
                    }
                    InventoryComponent dwarfsInventoryComponent = dwarfsInventoryComponents[i];

                    if(pathComponent.index != 0 && pathComponent.dwarfsSlots[pathComponent.index - 1] != null) {
                        pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsAlreadyIn--;
                        pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsInside
                            .Remove(dwarfsInventoryComponent);

                    }

                    giverComponent.nbDwarfsAttributed++;
                    
                    dwarfsInventoryComponent.resourceType = giverComponent.resourceType;

                    //Go to giver
                    pathComponent.nodes = aStarSystem.GetPath(dwarfsTransform, giverBuildingTransformsComponent);
                    pathComponent.dwarfsSlots = new DwarfsSlots[pathComponent.nodes.Length];
                    pathComponent.dwarfsSlots[pathComponent.nodes.Length - 1] = giverDwarfsSlots;
                    pathComponent.index = 0;

                    //Then go to closet storage
                    GraphNodeComponent[] path2  = aStarSystem.GetPath(giverBuildingTransformsComponent, passiveInventoryTransformsComponents[indexPassiveInventory]);
                    DwarfsSlots[] slots2 = new DwarfsSlots[path2.Length];
                    slots2[path2.Length - 1] = passiveInventoryDwarfsSlotsComponents[indexPassiveInventory];

                    pathComponent.nodes = pathComponent.nodes.Concat(path2).ToArray();
                    pathComponent.dwarfsSlots = pathComponent.dwarfsSlots.Concat(slots2).ToArray();
                    break;
                }
            }

            //Get ressource from storage then go the receiver
            for(int index = 0;index < receiverComponents.Length;index++) {
                ReceiverComponent receiverComponent = receiverComponents[index];
                if(receiverComponent.nbDwarfsAttributed >= 1 || receiverComponent.amount >= receiverComponent.maxCapacity) continue;
                

                for(int i = 0;i < pathComponents.Length;i++) {
                    PathComponent pathComponent = pathComponents[i];
                    if (pathComponent.nodes.Length != 0 ) continue;

                    receiverComponent.nbDwarfsAttributed++;

                    Transform receiverBuildingTransformsComponent =  receiverTransformComponents[index];
                    DwarfsSlots receiverDwarfsSlots = receiverDwarfsSlotComponents[index];

                    Transform dwarfsTransform = dwarfsTransformComponents[i];

                    if(pathComponent.index != 0 && pathComponent.dwarfsSlots[pathComponent.index - 1] != null) {
                        pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsAlreadyIn--;
                    }

                    //Go to closet storage

                    //Then go to receiver
                    pathComponent.nodes = aStarSystem.GetPath(dwarfsTransform, receiverBuildingTransformsComponent);
                    pathComponent.dwarfsSlots = new DwarfsSlots[pathComponent.nodes.Length];
                    pathComponent.dwarfsSlots[pathComponent.nodes.Length - 1] = receiverDwarfsSlots;
                    pathComponent.index = 0;

                    break;
                }
            }
        }

        int GetClosestStorageIndexFromBuilding(Vector2 pos, ResourceType ressourceType) {
            int minIndex = -1;
            float minDistance = float.MaxValue;

            for (int index = 0; index < passiveInventoryTransformsComponents.Length; index++) {
                if (passiveInventoryComponents[index].resourceType != ressourceType) {
                    continue;
                }

                Transform passiveInventoryTransformsComponent = passiveInventoryTransformsComponents[index];
                float distance = Vector2.Distance(passiveInventoryTransformsComponent.position, pos);

                if (distance < minDistance) {
                    minDistance = distance;
                    minIndex = index;
                }
            }

            return minIndex;
        }
    }
}
