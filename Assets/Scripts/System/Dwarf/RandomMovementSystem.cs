using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class RandomMovementSystem : System {

        List<MotionComponent> motionComponents;

        const float originalTimer = 1;
        float timer = 1;

        // Use this for initialization
        void Start() {
            motionComponents = new List<MotionComponent>();

            List<GameObject> tmpEntities = GetEntities();

            foreach(GameObject e in tmpEntities) {
                if(e.GetComponent<MotionComponent>() != null) {
                    motionComponents.Add(e.GetComponent<MotionComponent>());
                }
            }
        }

        // Update is called once per frame
        void Update() {
            if (timer < 0) {
                foreach (MotionComponent motionComponent in motionComponents) {
                    motionComponent.direction = Random.insideUnitCircle;
                }

                timer = originalTimer;
            }
            else {
                timer -= Time.deltaTime;
            }
        }
    }
}
