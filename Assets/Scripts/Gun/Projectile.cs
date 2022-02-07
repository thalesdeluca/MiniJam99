using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] LayerMask layerCollision;
    [SerializeField] float damage;
    public Vector2 direction;

    Rigidbody2D rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Setup(Vector2 direction)
    {
        this.direction = new Vector2(direction.x > 0 ? 1 : -1, 0);
    }

    void Update()
    {
        rigidbody.velocity = new Vector2(speed, 0) * direction * Time.deltaTime;

        CheckOffCamera();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (layerCollision == (layerCollision | 1 << other.gameObject.layer))
        {
            var damageable = other.gameObject.GetComponent<IDamageable>();

            if (damageable != null)
                damageable.Hit(damage);

            Destroy(gameObject);
        }
    }

    void CheckOffCamera()
    {
        var point = Camera.main.WorldToViewportPoint(transform.position);

        bool isVisible = point.x >= 0 && point.x <= 1 && point.y >= 0 && point.y <= 1;

        if (!isVisible)
            Destroy(gameObject);
    }
}