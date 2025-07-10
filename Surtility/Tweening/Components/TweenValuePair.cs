namespace Surtility.Tweening.Components;

public struct TweenValuePair<T>(T startValue, T endValue)
{
    public T StartValue = startValue;
    public T EndValue = endValue;
}
