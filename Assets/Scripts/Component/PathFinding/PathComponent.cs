using UnityEngine;

namespace Nastrond {
   public class PathComponent : Component {
       public GraphNodeComponent[] nodes;
       public int index = 0;

       void OnDrawGizmos()
       {
           Gizmos.color = Color.cyan;
           for (int i = index; i < nodes.Length; i++) {
               Gizmos.DrawWireSphere(nodes[i].position.position, 0.1f);

               if (i < nodes.Length - 1) {
                   Gizmos.DrawLine(nodes[i].position.position, nodes[i+1].position.position);
               }
           }
       }
   } 
}