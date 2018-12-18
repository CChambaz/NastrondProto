using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class MotionSystem:System {

        MotionComponent[] motionComponents;
        Transform[] transformComponents;

        // Use this for initialization
        void Start() {
            List<MotionComponent> tmpMotionComponents = new List<MotionComponent>();
            List<Transform> tmpTransformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            foreach (GameObject e in tmpEntities) {
                if (e.GetComponent<MotionComponent>() != null) {
                    tmpTransformComponents.Add(e.transform);
                    tmpMotionComponents.Add(e.GetComponent<MotionComponent>());
                }
            }

            motionComponents = tmpMotionComponents.ToArray();
            transformComponents = tmpTransformComponents.ToArray();
        }

        // Update is called once per frame
        void Update() {
            for (int index = 0; index < motionComponents.Length; index++) {
                MotionComponent motionComponent = motionComponents[index];
                Transform trans = transformComponents[index];
                trans.position += Time.deltaTime * (Vector3) motionComponent.direction.normalized * motionComponent.speed;
            }
        }
    }
}