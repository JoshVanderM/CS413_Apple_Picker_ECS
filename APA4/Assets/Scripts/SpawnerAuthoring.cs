using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SpawnerAuthoring : MonoBehaviour
{
    public GameObject applePrefab;
    public float delay;
    public float speed;

    public class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var propertiesComponent = new AppleProperties
            {
                ApplePrefab = GetEntity(authoring.applePrefab, TransformUsageFlags.Dynamic),
                delay = authoring.delay,
                speed = authoring.speed,
                timer = UnityEngine.Random.value * 2,
                Random = Random.CreateFromIndex((uint)entity.Index)
            };

            AddComponent(entity, propertiesComponent);
        }
    }
}

public struct AppleProperties : IComponentData
{
    public Entity ApplePrefab;
    public float delay;
    public float speed;
    public float timer;
    public Random Random;
}
