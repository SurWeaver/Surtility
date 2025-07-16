using Leopotam.EcsLite;
using Surtility.Tweening.Components;

namespace Surtility.Tweening.Systems;

public class LoopTweenSystem<T>
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<Tween> _tweenPool;
    private EcsPool<TweenValuePair<T>> _pairPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Tween>()
            .Inc<Looping>()
            .Inc<TweenValuePair<T>>()
            .End();

        _tweenPool = world.GetPool<Tween>();
        _pairPool = world.GetPool<TweenValuePair<T>>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var tween = ref _tweenPool.Get(entity);

            if (tween.Percent < 1f)
                continue;

            tween.CurrentTime = 0;
            tween.Percent = 0;

            ref var pair = ref _pairPool.Get(entity);
            (pair.EndValue, pair.StartValue) = (pair.StartValue, pair.EndValue);
        }
    }
}
