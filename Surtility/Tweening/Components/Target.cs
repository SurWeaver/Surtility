using Leopotam.EcsLite;

namespace Surtility.Tweening.Components;

public struct Target(EcsPackedEntity entity)
{
    public EcsPackedEntity Entity = entity;
}