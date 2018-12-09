using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        List<GameObject> entities = new List<GameObject>();

        void Start() {
        }

        public List<GameObject> GetEntities() {
            List<Entity> e = FindObjectsOfType<Entity>().ToList();
            foreach(Entity entity in e) {
                if(!entities.Contains(entity.gameObject))
                entities.Add(entity.gameObject);
            }

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

