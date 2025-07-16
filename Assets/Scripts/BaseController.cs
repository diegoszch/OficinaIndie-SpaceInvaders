using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private int maxLife = 3;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        maxLife = life;
        UpdateColor();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);
            life--;
            UpdateColor();
            if (life <= 0)
                Destroy(gameObject);
        }
    }

    void UpdateColor()
    {
        float t = 1f - Mathf.Clamp01((float)life / maxLife);
        sr.color = Color.Lerp(Color.white, Color.red, t);
    }
}
