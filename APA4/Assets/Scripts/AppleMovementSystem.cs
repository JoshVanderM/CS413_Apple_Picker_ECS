using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial struct AppleMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (transform, properties, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<ApplePrefabProperties>>().WithEntityAccess())
        {
            var pos = transform.ValueRO.Position;
            var speed = properties.ValueRO.speed;

            pos.y -= speed * SystemAPI.Time.DeltaTime;
            transform.ValueRW.Position = pos;

            if (pos.y < properties.ValueRO.BottomY)
            {
                ecb.DestroyEntity(entity);
            }

        }
    }
}
