using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player/PlayerState", order = 0)]
public class PlayerState : ScriptableObject
{
    [Header("Player")]
    public float health;
    public float initHealth;

    [Header("Jump")]
    [Space(20)]
    public float initJumpForce;
    public float jumpForce;
    public float jumpTime;
    public Vector2 footSize;
    public bool isJumping;
    public bool isFalling;
    public bool isGrounded;
    public bool canJump;


    [Header("Move")]
    [Space(20)]
    public float speed;
    public bool isMoving;



    [Header("Dash")]
    [Space(20)]
    public float dashSpeed;
    public float dashTime;
    public bool isDashing;
    public bool canDash;
    public bool hasAirDashed;

    [Header("Gun")]
    [Space(20)]
    public bool canShoot;
    public float shootTime;


    [Header("Rigidbody")]
    [Space(20)]
    public Vector2 direction;
    public Vector2 velocity;

    public void Reset()
    {
        velocity = Vector2.zero;
        direction = Vector2.zero;
        isGrounded = false;
        isJumping = false;
        isMoving = false;
        health = initHealth;
    }
}