using UnityEngine;

public class HeroShipMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float speed;

    private Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator anim = GetComponent<Animator>();
        anim.Play("HeroShip");
    }

    // Update is called once per frame
    void Update()
    {
        var dir = Input.GetAxis("Horizontal");
        if (dir > 0)
        {
            direction = Vector2.right;
        }
        else if (dir < 0)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }
}
