using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

public partial struct CheckForLoseSystem : ISystem
{
    [BurstCompile]
    void OnStart()
    {
        var TotalBaskets = 3;
    }

    [BurstCompile]
    void OnUpdate()
    {
        
    }
}
