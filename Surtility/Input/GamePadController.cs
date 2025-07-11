using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Surtility.Input;

public class GamePadController(PlayerIndex playerIndex = PlayerIndex.One)
{
    private GamePadState _previousState;
    private GamePadState _currentState;

    public bool IsConnected()
    {
        var capabilities = GamePad.GetCapabilities(playerIndex);
        return capabilities.IsConnected;
    }

    public void SetVibration(float leftMotor, float rightMotor)
    {
        GamePad.SetVibration(playerIndex, leftMotor, rightMotor);
    }

    public void Update()
    {
        _previousState = _currentState;
        _currentState = GamePad.GetState(playerIndex);
    }

    public bool IsPressed(Buttons button)
    {
        return _currentState.IsButtonDown(button);
    }

    public bool IsJustPressed(Buttons button)
    {
        return _previousState.IsButtonUp(button)
            && _currentState.IsButtonDown(button);
    }

    public bool IsReleased(Buttons button)
    {
        return _currentState.IsButtonUp(button);
    }

    public bool IsJustReleased(Buttons button)
    {
        return _previousState.IsButtonDown(button)
            && _currentState.IsButtonUp(button);
    }

    public float GetTriggerValue(ControllerSide side)
    {
        if (side is ControllerSide.Left)
            return _currentState.Triggers.Left;

        return _currentState.Triggers.Right;
    }

    public Vector2 GetStickValue(ControllerSide side)
    {
        if (side is ControllerSide.Left)
            return _currentState.ThumbSticks.Left;

        return _currentState.ThumbSticks.Right;
    }
}
