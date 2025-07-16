using Leopotam.EcsLite;

namespace Surtility.Extensions;

public static class FilterExtensions
{
    public static ref T GetSingleEntityComponent<T>(this EcsFilter filter, EcsPool<T> pool) where T : struct
    {
        if (filter.GetEntitiesCount() == 1)
            foreach (var entity in filter)
                return ref pool.Get(entity);

        throw new Exception("Wrong entity count: " + filter.GetEntitiesCount().ToString());
    }
}
