using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class MotionSystem:System {

        List<MotionComponent> motionComponents;
        List<Transform> transformComponents;

        // Use this for initialization
        void Start() {
            List<GameObject> tmpEntities = GetEntities();

            foreach (GameObject e in tmpEntities) {
                if (e.GetComponent<MotionComponent>() != null) {
                    transformComponents.Add(e.transform);
                    motionComponents.Add(e.GetComponent<MotionComponent>());
                }
            }
        }

        // Update is called once per frame
        void Update() {
            foreach (MotionComponent motionComponent in motionComponents) {
                transformComponents += Time.deltaTime * motionComponent.
            }
        }
    }
}
