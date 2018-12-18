using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class RotationSystem : System {

        RotationComponent[] rotationSpriteComponents;
        Transform[] transformComponents;

        // Use this for initialization
        void Start() {
            List<RotationComponent> tmpRotationSpriteComponents = new List<RotationComponent>();
            List<Transform> tmpTransformComponents = new List<Transform>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<RotationComponent>()) {
                    tmpTransformComponents.Add(e.transform);
                    tmpRotationSpriteComponents.Add(e.GetComponent<RotationComponent>());
                }
            }

            rotationSpriteComponents = tmpRotationSpriteComponents.ToArray();
            transformComponents = tmpTransformComponents.ToArray();
        }

        // Update is called once per frame
        void Update() {
            for (int index = 0; index < rotationSpriteComponents.Length; index++) {
                RotationComponent rotationComponent = rotationSpriteComponents[index];
                Transform transformComponent = transformComponents[index];

                transformComponent.rotation = Quaternion.Euler(0, 0, transformComponent.eulerAngles.z + rotationComponent.angleOffset);
            }
        }
    }
}
