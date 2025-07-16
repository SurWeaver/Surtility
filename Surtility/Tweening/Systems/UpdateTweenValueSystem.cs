using Leopotam.EcsLite;
using Surtility.Extensions;
using Surtility.Tweening.Components;

namespace Surtility.Tweening.Systems;

public class UpdateTweenValueSystem<T>(Func<T, T, float, T> lerpFunction)
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<Tween> _tweenPool;
    private EcsPool<Easing> _easingPool;
    private EcsPool<TweenValuePair<T>> _tweenValuePool;
    private EcsPool<TweenCurrentValue<T>> _tweenCurrentValuePool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Tween>()
            .Inc<Easing>()
            .Inc<TweenValuePair<T>>()
            .End();

        _tweenPool = world.GetPool<Tween>();
        _easingPool = world.GetPool<Easing>();

        _tweenValuePool = world.GetPool<TweenValuePair<T>>();
        _tweenCurrentValuePool = world.GetPool<TweenCurrentValue<T>>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var tween = ref _tweenPool.Get(entity);
            ref var easingType = ref _easingPool.Get(entity).Type;

            ref var valuePair = ref _tweenValuePool.Get(entity);

            ref var currentValue = ref _tweenCurrentValuePool.GetOrAdd(entity);

            var easingFunction = EasingFunctions.GetEaseFunction(easingType);
            var easeProgress = easingFunction(tween.Percent);

            currentValue.Value = lerpFunction(valuePair.StartValue, valuePair.EndValue, easeProgress);
        }
    }
}
