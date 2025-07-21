using Leopotam.EcsLite;

namespace Surtility.Cleaning;

/// <summary>
/// Система, удаляющая все сущности, имеющие указанный компонент <see cref="TComponent"/>
/// </summary>
/// <typeparam name="TComponent">Компонент, существующий один кадр (например, событие)</typeparam>
public class RemoveEntityWithComponentSystem<TComponent>
    : IEcsInitSystem, IEcsRunSystem where TComponent : struct
{
    private EcsWorld _world;
    private EcsFilter _filter;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        _filter = _world.Filter<TComponent>()
            .End();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
            _world.DelEntity(entity);
    }
}
