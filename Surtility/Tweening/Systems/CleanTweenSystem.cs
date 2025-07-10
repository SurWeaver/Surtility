using Leopotam.EcsLite;
using Surtility.Tweening.Components;

namespace Surtility.Tweening.Systems;

/// <summary>
/// Система, вызываемая в конце цикла, для удаления tween-компонентов. Необходимо указать свои
/// реализации <see cref="TweenValuePair{T}"/> и <see cref="TweenCurrentValue{T}"/> и прочие типы
/// для удаления после завершения анимации.
/// </summary>
public class CleanTweenSystem(params Type[] userComponentTypes)
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<Tween> _tweenPool;
    private EcsPool<Easing> _easingPool;

    private readonly List<IEcsPool> userComponentPools = [];

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Tween>()
            .Inc<Easing>()
            .End();

        _tweenPool = world.GetPool<Tween>();
        _easingPool = world.GetPool<Easing>();


        if (userComponentTypes.Length > 0)
            for (var i = 0; i < userComponentTypes.Length; i++)
                userComponentPools.Add(world.GetPoolByType(userComponentTypes[i]));
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var tween = ref _tweenPool.Get(entity);
            if (tween.Percent < 1)
                return;

            _tweenPool.Del(entity);
            _easingPool.Del(entity);

            foreach (var userComponentPool in userComponentPools)
                userComponentPool.Del(entity);
        }
    }
}
