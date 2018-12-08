using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nastrond {
    public class EntityManager : MonoBehaviour {

        static EntityManager instance;
        public static EntityManager Instance {
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

        List<GameObject> entities;

        public List<GameObject> GetEntities() {
            return entities;
        }

        public void RegisterAsEntities(GameObject o) {
            if (entities == null) {
                entities = new List<GameObject>();
            }

            if (entities.Contains(o)) {
                return;
            }

            entities.Add(o);
        }
    }
}

