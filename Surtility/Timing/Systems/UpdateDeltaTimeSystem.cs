using Leopotam.EcsLite;
using Surtility.Extensions;
using Surtility.Timing.Components;
using Surtility.Timing.Tools;

namespace Surtility.Timing.Systems;

public class UpdateDeltaTimeSystem(DeltaTimer deltaTimer)
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<DeltaTime> _pool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<DeltaTime>()
            .End();

        _pool = world.GetPool<DeltaTime>();

        if (_filter.GetEntitiesCount() == 0)
        {
            var entity = world.NewEntity();
            _pool.Add(entity);
        }
    }

    public void Run(IEcsSystems systems)
    {
        ref var deltaTime = ref _filter.GetSingleEntityComponent(_pool);
        deltaTime.Seconds = deltaTimer.GetDeltaSeconds();
    }
}
