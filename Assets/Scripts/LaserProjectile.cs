using UnityEngine;

public enum ProjectileDirectionEnum
{
    Up,
    Down
}

public class LaserProjectile : MonoBehaviour
{
    [SerializeField] private Color color;

    [SerializeField] private ProjectileDirectionEnum direction;

    [SerializeField] private float speed;

    private float bonus;

    private SpriteRenderer sr;

    private Rigidbody2D rb;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = color;

        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 3f);
    }

    public void SetConfig(Color paramColor, ProjectileDirectionEnum paramDirection, float paramBonus = 0)
    {
        direction = paramDirection;
        color = paramColor;
        bonus = paramBonus;
    }

    void FixedUpdate()
    {
        var dir = direction == ProjectileDirectionEnum.Up
            ? Vector2.up
            : Vector2.down;
        rb.linearVelocity = dir * speed;
    }
}
