using UnityEngine;

namespace Nastrond
{
    public class EntityFactory : MonoBehaviour
    {
        public Sprite spriteDwarf;

        PathFollowSystem pathFollowSystem;
        DayCycleSystem dayCycleSystem;
        FollowCursorSystem followCursorSystem;
        GrowthSystem growthSystem;
        MotionSystem motionSystem;
        RandomMovementSystem randomMovementSystem;
        RotationSystem rotationSystem;
        CarrierManager carrierManager;

        void Start()
        {
            pathFollowSystem = FindObjectOfType<PathFollowSystem>();
            dayCycleSystem = FindObjectOfType<DayCycleSystem>();
            followCursorSystem = FindObjectOfType<FollowCursorSystem>();
            growthSystem = FindObjectOfType<GrowthSystem>();
            motionSystem = FindObjectOfType<MotionSystem>();
            randomMovementSystem = FindObjectOfType<RandomMovementSystem>();
            rotationSystem = FindObjectOfType<RotationSystem>();
            carrierManager = FindObjectOfType<CarrierManager>();
        }

        void Update()
        {
            if(Input.GetButtonDown("Fire1")) {
                SpawnDwarfWorker(Vector2.zero);
            }

            if(Input.GetButtonDown("Fire2")) {
                SpawnDwarfCarrier(Vector2.zero);
            }
        }

        public void SpawnDwarfWorker(Vector2 position)
        {
            GameObject entity = new GameObject("DwarfsWorker");
            entity.transform.position = position;
            entity.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            entity.AddComponent<Entity>();

            entity.AddComponent<MotionComponent>();
            entity.GetComponent<MotionComponent>().maxSpeed = 2.5f;

            entity.AddComponent<SpriteRenderer>();
            entity.GetComponent<SpriteRenderer>().sprite = spriteDwarf;

            entity.AddComponent<PathComponent>();

            entity.AddComponent<DwellingSlotIndexComponent>();

            entity.AddComponent<WorkingSlotIndexComponent>();

            entity.AddComponent<InventoryComponent>();

            //Add inside systems
            if (pathFollowSystem && pathFollowSystem.enabled) {
                pathFollowSystem.AddEntity(entity);
            }

            if (dayCycleSystem && dayCycleSystem.enabled) {
                dayCycleSystem.AddEntity(entity);
            }

            if (followCursorSystem && followCursorSystem.enabled) {
                followCursorSystem.AddEntity(entity);
            }

            if (growthSystem && growthSystem.enabled) {
                growthSystem.RegisterDwarf();
            }

            if (motionSystem && motionSystem.enabled) {
                motionSystem.AddEntity(entity);
            }

            if (randomMovementSystem && randomMovementSystem.enabled) {
                randomMovementSystem.AddEntity(entity);
            }

            if (rotationSystem && rotationSystem.enabled) {
                rotationSystem.AddEntity(entity);
            }
        }

        public void SpawnDwarfCarrier(Vector2 position) {
            GameObject entity = new GameObject("DwarfsCarrier");
            entity.transform.position = position;
            entity.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            entity.AddComponent<Entity>();

            entity.AddComponent<MotionComponent>();
            entity.GetComponent<MotionComponent>().maxSpeed = 2.5f;

            entity.AddComponent<SpriteRenderer>();
            entity.GetComponent<SpriteRenderer>().sprite = spriteDwarf;

            entity.AddComponent<PathComponent>();

            entity.AddComponent<DwellingSlotIndexComponent>();

            entity.AddComponent<InventorySlotsComponent>();

            entity.AddComponent<InventoryComponent>();

            //Add inside systems
            if(pathFollowSystem && pathFollowSystem.enabled) {
                pathFollowSystem.AddEntity(entity);
            }

            if(dayCycleSystem && dayCycleSystem.enabled) {
                dayCycleSystem.AddEntity(entity);
            }

            if (carrierManager && carrierManager.enabled) {
                carrierManager.AddEntity(entity);
            }

            if(followCursorSystem && followCursorSystem.enabled) {
                followCursorSystem.AddEntity(entity);
            }

            if(growthSystem && growthSystem.enabled) {
                growthSystem.RegisterDwarf();
            }

            if(motionSystem && motionSystem.enabled) {
                motionSystem.AddEntity(entity);
            }

            if(randomMovementSystem && randomMovementSystem.enabled) {
                randomMovementSystem.AddEntity(entity);
            }

            if(rotationSystem && rotationSystem.enabled) {
                rotationSystem.AddEntity(entity);
            }
        }
    }
}
