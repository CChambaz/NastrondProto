using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class GraphNodeComponent : Component {
        public float cost;
        public List<GameObject> neighbors;
        public Transform position;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            foreach(GameObject neighbor in neighbors) {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}