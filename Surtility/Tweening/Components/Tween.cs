namespace Surtility.Tweening.Components;

public struct Tween(double duration)
{
    public Tween(double duration, float percent) : this(duration)
    {
        Percent = percent;
        CurrentTime = duration * percent;
    }

    public double Duration = duration;
    public double CurrentTime;
    public float Percent;
}
