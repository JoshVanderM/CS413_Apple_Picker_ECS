using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class AppleAuthoring : MonoBehaviour
{
    public float bottomY;
    public float speed;

    private class AppleBaker : Baker<AppleAuthoring>
    {
        public override void Bake(AppleAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var propertiesComponent = new ApplePrefabProperties
            {
                BottomY = authoring.bottomY,
                speed = authoring.speed
            };

            AddComponent(entity, propertiesComponent);
        }
    }

}

public struct ApplePrefabProperties : IComponentData
{
    public float BottomY;
    public float speed;
}