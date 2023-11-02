using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Burst;
using Unity.Transforms;
using Unity.Collections;

public partial struct BasketMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Vector3 mousePos2d = Input.mousePosition;
        mousePos2d.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2d);
        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketTag>>())
        {
            var pos = transform.ValueRO.Position;
            pos.x = mousePos3D.x;
            transform.ValueRW.Position = pos;
        }
    }
}
