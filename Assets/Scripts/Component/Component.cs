using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class Component : MonoBehaviour{
        void Start() {
            ComponentManager.Instance.AddComponentToManager(this);
        }
    }
}
