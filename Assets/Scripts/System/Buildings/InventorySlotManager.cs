using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class InventorySlotManager:System {
        //Buildings with a dwarfSlot type = INVENTORY
        DwarfsSlots[] dwarfsSlotsComponents;
        GiverComponent[][] giverComponents;
        ReceiverComponent[][] receiverComponents;
        PassiveInventoryComponent[][] passiveInventoryComponents;
        
        // Start is called before the first frame update
        void Start() {
            List<DwarfsSlots> tmpDwarfsSlotComponents = new List<DwarfsSlots>();
            List<GiverComponent[]> tmpGiverComponents = new List<GiverComponent[]>();
            List<ReceiverComponent[]> tmpReceiverComponents = new List<ReceiverComponent[]>();
            List<PassiveInventoryComponent[]> tmpPassiveInventoryComponents = new List<PassiveInventoryComponent[]>();

            List<GameObject> tmpEntities = GetEntities();

            foreach (GameObject tmpEntity in tmpEntities) {
                if (tmpEntity.GetComponent<DwarfsSlots>()) {
                    if (tmpEntity.GetComponent<DwarfsSlots>().buildingType == DwarfsSlots.BuildingType.INVENTORY) {
                        tmpDwarfsSlotComponents.Add(tmpEntity.GetComponent<DwarfsSlots>());

                        if (tmpEntity.GetComponent<GiverComponent>()) {
                            tmpGiverComponents.Add(tmpEntity.GetComponents<GiverComponent>());
                        }
                        else {
                            tmpGiverComponents.Add(null);
                        }

                        if(tmpEntity.GetComponent<ReceiverComponent>()) {
                            tmpReceiverComponents.Add(tmpEntity.GetComponents<ReceiverComponent>());
                        } else {
                            tmpReceiverComponents.Add(null);
                        }

                        if(tmpEntity.GetComponent<PassiveInventoryComponent>()) {
                            tmpPassiveInventoryComponents.Add(tmpEntity.GetComponents<PassiveInventoryComponent>());
                        } else {
                            tmpPassiveInventoryComponents.Add(null);
                        }
                    }
                }
            }

            dwarfsSlotsComponents = tmpDwarfsSlotComponents.ToArray();
            giverComponents = tmpGiverComponents.ToArray();
            receiverComponents = tmpReceiverComponents.ToArray();
            passiveInventoryComponents = tmpPassiveInventoryComponents.ToArray();
        }

        // Update is called once per frame
        void Update() {
            for (int index = 0; index < dwarfsSlotsComponents.Length; index++) {
                DwarfsSlots dwarfsSlotsComponent = dwarfsSlotsComponents[index];

                if (dwarfsSlotsComponent.dwarfsInside.Count <= 0) {
                    continue;
                }

                foreach (InventoryComponent inventoryComponent in dwarfsSlotsComponent.dwarfsInside) {
                    bool found = false;

                    foreach (InventoryComponent component in dwarfsSlotsComponent.dwarfsInside) {
                        if (component == inventoryComponent) {
                            found = true;
                            break;
                        }
                    }

                    if (!found) {
                        return;
                    }

                    GiverComponent[] giverComponent = giverComponents[index];
                    ReceiverComponent[] receiverComponent = receiverComponents[index];
                    PassiveInventoryComponent[] passiveInventoryComponent = passiveInventoryComponents[index];

                    if (giverComponent != null && giverComponent.Length > 0) {
                        foreach (GiverComponent component in giverComponent) {
                            if (component.nbDwarfsAttributed > 0 && component.resourceType == inventoryComponent.resourceType) {
                                if(inventoryComponent.amount >= inventoryComponent.maxCapacity)
                                {
                                    continue;
                                }

                                if (component.amount < inventoryComponent.maxCapacity) {
                                    inventoryComponent.amount = component.amount;
                                }
                                else {
                                    inventoryComponent.amount = inventoryComponent.maxCapacity;
                                }
                                component.amount -= inventoryComponent.amount;
                                component.nbDwarfsAttributed--;
                                break;
                            }
                        }
                    }

                    if(receiverComponent != null && receiverComponent.Length > 0) {
                        foreach(ReceiverComponent component in receiverComponent) {
                            if(component.nbDwarfsAttributed > 0 && component.resourceType == inventoryComponent.resourceType) {
                                component.amount += inventoryComponent.amount;
                                inventoryComponent.amount = 0;
                                component.nbDwarfsAttributed--;
                                break;
                            }
                        }
                    }

                    if(passiveInventoryComponent != null && passiveInventoryComponent.Length > 0) {
                        foreach(PassiveInventoryComponent component in passiveInventoryComponent) {
                            if(component.resourceType == inventoryComponent.resourceType) {
                                if (inventoryComponent.amount == 0) {
                                    inventoryComponent.amount = inventoryComponent.maxCapacity;
                                    component.amount -= inventoryComponent.amount;
                                }
                                else {
                                    component.amount += inventoryComponent.amount;
                                    inventoryComponent.amount = 0;
                                }

                                dwarfsSlotsComponent.dwarfsInside.Remove(inventoryComponent);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
