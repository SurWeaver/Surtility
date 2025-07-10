using Leopotam.EcsLite;
using Surtility.Timing.Components;
using Surtility.Tweening.Components;

using Surtility.Extensions;

namespace Surtility.Tweening.Systems;

public class UpdateTweenTimeSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _tweenFilter;
    private EcsPool<Tween> _tweenPool;
    private EcsPool<DeltaTime> _dtPool;
    private EcsFilter _dtFilter;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _tweenFilter = world.Filter<Tween>()
            .End();

        _tweenPool = world.GetPool<Tween>();
        _dtPool = world.GetPool<DeltaTime>();
        _dtFilter = world.Filter<DeltaTime>().End();

    }

    public void Run(IEcsSystems systems)
    {
        if (_tweenFilter.GetEntitiesCount() < 1)
            return;

        var deltaTime = _dtFilter.GetSingleEntityComponent(_dtPool).Seconds;

        foreach (var entity in _tweenFilter)
        {
            ref var tween = ref _tweenPool.Get(entity);

            tween.CurrentTime = Math.Min(tween.CurrentTime + deltaTime, tween.Duration);
            tween.Percent = (float)(tween.CurrentTime / tween.Duration);
        }
    }
}
