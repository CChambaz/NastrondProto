using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class MotionSystem:System {

        List<MotionComponent> motionComponents;
        List<Transform> transformComponents;

        // Use this for initialization
        void Start() {
            motionComponents = new List<MotionComponent>();
            transformComponents = new List<Transform>();

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
            for (int index = 0; index < motionComponents.Count; index++) {
                MotionComponent motionComponent = motionComponents[index];
                Transform trans = transformComponents[index];
                trans.position += Time.deltaTime * (Vector3) motionComponent.direction.normalized * motionComponent.maxSpeed;
            }
        }
    }
}