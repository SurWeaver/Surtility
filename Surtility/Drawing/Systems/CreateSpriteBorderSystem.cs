using Leopotam.EcsLite;
using Microsoft.Xna.Framework;
using Surtility.Drawing.Components;
using Surtility.Extensions;

namespace Surtility.Drawing.Systems;

public class CreateSpriteBorderSystem
    : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<Sprite> _spritePool;
    private EcsPool<FrameCount> _frameCountPool;
    private EcsPool<CurrentFrame> _currentFramePool;
    private EcsPool<SpriteBorder> _spriteBorderPool;

    public void Init(IEcsSystems systems)
    {
        var world = systems.GetWorld();

        _filter = world.Filter<Sprite>()
            .Inc<FrameCount>()
            .Inc<CurrentFrame>()
            .Exc<SpriteBorder>()
            .End();

        _spritePool = world.GetPool<Sprite>();
        _frameCountPool = world.GetPool<FrameCount>();
        _currentFramePool = world.GetPool<CurrentFrame>();
        _spriteBorderPool = world.GetPool<SpriteBorder>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var texture = ref _spritePool.Get(entity).Texture;
            var currentFrame = _currentFramePool.Get(entity).Index;
            var FrameCount = _frameCountPool.Get(entity).Count;

            var frameWidth = texture.Width / FrameCount;

            var frameStart = new Point(frameWidth * currentFrame, 0);
            var spriteSize = new Point(frameWidth, texture.Height);

            _spriteBorderPool.Add(entity, new() { Rectangle = new Rectangle(frameStart, spriteSize) });
        }
    }
}
