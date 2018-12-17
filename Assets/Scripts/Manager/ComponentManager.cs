using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class ComponentManager:MonoBehaviour {

        static ComponentManager instance;
        public static ComponentManager Instance {
            get {
                return instance;
            }
        }

        private void Awake() {
            if(instance == null) {
                instance = this;
            } else if(instance != this) {
                Destroy(gameObject);
            }
        }

        List<Component> components = new List<Component>();

        public void AddComponentToManager(Component c) {
            components.Add(c);
        }
    }
}