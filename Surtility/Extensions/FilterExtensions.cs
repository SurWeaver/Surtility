using Leopotam.EcsLite;

namespace Surtility.Extensions;

public static class FilterExtensions
{
    public static ref T GetSingleEntityComponent<T>(this EcsFilter filter, EcsPool<T> pool) where T : struct
    {
        var entityCount = filter.GetEntitiesCount();

        if (entityCount == 1)
        {
            var entity = filter.GetEnumerator().Current;

            return ref pool.Get(entity);
        }

        throw new Exception("Wrong entity count: " + entityCount.ToString());
    }
}
