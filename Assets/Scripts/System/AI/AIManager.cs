using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Nastrond {
    public class AIManager : System {

        [SerializeField] List<TileBase> solidTiles;
        [SerializeField] Tilemap tilemap;



        public void Bake() {
            //Clean previous entity
            List<GraphNodeComponent> previousGraphNodeComponents = new List<GraphNodeComponent>();

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
            int graphWidth = 0;
            int graphHeight = 0;
            Vector2Int graphOrigin = Vector2Int.zero; //Bottom left corner

            tilemap.CompressBounds();

            graphWidth = tilemap.size.x;
            graphHeight = tilemap.size.y;

            graphOrigin = new Vector2Int(tilemap.origin.x, tilemap.origin.y);

            //Build graph
            //Create new graph
            GameObject[,] graph;

            graph = new GameObject[graphWidth, graphHeight];

            int nbNodePerTile = 1;

            //Assign all existing tile
            for(int x = 0;x < graphWidth;x++) {
                for(int y = 0;y < graphHeight;y++) {

                    float currentCost = 0;

                    bool isWalkableTile = false;

                    //Get the tile associated to the node
                    Vector3 nodePos = new Vector3(graphOrigin.x, graphOrigin.y, 0) + new Vector3(
                                      x ,
                                      y, 0);

                    Vector3Int tilePos = tilemap.LocalToCell(new Vector3(nodePos.x, nodePos.y, 0));
                    

                    if(tilemap.HasTile(tilePos)) {
                        if(!solidTiles.Contains(tilemap.GetTile(tilePos))) {
                            GameObject tmp = new GameObject();
                            tmp.name = "Node[" + x + "." + y + "]";
                            tmp.AddComponent<GraphNodeComponent>();
                            tmp.GetComponent<GraphNodeComponent>().cost = 1;
                            tmp.GetComponent<GraphNodeComponent>().neighbors = new List<GameObject>();
                            tmp.AddComponent<Entity>();
                            tmp.transform.position = new Vector2(tilePos.x + 0.5f, tilePos.y + 0.5f);

                            //GameObject instance = GameObject.Instantiate(tmp, new Vector2(x, y), Quaternion.identity);

                            graph[x, y] = tmp;
                        }
                    }
                }
            }

            BoundsInt bounds = new BoundsInt(-1, -1, 0, 3, 3, 1);

            //Get All neighbors
            for(int x = 0;x < graphWidth;x++) {
                for(int y = 0;y < graphHeight;y++) {
                    //If is a walkable tile => Add neighbors
                    if(graph[x, y] == null) continue;

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
