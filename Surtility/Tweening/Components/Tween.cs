namespace Surtility.Tweening.Components;

public struct Tween(double duration)
{
    public double Duration = duration;
    public double CurrentTime;
    public float Percent;
}
