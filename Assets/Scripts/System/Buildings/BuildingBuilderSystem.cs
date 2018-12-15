using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Nastrond {
    public class BuildingBuilderSystem : System {

        [SerializeField] Tilemap tilemap;

        [Header("Tile for comparaison")]
        [SerializeField] TileBase houseLvl1;
        [SerializeField] TileBase houseLvl2;
        [SerializeField] TileBase houseLvl3;

        void Start() {
            BuildBuilding();
        }

        void BuildBuilding() {
            tilemap.CompressBounds();

            foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin) {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace)) {
                    TileBase currentTile = tilemap.GetTile(localPlace);

                    if (currentTile == houseLvl1) {
                        GameObject tmp = new GameObject("HouseLvl1[" + localPlace.x + ","+localPlace.y+"]");
                        tmp.transform.position = tilemap.GetCellCenterWorld(localPlace);
                        tmp.transform.parent = transform;
                        tmp.AddComponent<DwarfsSlots>();
                        tmp.GetComponent<DwarfsSlots>().buildingType = DwarfsSlots.BuildingType.DWELLING;
                        tmp.GetComponent<DwarfsSlots>().maxNumberSlots = 3;
                    } else if (currentTile == houseLvl2) {
                        GameObject tmp = new GameObject("HouseLvl2[" + localPlace.x + ","+localPlace.y+"]");
                        tmp.transform.position = tilemap.GetCellCenterWorld(localPlace);
                        tmp.transform.parent = transform;
                        tmp.AddComponent<DwarfsSlots>();
                        tmp.GetComponent<DwarfsSlots>().buildingType = DwarfsSlots.BuildingType.DWELLING;
                        tmp.GetComponent<DwarfsSlots>().maxNumberSlots = 5;
                    } else if (currentTile == houseLvl3) {
                        GameObject tmp = new GameObject("HouseLvl3[" + localPlace.x + ","+localPlace.y+"]");
                        tmp.transform.position = tilemap.GetCellCenterWorld(localPlace);
                        tmp.transform.parent = transform;
                        tmp.AddComponent<DwarfsSlots>();
                        tmp.GetComponent<DwarfsSlots>().buildingType = DwarfsSlots.BuildingType.DWELLING;
                        tmp.GetComponent<DwarfsSlots>().maxNumberSlots = 10;
                    }
                }
            }
        }
    }
}
