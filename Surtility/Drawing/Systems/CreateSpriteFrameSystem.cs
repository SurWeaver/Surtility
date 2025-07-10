using Leopotam.EcsLite;
using Surtility.Drawing.Components;

namespace Surtility.Drawing.Systems;

public class CreateSpriteFrameSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<CurrentFrame> _framePool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<FrameCount>()
            .Exc<CurrentFrame>()
            .End();

        _framePool = world.GetPool<CurrentFrame>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            _framePool.Add(entity);
        }
    }
}
