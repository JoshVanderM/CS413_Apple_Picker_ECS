using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct SpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleProperties>>())
        {
            if (properties.ValueRO.timer <= 0)
            {
                var apple = ecb.Instantiate(properties.ValueRO.ApplePrefab);
                ecb.SetComponent(apple, LocalTransform.FromPosition(transform.ValueRO.Position));
                properties.ValueRW.timer = properties.ValueRO.delay;
            }
            else
            {
                properties.ValueRW.timer = properties.ValueRO.timer - SystemAPI.Time.DeltaTime;
            }
        }
    }
}
