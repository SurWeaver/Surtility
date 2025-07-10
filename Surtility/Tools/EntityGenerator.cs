using Leopotam.EcsLite;

namespace Surtility.Tools;

public class EntityGenerator(EcsWorld ecsWorld)
{
    public EntityBuilder NewEntity()
    {
        var entity = ecsWorld.NewEntity();
        return new EntityBuilder(ecsWorld, entity);
    }

    public class EntityBuilder(EcsWorld ecsWorld, int entity)
    {
        public EntityBuilder With<TComponent>(TComponent component) where TComponent : struct
        {
            var pool = ecsWorld.GetPool<TComponent>();

            ref var newComponent = ref pool.Add(entity);
            newComponent = component;
            return this;
        }
    }
}
