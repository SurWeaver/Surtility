using Leopotam.EcsLite;

namespace Surtility.Extensions;

public static class PoolExtensions
{
    public static ref T Add<T>(this EcsPool<T> pool, int entity, T componentValue) where T : struct
    {
        ref var addedComponent = ref pool.Add(entity);
        addedComponent = componentValue;

        return ref addedComponent;
    }

    public static ref T GetOrAdd<T>(this EcsPool<T> pool, int entity, T defaultValue = default) where T : struct
    {
        if (pool.Has(entity))
            return ref pool.Get(entity);

        return ref pool.Add(entity, defaultValue);
    }
}
