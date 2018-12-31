using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Nastrond
{
    public class MoveCameraSystem : System
    {
        private InputManager inputManager;

        private Vector3 cameraPosition;
        private List<ComponentMove> moveComponents;
        private List<ComponentMap> mapComponents;
        List<Transform> transformComponents;


        public void Start()
        {
            inputManager = GameObject.Find("Manager").GetComponent<InputManager>();

            moveComponents = new List<ComponentMove>();
            mapComponents = new List<ComponentMap>();
            transformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            //Get Entity Contain ComponentMove
            foreach (GameObject e in tmpEntities)
            {
                if (e.GetComponent<ComponentMove>() != null)
                {
                    moveComponents.Add(e.GetComponent<ComponentMove>());
                    transformComponents.Add(e.GetComponent<Transform>());
                }

                if (e.GetComponent<ComponentMap>() != null)
                {
                    mapComponents.Add(e.GetComponent<ComponentMap>());
                }
            }
        }

        private void MoveCamera(Vector2 dir, ComponentMap mapComponent , Transform transformComponent, float velocity)
        {
            //Check if camera position is in limit
            if (cameraPosition.y < mapComponent.offSet.y && dir.y < 0 
                || cameraPosition.y > mapComponent.offSet.y + mapComponent.sizeMap.y && dir.y > 0) {
                dir.y = 0;
            }

            if (cameraPosition.x < mapComponent.offSet.x && dir.x < 0 
                || cameraPosition.x > mapComponent.offSet.x + mapComponent.sizeMap.x && dir.x > 0) {
                dir.x = 0;
            }

            transformComponent.position +=
                (Vector3)dir * velocity * Time.deltaTime;
        }

        public void Update()
        {
            Vector2 dir = new Vector2();

            for (int index = 0; index < moveComponents.Count; index++)
            {
                ComponentMove moveComponent = moveComponents[index];
                ComponentMap mapComponent = mapComponents[index];
                Transform transformComponent = transformComponents[index];

                cameraPosition = transformComponent.position;
                
                //MouseInput
                dir = inputManager.GetDirCamera() * moveComponent.velocity;

                //KeyInput
                if (inputManager.KeyIsPress(keyCode: KeyCode.UpArrow))
                {
                    dir.y = 1f;
                }

                if (inputManager.KeyIsPress(keyCode: KeyCode.DownArrow))
                {
                    dir.y = -1f;
                }

                if (inputManager.KeyIsPress(keyCode: KeyCode.RightArrow))
                {
                    dir.x = 1f;
                }

                if (inputManager.KeyIsPress(keyCode: KeyCode.LeftArrow))
                {
                    dir.x = -1f;
                }

                if (inputManager.KeyIsPress(keyCode: KeyCode.LeftShift))
                {
                    dir *= moveComponent.velocity;
                }

                MoveCamera(dir, mapComponent, transformComponent, moveComponent.velocity);
            }
        }
    }
}