using Microsoft.Xna.Framework.Input;

namespace Surtility.Input;

public class KeyboardController
{
    private KeyboardState _previousState;
    private KeyboardState _currentState;

    public void Update()
    {
        _previousState = _currentState;
        _currentState = Keyboard.GetState();
    }

    public bool IsPressed(Keys key)
    {
        return _currentState.IsKeyDown(key);
    }

    public bool IsPressedNow(Keys key)
    {
        return !_previousState.IsKeyDown(key) && _currentState.IsKeyDown(key);
    }
}
