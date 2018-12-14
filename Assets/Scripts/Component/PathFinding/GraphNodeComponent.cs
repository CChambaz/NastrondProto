using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class GraphNodeComponent : Component {
        public float cost;
        public List<GameObject> neighbors;
        public Transform position;

        void OnDrawGizmos() {
            foreach (GameObject neighbor in neighbors) {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}
