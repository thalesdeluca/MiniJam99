using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputEvents inputEvents;

    public void OnMove(InputAction.CallbackContext context)
        => inputEvents.MoveInputEvent?.Invoke(context);

    public void OnJump(InputAction.CallbackContext context)
        => inputEvents.JumpInputEvent?.Invoke(context);

    public void OnFire(InputAction.CallbackContext context)
        => inputEvents.FireInputEvent?.Invoke(context);

    public void OnDash(InputAction.CallbackContext context)
        => inputEvents.DashInputEvent?.Invoke(context);
}