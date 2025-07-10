using Leopotam.EcsLite;

namespace Surtility.Extensions;

public static class PoolExtensions
{
    public static void Add<T>(this EcsPool<T> pool, int entity, T componentValue) where T : struct
    {
        ref var addedComponent = ref pool.Add(entity);
        addedComponent = componentValue;
    }
}
