using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class RotationSystem : System {

        List<RotationComponent> rotationSpriteComponents;
        List<Transform> transformComponents;

        // Use this for initialization
        void Start() {
            rotationSpriteComponents = new List<RotationComponent>();
            transformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<RotationComponent>()) {
                    transformComponents.Add(e.transform);
                    rotationSpriteComponents.Add(e.GetComponent<RotationComponent>());
                }
            }
        }

        // Update is called once per frame
        void Update() {
            for (int index = 0; index < rotationSpriteComponents.Count; index++) {
                RotationComponent rotationComponent = rotationSpriteComponents[index];
                Transform transformComponent = transformComponents[index];

                transformComponent.rotation = Quaternion.Euler(0, 0, transformComponent.eulerAngles.z + rotationComponent.angleOffset);
            }
        }
    }
}
