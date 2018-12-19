using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class SystemMove : System {
        private Vector3 cameraPosition;
        private List<ComponentMove> moveComponents;
        private List<ComponentMap> mapComponents;
        List<Transform> transformComponents;


        public void Start() {
            moveComponents = new List<ComponentMove>();
            mapComponents = new List<ComponentMap>();
            transformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            //Get Entity Contain ComponentMove
            foreach (GameObject e in tmpEntities) {
                if (e.GetComponent<ComponentMove>() == null || e.GetComponent<ComponentMap>() == null) {
                    continue;
                }

                moveComponents.Add(e.GetComponent<ComponentMove>());
                mapComponents.Add(e.GetComponent<ComponentMap>());
                transformComponents.Add(e.GetComponent<Transform>());
            }
        }

        public void Update() {
            /**
             * Ce n'est pas de l'ECS, il faut récupérer les informations de Display.main.rendering ailleurs. Dans le cas d'Unity ça suffit mais dans
             * SFGE ce ne sera pas possible de faire comme ça
             */
            Vector2 sizeDisplay = new Vector2(Display.main.renderingWidth, Display.main.renderingHeight);

            for (int index = 0; index < moveComponents.Count; index++) {
                ComponentMove moveComponent = moveComponents[index];
                ComponentMap mapComponent = mapComponents[index];
                Transform transformComponent = transformComponents[index];

                /**
                 * Ce n'est pas de l'ECS, mais après je n'ai pas d'idée de comment faire dans Unity
                 */
                Vector2 mousePosition = Input.mousePosition;

                if ((mousePosition.x >= 0 && mousePosition.x <= sizeDisplay.x)
                    && (mousePosition.y >= 0 && mousePosition.y <= sizeDisplay.y)) {
                    cameraPosition = transformComponent.position;

                    Vector2 dir = new Vector2();
                    if (cameraPosition.y > mapComponent.offSet.y) {
                        if (mousePosition.y < sizeDisplay.y / 100 * 15) {
                            dir.y = -1;
                        }
                    }

                    if (cameraPosition.y < mapComponent.offSet.y + mapComponent.sizeMap.y) {
                        if (mousePosition.y > sizeDisplay.y / 100 * 85) {
                            dir.y = 1;
                        }
                    }

                    if (cameraPosition.x > mapComponent.offSet.x) {
                        if (mousePosition.x < sizeDisplay.x / 100 * 15) {
                            dir.x = -1;
                        }
                    }

                    if (cameraPosition.x < mapComponent.offSet.x + mapComponent.sizeMap.x) {
                        if (mousePosition.x > sizeDisplay.x / 100 * 85) {
                            dir.x = 1;
                        }
                    }

                    transformComponent.position +=
                        (Vector3) dir * moveComponent.velocity * Time.deltaTime;
                }
            }
        }
    }
}