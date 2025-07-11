using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Surtility.Input;

public class MouseController
{
    private MouseState _previousState;
    private MouseState _currentState;

    /// <summary>
    /// Текущая позиция курсора мыши относительно окна
    /// </summary>
    public Point MousePosition { get; private set; }

    /// <summary>
    /// Смещение курсора мыши относительно предыдущего кадра
    /// </summary>
    public Point MouseDeltaPosition { get; private set; }

    /// <summary>
    /// Текущее значение прокрутки колеса мыши.
    /// </summary>
    public int CurrentScroll { get; private set; }

    public void Update()
    {
        UpdateMouseState();
        UpdateMousePosition();
        UpdateMouseScroll();
    }

    private void UpdateMouseState()
    {
        _previousState = _currentState;
        _currentState = Mouse.GetState();
    }

    private void UpdateMousePosition()
    {
        MousePosition = _currentState.Position;
        MouseDeltaPosition = _currentState.Position - _previousState.Position;
    }

    private void UpdateMouseScroll()
    {
        CurrentScroll = _currentState.ScrollWheelValue - _previousState.ScrollWheelValue;
    }

    public bool IsPressed(MouseButton button) => button switch
    {
        MouseButton.Left => _currentState.LeftButton is ButtonState.Pressed,
        MouseButton.Right => _currentState.RightButton is ButtonState.Pressed,
        MouseButton.Middle => _currentState.MiddleButton is ButtonState.Pressed,
        MouseButton.X1 => _currentState.XButton1 is ButtonState.Pressed,
        MouseButton.X2 => _currentState.XButton1 is ButtonState.Pressed,
        _ => false,
    };

    public bool IsJustPressed(MouseButton button) => button switch
    {
        MouseButton.Left => _previousState.LeftButton is ButtonState.Released && _currentState.LeftButton is ButtonState.Pressed,
        MouseButton.Right => _previousState.RightButton is ButtonState.Released && _currentState.RightButton is ButtonState.Pressed,
        MouseButton.Middle => _previousState.MiddleButton is ButtonState.Released && _currentState.MiddleButton is ButtonState.Pressed,
        MouseButton.X1 => _previousState.XButton1 is ButtonState.Released && _currentState.XButton1 is ButtonState.Pressed,
        MouseButton.X2 => _previousState.XButton2 is ButtonState.Released && _currentState.XButton2 is ButtonState.Pressed,
        _ => false,
    };


    public bool IsReleased(MouseButton button) => button switch
    {
        MouseButton.Left => _currentState.LeftButton is ButtonState.Released,
        MouseButton.Right => _currentState.RightButton is ButtonState.Released,
        MouseButton.Middle => _currentState.MiddleButton is ButtonState.Released,
        MouseButton.X1 => _currentState.XButton1 is ButtonState.Released,
        MouseButton.X2 => _currentState.XButton2 is ButtonState.Released,
        _ => false,
    };

    public bool IsJustReleased(MouseButton button) => button switch
    {
        MouseButton.Left => _previousState.LeftButton is ButtonState.Pressed && _currentState.LeftButton is ButtonState.Released,
        MouseButton.Right => _previousState.RightButton is ButtonState.Pressed && _currentState.RightButton is ButtonState.Released,
        MouseButton.Middle => _previousState.MiddleButton is ButtonState.Pressed && _currentState.MiddleButton is ButtonState.Released,
        MouseButton.X1 => _previousState.XButton1 is ButtonState.Pressed && _currentState.XButton1 is ButtonState.Released,
        MouseButton.X2 => _previousState.XButton2 is ButtonState.Pressed && _currentState.XButton2 is ButtonState.Released,
        _ => false,
    };
}
