using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Nastrond {
    public class AIManager : System {

        [Serializable]
        struct TileCost
        {
            public TileBase tileBase;
            public short cost;
        }

        [SerializeField] TileBase[] solidTiles;
        [SerializeField] TileCost[] costTiles;
        [SerializeField] Tilemap tilemap;
        [SerializeField] Tilemap buildingTilemap;
        
        public void Bake() {
            //Clean previous entity
            List<Entity> tmpEntities = FindObjectsOfType<Entity>().ToList();
            List<GameObject> entities = new List<GameObject>();

            foreach(Entity entity in tmpEntities) {
                if(!entities.Contains(entity.gameObject))
                    entities.Add(entity.gameObject);
            }

            
            List<GameObject> previousEntities = new List<GameObject>();

            foreach(GameObject e in entities) {
                if(e.GetComponent<GraphNodeComponent>()) {
                    previousEntities.Add(e);
                }
            }

            foreach (GameObject e in previousEntities) {
                DestroyImmediate(e);
            }
            
            //Get bounds
            int graphWidth = Mathf.Abs(tilemap.cellBounds.xMin - tilemap.cellBounds.xMax);
            int graphHeight = Mathf.Abs(tilemap.cellBounds.yMin - tilemap.cellBounds.yMax);

            tilemap.CompressBounds();

            //Build graph
            //Create new graph
            GameObject[,] graph = new GameObject[graphWidth, graphHeight];
            
            int indexX = 0;
            int indexY = 0;
            foreach(Vector3Int pos in tilemap.cellBounds.allPositionsWithin) {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                Vector3 place = tilemap.CellToWorld(localPlace);
                if(tilemap.HasTile(localPlace) && ! buildingTilemap.HasTile(localPlace)) {
                    if(!solidTiles.Contains(tilemap.GetTile(localPlace))) {
                        GameObject tmp = new GameObject {name = "Node[" + indexX + "." + indexY + "]"};
                        tmp.AddComponent<GraphNodeComponent>();
                        int cost = 0;
                        foreach (TileCost costTile in costTiles) {
                            if (costTile.tileBase == tilemap.GetTile(localPlace)) {
                                cost = costTile.cost;
                            }
                        }

                        tmp.GetComponent<GraphNodeComponent>().cost = cost;
                        tmp.GetComponent<GraphNodeComponent>().neighbors = new List<GameObject>();
                        tmp.AddComponent<Entity>();
                        tmp.transform.position = tilemap.GetCellCenterWorld(localPlace);
                        tmp.GetComponent<GraphNodeComponent>().position = tmp.transform;
                        graph[indexX, indexY] = tmp;

                        tmp.transform.SetParent(transform);
                    }
                }

                indexX++;
                if (indexX >= graph.GetLength((0))) {
                    indexX = 0;
                    indexY++;
                }
            }

            BoundsInt bounds = new BoundsInt(-1, -1, 0, 3, 3, 1);

            //Get All neighbors
            for(int x = 0;x < graphWidth;x++) {
                for(int y = 0;y < graphHeight;y++) {
                    //If is a walkable tile => Add neighbors
                    if (graph[x, y] == null) continue;

                    foreach(Vector3Int b in bounds.allPositionsWithin) {
                        //Check that b is not himself as a node
                        if(b.x == 0 && b.y == 0) continue;
                        //Check if b is inside bounds of graph
                        if(b.x + x < 0 || b.x + x >= graphWidth || b.y + y < 0 || b.y + y >= graphHeight) continue;
                        //Check if neighbors node is walkable
                        if(graph[x + b.x, y + b.y] == null) continue;
                        //Add cross without check
                        if(b.x == 0 || b.y == 0) {
                            graph[x, y].GetComponent<GraphNodeComponent>().neighbors.Add(graph[x + b.x, y + b.y]);
                        } else {//Else add only if both corner are free
                            if(graph[x, y + b.y] != null && graph[x + b.x, y] != null) {
                                graph[x, y].GetComponent<GraphNodeComponent>().neighbors.Add(graph[x + b.x, y + b.y]);
                            }
                        }
                    }
                }
            }

        }


    }

#if UNITY_EDITOR
    [CustomEditor(typeof(AIManager))]
    public class AIManagerEditor : Editor {
        AIManager aiManager;

        void OnEnable() {
            aiManager = (AIManager) target;
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            if (GUILayout.Button("Bake")) {
                aiManager.Bake();
            }
        }
    }
#endif
}