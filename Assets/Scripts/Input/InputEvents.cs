using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputEvents", menuName = "Input/InputEvents", order = 0)]
public class InputEvents : ScriptableObject
{
    public UnityEvent<InputAction.CallbackContext> MoveInputEvent;
    public UnityEvent<InputAction.CallbackContext> JumpInputEvent;
    public UnityEvent<InputAction.CallbackContext> FireInputEvent;
    public UnityEvent<InputAction.CallbackContext> DashInputEvent;
}