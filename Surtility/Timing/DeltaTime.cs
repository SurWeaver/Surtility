using Microsoft.Xna.Framework;

namespace Surtility.Timing;

public static class DeltaTime
{
    public static double Seconds { get; private set; }

    public static void UpdateTime(GameTime gameTime)
    {
        Seconds = gameTime.ElapsedGameTime.TotalSeconds;
    }
}
