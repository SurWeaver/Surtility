using Leopotam.EcsLite;
using Surtility.Timing.Components;
using Surtility.Tweening.Components;
using Surtility.Tweening.Utils;

namespace Surtility.Tweening.Systems;

public class AnimateComponentSystem<TComponent, TValue>(RefDelegates.MapSaveFunction<TComponent, TValue> mapSaveFunction)
    : IEcsInitSystem, IEcsRunSystem where TComponent : struct
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<TweenCurrentValue<TValue>> _tweenPool;
    private EcsPool<Target> _targetPool;
    private EcsPool<TComponent> _componentPool;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<Target>()
            .Inc<TweenCurrentValue<TValue>>()
            .Inc<TComponent>()
            .Exc<Delay>()
            .End();

        _tweenPool = _world.GetPool<TweenCurrentValue<TValue>>();
        _targetPool = _world.GetPool<Target>();
        _componentPool = _world.GetPool<TComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var tweenEntity in _filter)
        {
            ref var target = ref _targetPool.Get(tweenEntity).Entity;

            if (!target.Unpack(_world, out int targetEntity))
                continue;

            var newValue = _tweenPool.Get(tweenEntity).Value;

            ref var component = ref _componentPool.Get(targetEntity);
            mapSaveFunction(ref component, newValue);
        }
    }
}
