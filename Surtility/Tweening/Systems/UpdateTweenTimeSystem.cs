using Leopotam.EcsLite;
using Surtility.Tweening.Components;
using Surtility.Timing;

namespace Surtility.Tweening.Systems;

public class UpdateTweenTimeSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _tweenFilter;
    private EcsPool<Tween> _tweenPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _tweenFilter = world.Filter<Tween>()
            .End();

        _tweenPool = world.GetPool<Tween>();
    }

    public void Run(IEcsSystems systems)
    {
        if (_tweenFilter.GetEntitiesCount() < 1)
            return;

        foreach (var entity in _tweenFilter)
        {
            ref var tween = ref _tweenPool.Get(entity);

            tween.CurrentTime = Math.Min(tween.CurrentTime + DeltaTime.Seconds, tween.Duration);
            tween.Percent = (float)(tween.CurrentTime / tween.Duration);
        }
    }
}
