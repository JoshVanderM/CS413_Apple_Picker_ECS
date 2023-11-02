using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct BasketSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<BasketPrefabProperties>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var properties = SystemAPI.GetSingleton<BasketPrefabProperties>();
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        for (var i = 0; i < properties.basketCount; i++)
        {
            var basket = ecb.Instantiate(properties.basketPrefab);
            var pos = new float3
            {
                y = properties.bottomY + (properties.basketSpacing * i)
            };

            ecb.SetComponent(basket, LocalTransform.FromPosition(pos));
        }

        ecb.Playback(state.EntityManager);
    }
}
