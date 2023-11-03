using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using static UnityEngine.EventSystems.EventTrigger;

public partial struct SpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (transform, properties, entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleProperties>>().WithEntityAccess())
        {
            if (properties.ValueRO.timer <= 0)
            {
                
                var apple = ecb.Instantiate(properties.ValueRO.ApplePrefab);
                ecb.SetComponent(apple, LocalTransform.FromPosition(transform.ValueRO.Position));
                properties.ValueRW.timer = properties.ValueRO.delay * (properties.ValueRW.Random.NextFloat() * 4);
            }
            else
            {
                properties.ValueRW.timer = properties.ValueRO.timer - SystemAPI.Time.DeltaTime;
            }
        }
    }
}
