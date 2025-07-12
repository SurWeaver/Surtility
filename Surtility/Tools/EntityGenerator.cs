using Leopotam.EcsLite;
using Surtility.Extensions;

namespace Surtility.Tools;

public static class EntityGenerator
{
    private static EcsWorld _ecsWorld;

    public static void Initialize(EcsWorld ecsWorld)
    {
        _ecsWorld = ecsWorld;
    }

    public static EntityBuilder NewEntity()
    {
        if (_ecsWorld is null)
            throw new NullReferenceException("EcsWorld is not initialized!");

        var entity = _ecsWorld.NewEntity();
        return new EntityBuilder(entity);
    }

    public static EntityBuilder FillEntity(int entity)
    {
        return new EntityBuilder(entity);
    }

    public class EntityBuilder
    {
        private readonly int _entity;

        internal EntityBuilder(int entity)
        {
            _entity = entity;
        }

        public EntityBuilder With<TComponent>(TComponent component) where TComponent : struct
        {
            var pool = _ecsWorld.GetPool<TComponent>();

            pool.Add(_entity, component);
            return this;
        }
    }
}
