using Leopotam.EcsLite;
using Surtility.Tweening.Components;

namespace Surtility.Tweening.Systems;

/// <summary>
/// Система, вызываемая в конце цикла, для удаления tween-сущностей.
/// </summary>
public class DeleteTweenSystem()
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<Tween> _tweenPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<Tween>()
            .End();

        _tweenPool = _world.GetPool<Tween>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var tween = ref _tweenPool.Get(entity);
            if (tween.Percent < 1f)
                return;

            _world.DelEntity(entity);
        }
    }
}
