using Leopotam.EcsLite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surtility.Drawing.Components;

namespace Surtility.Drawing.Systems;

public class DrawSpriteSystem(SpriteBatch spriteBatch) : IEcsInitSystem, IEcsRunSystem
{
    private EcsPool<Sprite> _spritePool;
    private EcsPool<SpritePosition> _positionPool;
    private EcsPool<SpriteBorder> _borderPool;
    private EcsPool<SpriteColor> _colorPool;
    private EcsPool<SpriteRotation> _rotationPool;
    private EcsPool<SpriteOrigin> _originPool;
    private EcsPool<SpriteScale> _scalePool;
    private EcsPool<SpriteDepth> _depthPool;
    private EcsFilter _filter;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world
            .Filter<Sprite>()
            .Inc<SpritePosition>()
            .End();

        _spritePool = world.GetPool<Sprite>();
        _positionPool = world.GetPool<SpritePosition>();
        _borderPool = world.GetPool<SpriteBorder>();
        _colorPool = world.GetPool<SpriteColor>();
        _rotationPool = world.GetPool<SpriteRotation>();
        _originPool = world.GetPool<SpriteOrigin>();
        _scalePool = world.GetPool<SpriteScale>();
        _depthPool = world.GetPool<SpriteDepth>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var sprite = ref _spritePool.Get(entity).Texture;
            ref var position = ref _positionPool.Get(entity).Vector;

            Rectangle? sourceRectangle = null;
            if (_borderPool.Has(entity))
                sourceRectangle = _borderPool.Get(entity).Rectangle;

            var color = Color.White;
            if (_colorPool.Has(entity))
                color = _colorPool.Get(entity).Color;

            var rotation = 0f;
            if (_rotationPool.Has(entity))
                rotation = _rotationPool.Get(entity).Angle;

            var origin = Vector2.Zero;
            if (_originPool.Has(entity))
                origin = _originPool.Get(entity).Vector;

            var scale = Vector2.One;
            if (_scalePool.Has(entity))
                scale = _scalePool.Get(entity).Vector;

            var depth = 0;
            if (_depthPool.Has(entity))
                depth = _depthPool.Get(entity).Layer;

            spriteBatch.Draw(sprite, position, sourceRectangle, color, rotation, origin, scale,
                SpriteEffects.None, depth);
        }
    }
}
