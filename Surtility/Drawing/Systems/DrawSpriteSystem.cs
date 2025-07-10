using Leopotam.EcsLite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surtility.Common.Components;
using Surtility.Drawing.Components;

namespace Surtility.Drawing.Systems;

public class DrawSpriteSystem(SpriteBatch spriteBatch) : IEcsInitSystem, IEcsRunSystem
{
    private EcsPool<Position> _positionPool;
    private EcsPool<Sprite> _spritePool;
    private EcsPool<SpriteBorder> _borderPool;
    private EcsPool<SpriteColor> _colorPool;

    private EcsFilter _filter;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _positionPool = world.GetPool<Position>();
        _spritePool = world.GetPool<Sprite>();
        _borderPool = world.GetPool<SpriteBorder>();
        _colorPool = world.GetPool<SpriteColor>();

        _filter = world
            .Filter<Sprite>()
            .Inc<Position>()
            .End();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var sprite = ref _spritePool.Get(entity).Texture;
            ref var position = ref _positionPool.Get(entity).Vector;

            var color = Color.White;
            if (_colorPool.Has(entity))
                color = _colorPool.Get(entity).Color;

            if (_borderPool.Has(entity))
            {
                ref var border = ref _borderPool.Get(entity).Rectangle;
                spriteBatch.Draw(sprite, position, border, color);
            }
            else
            {
                spriteBatch.Draw(sprite, position, color);
            }
        }
    }
}
