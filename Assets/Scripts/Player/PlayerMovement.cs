using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameState gameState;
    [SerializeField] PlayerState playerState;
    [SerializeField] InputEvents inputEvents;

    Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!gameState.canControl)
            return;

        if (!playerState.isMoving && !playerState.isDashing)
        {
            ResetForce();
            return;
        }

        ApplyForce();
    }

    void ResetForce()
    {
        var force = rigidbody.velocity;
        force.x = 0;

        playerState.velocity = force;
        rigidbody.velocity = force;
    }

    void ApplyForce()
    {
        var force = playerState.direction;

        force.x *= playerState.speed * Time.deltaTime;
        force.y = rigidbody.velocity.y;

        rigidbody.velocity = force;
        playerState.velocity = rigidbody.velocity;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        if (!gameState.canControl)
            return;

        if (context.canceled)
        {
            playerState.isMoving = false;
            return;
        }

        var direction = context.ReadValue<Vector2>().normalized;

        if (IsOpposingDirection(direction))
            ResetForce();

        playerState.direction = direction;
        playerState.isMoving = true;
    }

    bool IsOpposingDirection(Vector2 direction)
        => (playerState.direction.x > 0 && direction.x < 0)
           || (playerState.direction.x < 0 && direction.x > 0);


    void OnEnable()
    {
        inputEvents.MoveInputEvent.AddListener(OnMove);
    }

    void OnDisable()
    {
        inputEvents.MoveInputEvent.RemoveListener(OnMove);
    }
}