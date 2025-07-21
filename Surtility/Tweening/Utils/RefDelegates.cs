namespace Surtility.Tweening.Utils;

public static class RefDelegates
{
    public delegate void MapSaveFunction<TComponent, TValue>(ref TComponent component, TValue value);
}
