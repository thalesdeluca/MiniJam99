using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDash : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameState gameState;
    [SerializeField] PlayerState playerState;
    [SerializeField] InputEvents inputEvents;

    Rigidbody2D rigidbody;
    SpriteRenderer sprite;

    float dashYPos;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        playerState.canDash = !playerState.isDashing && !playerState.hasAirDashed;

        if (playerState.hasAirDashed && playerState.isGrounded)
            playerState.hasAirDashed = false;

        if (playerState.isDashing && !playerState.isGrounded && !playerState.hasAirDashed)
            EnableDash();

        if (!gameState.canControl)
            return;

        if (!playerState.isDashing)
        {
            sprite.color = Color.white;
            return;
        }

        ApplyForce();
    }

    void OnDash(InputAction.CallbackContext context)
    {
        if (!gameState.canControl)
            return;

        if (context.performed)
        {
            EnableDash();
            return;
        }

        if (context.started && playerState.canDash)
        {
            playerState.hasAirDashed = !playerState.isGrounded && !playerState.hasAirDashed;
            DisableDash();


            StartCoroutine(WaitDashTime());
        }
    }

    void ApplyForce()
    {
        sprite.color = Color.red;
        var force = (playerState.dashSpeed + playerState.speed) * Time.deltaTime * playerState.direction;
        force.y = rigidbody.velocity.y;

        if (!playerState.isJumping)
            force.y = 0;

        rigidbody.velocity = force;
    }

    void EnableDash()
    {
        StopAllCoroutines();
        playerState.isDashing = false;
        playerState.canDash = true;

        var force = (playerState.speed) * Time.deltaTime * playerState.direction;
        force.y = rigidbody.velocity.y;
        rigidbody.velocity = force;
    }

    void DisableDash()
    {
        StopAllCoroutines();
        playerState.isDashing = true;
        playerState.canDash = false;
    }

    IEnumerator WaitDashTime()
    {
        yield return new WaitForSeconds(playerState.dashTime);

        EnableDash();
    }

    void OnEnable()
    {
        inputEvents.DashInputEvent.AddListener(OnDash);
    }

    void OnDisable()
    {
        inputEvents.DashInputEvent.RemoveListener(OnDash);
    }
}