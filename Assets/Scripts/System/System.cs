using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class System:MonoBehaviour {

        protected List<GameObject> GetEntities() {
            return EntityManager.Instance.GetEntities();
        }
    }
}

