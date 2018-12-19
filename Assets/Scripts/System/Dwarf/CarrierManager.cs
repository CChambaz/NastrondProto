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

        //Building Giver
        InventoryComponent[] giverInventoryComponents;
        DwarfsSlots[] giverDwarfsSlotComponents;
        Transform[] giverTransformComponents;
        GiverComponent[] giverComponents;

        //Building Receiver
        InventoryComponent[] receiverInventoryComponents;
        DwarfsSlots[] receiverDwarfsSlotComponents;
        Transform[] receiverTransformComponents;
        ReceiverComponent[] receiverComponents;

        //Building Storage
        InventoryComponent[][] passiveInventoryInventoryComponents;
        DwarfsSlots[] passiveInventoryDwarfsSlotsComponents;
        Transform[] passiveInventoryTransformsComponents;
        PassiveInventoryComponent[] passiveInventoryComponents;

        void Start() {
            //Dwarfs
            List<PathComponent> tmpPathComponents = new List<PathComponent>();
            List<Transform> tmpDwarfsTransformComponents = new List<Transform>();
            List<InventoryComponent> tmpDwarfsInventoryComponents = new List<InventoryComponent>();

            //Giver
            List<InventoryComponent> tmpGiverBuildingInventoryComponents = new List<InventoryComponent>();
            List<DwarfsSlots> tmpGiverDwarfsSlotsComponents = new List<DwarfsSlots>();
            List<Transform> tmpGiverBuildingsTransformsComponents = new List<Transform>();
            List<GiverComponent> tmpGiverComponents = new List<GiverComponent>();

            //Receiver
            List<InventoryComponent> tmpReceiverBuildingInventoryComponents = new List<InventoryComponent>();
            List<DwarfsSlots> tmpReceiverDwarfsSlotsComponents = new List<DwarfsSlots>();
            List<Transform> tmpReceiverBuildingsTransformsComponents = new List<Transform>();
            List<ReceiverComponent> tmpReceiverComponents = new List<ReceiverComponent>();

            //Storage
            List<InventoryComponent[]> tmpPassiveInventoryInventoryInventoryComponents = new List<InventoryComponent[]>();
            List<DwarfsSlots> tmpPassiveInventoryDwarfsSlotsComponents = new List<DwarfsSlots>();
            List<Transform> tmpPassiveInventoryTransformsComponents = new List<Transform>();
            List<PassiveInventoryComponent> tmpPassiveInventoryComponents = new List<PassiveInventoryComponent>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<PathComponent>() && e.GetComponent<InventoryComponent>()) {
                    tmpPathComponents.Add(e.GetComponent<PathComponent>());
                    tmpDwarfsTransformComponents.Add(e.GetComponent<Transform>());
                    tmpDwarfsInventoryComponents.Add((e.GetComponent<InventoryComponent>()));
                }else if (e.GetComponent<InventoryComponent>() && 
                          e.GetComponent<DwarfsSlots>() &&
                          e.GetComponent<GiverComponent>()) {
                    tmpGiverBuildingInventoryComponents.Add(e.GetComponent<InventoryComponent>());
                    tmpGiverDwarfsSlotsComponents.Add(e.GetComponent<DwarfsSlots>());
                    tmpGiverComponents.Add(e.GetComponent<GiverComponent>());
                    tmpGiverBuildingsTransformsComponents.Add(e.GetComponent<Transform>());
                }else if (e.GetComponent<InventoryComponent>() &&
                          e.GetComponent<DwarfsSlots>() &&
                          e.GetComponent<ReceiverComponent>()) {
                    tmpReceiverBuildingInventoryComponents.Add(e.GetComponent<InventoryComponent>());
                    tmpReceiverDwarfsSlotsComponents.Add(e.GetComponent<DwarfsSlots>());
                    tmpReceiverComponents.Add(e.GetComponent<ReceiverComponent>());
                    tmpReceiverBuildingsTransformsComponents.Add(e.GetComponent<Transform>());
                }else if (e.GetComponent<PassiveInventoryComponent>() &&
                          e.GetComponent<DwarfsSlots>() && 
                          e.GetComponent<InventoryComponent>()) {
                    tmpPassiveInventoryComponents.Add(e.GetComponent<PassiveInventoryComponent>());
                    InventoryComponent[] inventories = e.GetComponents<InventoryComponent>();
                    tmpPassiveInventoryInventoryInventoryComponents.Add(inventories);
                    tmpPassiveInventoryDwarfsSlotsComponents.Add(e.GetComponent<DwarfsSlots>());
                    tmpPassiveInventoryTransformsComponents.Add(e.GetComponent<Transform>());
                }
            }

            //Dwarfs
            pathComponents = tmpPathComponents.ToArray();
            dwarfsTransformComponents = tmpDwarfsTransformComponents.ToArray();

            //Giver
            giverInventoryComponents = tmpGiverBuildingInventoryComponents.ToArray();
            giverDwarfsSlotComponents = tmpGiverDwarfsSlotsComponents.ToArray();
            giverComponents = tmpGiverComponents.ToArray();
            giverTransformComponents = tmpGiverBuildingsTransformsComponents.ToArray();

            //Receiver
            receiverInventoryComponents = tmpReceiverBuildingInventoryComponents.ToArray();
            receiverDwarfsSlotComponents = tmpReceiverDwarfsSlotsComponents.ToArray();
            receiverComponents = tmpReceiverComponents.ToArray();
            receiverTransformComponents = tmpReceiverBuildingsTransformsComponents.ToArray();

            //Storage
            passiveInventoryInventoryComponents = tmpPassiveInventoryInventoryInventoryComponents.ToArray();
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
                InventoryComponent giverInventoryComponent = giverInventoryComponents[index];
                if (giverComponent.nbDwarfsAttributed >= 1 || giverInventoryComponent.amount < 10) continue;
                
                for (int i = 0; i < pathComponents.Length; i++) {
                    PathComponent pathComponent = pathComponents[i];
                    if (pathComponent.nodes.Length != 0) continue;

                    giverComponent.nbDwarfsAttributed++;

                    Transform giverBuildingTransformsComponent = giverTransformComponents[index];
                    DwarfsSlots giverDwarfsSlots = giverDwarfsSlotComponents[index];

                    Transform dwarfsTransform = dwarfsTransformComponents[i];

                    if(pathComponent.index != 0 && pathComponent.dwarfsSlots[pathComponent.index - 1] != null) {
                        pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsAlreadyIn--;
                    }

                    //Go to giver
                    pathComponent.nodes = aStarSystem.GetPath(dwarfsTransform, giverBuildingTransformsComponent);
                    pathComponent.dwarfsSlots = new DwarfsSlots[pathComponent.nodes.Length];
                    pathComponent.dwarfsSlots[pathComponent.nodes.Length - 1] = giverDwarfsSlots;
                    pathComponent.index = 0;

                    //Then go to closet storage

                    break;
                }
            }

            //Get ressource from storage then go the receiver
            for(int index = 0;index < receiverComponents.Length;index++) {
                ReceiverComponent receiverComponent = receiverComponents[index];
                InventoryComponent receiverInventoryComponent = receiverInventoryComponents[index];
                if(receiverComponent.nbDwarfsAttributed >= 1 || receiverInventoryComponent.amount >= receiverInventoryComponent.maxCapacity) continue;


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
    }
}
