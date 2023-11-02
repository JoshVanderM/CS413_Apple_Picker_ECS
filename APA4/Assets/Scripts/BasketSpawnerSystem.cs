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
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketPrefabProperties>>())
        {
            if (properties.ValueRO.respawn == true)
            {
                for (var i = 0; i < properties.ValueRO.basketCount; i++)
                {
                    var basket = ecb.Instantiate(properties.ValueRO.basketPrefab);
                    var pos = new float3
                    {
                        y = properties.ValueRO.bottomY + (properties.ValueRO.basketSpacing * i)
                    };

                    ecb.SetComponent(basket, LocalTransform.FromPosition(pos));
                }
            }
            properties.ValueRW.respawn = false;
        }
    }
}
