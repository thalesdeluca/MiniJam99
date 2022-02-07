using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameState gameState;
    [SerializeField] PlayerState playerState;
    [SerializeField] InputEvents inputEvents;

    [Header("Setup")]
    [SerializeField] Transform groundFoot;
    [SerializeField] LayerMask groundLayer;

    Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.2f);
        Vector3 size = playerState.footSize;
        size.z = 1;

        Gizmos.DrawCube(groundFoot.position, size);
    }

    void Update()
    {
        if (!gameState.canControl)
            return;

        playerState.canJump = playerState.isGrounded;
        playerState.isFalling = playerState.velocity.y < 0;

        UpdateGrounded();

        if (!playerState.isJumping)
            return;

        ApplyForce();
    }

    void UpdateGrounded()
    {
        Collider2D hit = Physics2D.OverlapBox(groundFoot.position, playerState.footSize, 0, groundLayer);

        playerState.isGrounded = hit != null;
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (!gameState.canControl)
            return;

        if (context.performed)
        {
            StopAllCoroutines();
            EnableJump();
        }

        if (context.started && playerState.canJump)
        {
            StopAllCoroutines();
            DisableJump();

            var force = new Vector2(0, playerState.initJumpForce);

            rigidbody.AddForce(force, ForceMode2D.Impulse);
            StartCoroutine(WaitJumpTime());
        }

    }

    void ApplyForce()
    {
        var force = Vector2.zero;
        force.y = playerState.jumpForce;

        rigidbody.AddForce(force);
    }

    void EnableJump()
    {
        playerState.canJump = true;
        playerState.isJumping = false;
    }

    void DisableJump()
    {
        playerState.canJump = false;
        playerState.isJumping = true;
    }

    IEnumerator WaitJumpTime()
    {
        yield return new WaitForSeconds(playerState.jumpTime);
        EnableJump();
    }

    void OnEnable()
    {
        inputEvents.JumpInputEvent.AddListener(OnJump);
    }

    void OnDisable()
    {
        inputEvents.JumpInputEvent.RemoveListener(OnJump);
    }
}