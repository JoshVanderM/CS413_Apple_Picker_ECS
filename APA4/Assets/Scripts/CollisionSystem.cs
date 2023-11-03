
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine.SocialPlatforms.Impl;
using Unity.Collections;
using UnityEngine.SceneManagement;

public partial struct CollisionSystem : ISystem
{
    void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
        foreach (var (transform, properties, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketTag>>().WithEntityAccess())
        {
            var pos = transform.ValueRO.Position;

            foreach (var (transform1, properties1, entity1) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<ApplePrefabProperties>>().WithEntityAccess())
            {
                var pos1 = transform1.ValueRO.Position;
                if (pos1.x >= pos.x - 2 && pos1.y <= pos.y + 1 && pos1.x <= pos.x + 2 && pos1.y >= pos.y + 0.5)
                {
                    ecb.DestroyEntity(entity1);
                }
                else if (pos1.y <= pos.y - 5)
                {
                    ecb.DestroyEntity(entity1);
                    ecb.DestroyEntity(entity);

                    foreach (var (transform2, properties2, entity2) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<ApplePrefabProperties>>().WithEntityAccess())
                    {
                        ecb.DestroyEntity(entity2);
                    }
                    foreach (var (transform3, properties3) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleProperties>>())
                    {
                        properties3.ValueRW.timer = UnityEngine.Random.value * 2;
                    }
                    foreach (var (transform4, properties4) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketPrefabProperties>>())
                    {
                        properties4.ValueRW.numberMissed = properties4.ValueRO.numberMissed + 1;

                        if (properties4.ValueRO.numberMissed >= 3)
                        {
                            SceneManager.LoadScene("MainMenu");
                        }
                    }
                }
            }
        }
    }
}
