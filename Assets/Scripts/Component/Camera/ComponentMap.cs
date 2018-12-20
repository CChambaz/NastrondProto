using UnityEngine;

namespace Nastrond {
    public class ComponentMap:Component {
        public Vector2 sizeMap;
        public Vector2 offSet;

        void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(offSet, offSet + sizeMap * Vector2.up);
            Gizmos.DrawLine(offSet, offSet + sizeMap * Vector2.right);
            Gizmos.DrawLine(offSet + sizeMap, offSet + sizeMap * Vector2.right);
            Gizmos.DrawLine(offSet + sizeMap, offSet + sizeMap * Vector2.up);
        }
    }
}
