using Microsoft.Xna.Framework.Graphics;

namespace Surtility.Drawing.Components;

public struct Sprite(Texture2D texture)
{
    public Texture2D Texture = texture;
}
