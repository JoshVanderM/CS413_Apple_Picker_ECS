using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ScoreAuthoring : MonoBehaviour
{
    private class ScoreBaker : Baker<ScoreAuthoring>
    {
        public override void Bake(ScoreAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            var propertiesComponent = new Score
            {
                score = 0
            };

            AddComponent(entity, propertiesComponent);
        }
    }
}
public struct Score : IComponentData
{
    public int score;
}
