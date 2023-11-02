using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class BasketSpawnerAuthoring : MonoBehaviour
{
    public GameObject basketPrefab;
    public int basketCount = 3;
    public int basketSpacing = 2;
    public int bottomY = -14;
    public int numberMissed = 0;
    public bool respawn = true; 
    public class BasketBaker : Baker<BasketSpawnerAuthoring>
    {
        public override void Bake(BasketSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var propertiesComponent = new BasketPrefabProperties
            {
                basketPrefab = GetEntity(authoring.basketPrefab, TransformUsageFlags.Dynamic),
                basketCount = authoring.basketCount,
                basketSpacing = authoring.basketSpacing,
                bottomY = authoring.bottomY,
                numberMissed = authoring.numberMissed,
                respawn = authoring.respawn
            };

            AddComponent(entity, propertiesComponent);
        }
    }
}
public struct BasketPrefabProperties : IComponentData
{
    public Entity basketPrefab;
    public int basketCount;
    public int basketSpacing;
    public int bottomY;
    public int numberMissed;
    public bool respawn;
}
