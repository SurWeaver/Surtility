using Leopotam.EcsLite;
using Surtility.Timing.Components;

namespace Surtility.Timing.Systems;

public class ProcessDelaySystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<Delay> _delayPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Delay>()
            .End();

        _delayPool = world.GetPool<Delay>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var delay = ref _delayPool.Get(entity);

            delay.SecondsLeft -= DeltaTime.Seconds;

            if (delay.SecondsLeft <= 0)
                _delayPool.Del(entity);
        }
    }
}
