using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond
{
    public class SystemZoom : System
    {
        private ComponentZoom zoomComponent;
        private Camera camera;

        public void Start()
        {
            zoomComponent = new ComponentZoom();
            List<GameObject> tmpEntities = GetEntities();

            //Get Entity Contain ComponentMove
            foreach (GameObject e in tmpEntities)
            {
                if (e.GetComponent<ComponentZoom>() != null)
                {
                    zoomComponent = e.GetComponent<ComponentZoom>();
                    camera = e.GetComponent<Camera>();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            float scrool = Input.mouseScrollDelta.y;
            if (scrool > 0 && camera.orthographicSize > zoomComponent.ZoomMax)
            {
                camera.orthographicSize -= zoomComponent.VelocityZoom * Time.deltaTime * scrool;
            }

            if (scrool < 0 && camera.orthographicSize < zoomComponent.ZoomMin)
            {
                camera.orthographicSize -= zoomComponent.VelocityZoom * Time.deltaTime * scrool;
            }
        }
    }
}
