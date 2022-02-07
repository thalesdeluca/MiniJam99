using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerGun : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] GameState gameState;
    [SerializeField] PlayerState playerState;
    [SerializeField] InputEvents inputEvents;
    [SerializeField] Transform gunTransform;
    [SerializeField] Projectile projectilePrefab;

    bool isInCooldown = false;

    void Start()
    {
        playerState.canShoot = true;
    }

    void Update()
    {
        if (!gameState.canControl)
            return;

        playerState.canShoot = !isInCooldown && !playerState.isDashing;
    }

    void OnFire(InputAction.CallbackContext context)
    {
        if (!gameState.canControl)
            return;

        if (context.performed && playerState.canShoot)
        {
            var go = Instantiate<Projectile>(projectilePrefab, gunTransform.position, Quaternion.identity);
            go.Setup(playerState.direction);
            playerState.canShoot = false;
            isInCooldown = true;
            StartCoroutine(WaitShoot());
        }
    }

    IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(playerState.shootTime);
        playerState.canShoot = true;
        isInCooldown = false;
    }

    void OnEnable()
    {
        inputEvents.FireInputEvent.AddListener(OnFire);
    }

    void OnDisable()
    {
        inputEvents.FireInputEvent.RemoveListener(OnFire);
    }
}