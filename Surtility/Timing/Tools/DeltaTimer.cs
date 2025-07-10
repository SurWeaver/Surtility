using Microsoft.Xna.Framework;

namespace Surtility.Timing.Tools;

public class DeltaTimer
{
    private double _deltaSeconds;

    public void UpdateTime(GameTime gameTime)
    {
        _deltaSeconds = gameTime.ElapsedGameTime.TotalSeconds;
    }

    public double GetDeltaSeconds()
    {
        return _deltaSeconds;
    }
}
