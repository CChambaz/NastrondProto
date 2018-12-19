using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class CarrierManager : System {
        //System
        AstarSystem aStarSystem;

        //Dwarfs
        PathComponent[] pathComponents;
        Transform[] dwarfsTransformComponents;
        InventoryComponent[] dwarfsInventoryComponents;

        //Building Giver
        InventoryComponent[] giverBuildingInventoryComponents;
        DwarfsSlots[] giverDwarfsSlotsComponents;
        Transform[] giverBuildingTransformsComponents;
        GiverComponent[] giverComponents;

        //Building Receiver
        InventoryComponent[] receiverBuildingInventoryComponents;
        DwarfsSlots[] receiverDwarfsSlotsComponents;
        Transform[] receiverBuildingsTransformsComponents;
        GiverComponent[] receiverComponents;

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
            List<GiverComponent> tmpReceiverComponents = new List<GiverComponent>();

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
                    tmpReceiverComponents.Add(e.GetComponent<GiverComponent>());
                    tmpReceiverBuildingsTransformsComponents.Add(e.GetComponent<Transform>());
                }
            }

            //Dwarfs
            pathComponents = tmpPathComponents.ToArray();
            dwarfsTransformComponents = tmpDwarfsTransformComponents.ToArray();
            dwarfsInventoryComponents = tmpDwarfsInventoryComponents.ToArray();

            //Giver
            giverBuildingInventoryComponents = tmpGiverBuildingInventoryComponents.ToArray();
            giverDwarfsSlotsComponents = tmpGiverDwarfsSlotsComponents.ToArray();
            giverComponents = tmpGiverComponents.ToArray();
            giverBuildingTransformsComponents = tmpGiverBuildingsTransformsComponents.ToArray();

            //Receiver
            receiverBuildingInventoryComponents = tmpReceiverBuildingInventoryComponents.ToArray();
            receiverDwarfsSlotsComponents = tmpReceiverDwarfsSlotsComponents.ToArray();
            receiverComponents = tmpReceiverComponents.ToArray();
            receiverBuildingsTransformsComponents = tmpReceiverBuildingsTransformsComponents.ToArray();

            aStarSystem = FindObjectOfType<AstarSystem>();
        }
        
        void Update()
        {
            //Get
            for (int index = 0; index < giverComponents.Length; index++) {
                GiverComponent giverComponent = giverComponents[index];
                if (giverComponent.nbDwarfsAttributed >= 1) continue;


                for (int i = 0; i < pathComponents.Length; i++) {
                    PathComponent pathComponent = pathComponents[i];
                    InventoryComponent inventoryComponent = dwarfsInventoryComponents[i];
                    if (pathComponent.nodes.Length == 0 && inventoryComponent.amount == 0) {

                        Transform giverBuildingTransformsComponent = giverBuildingTransformsComponents[index];
                        DwarfsSlots giverDwarfsSlots = giverDwarfsSlotsComponents[index];

                        Transform dwarfsTransform = dwarfsTransformComponents[i];

                        if(pathComponent.index != 0 && pathComponent.dwarfsSlots[pathComponent.index - 1] != null) {
                            pathComponent.dwarfsSlots[pathComponent.index - 1].dwarfsAlreadyIn--;
                        }

                        pathComponent.nodes = aStarSystem.GetPath(dwarfsTransform, giverBuildingTransformsComponent);
                        pathComponent.dwarfsSlots = new DwarfsSlots[pathComponent.nodes.Length];
                        pathComponent.dwarfsSlots[pathComponent.nodes.Length - 1] = giverDwarfsSlots;
                        pathComponent.index = 0;
                    }
                }
            }


        }
    }
}
