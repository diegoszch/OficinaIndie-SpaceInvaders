using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private LaserProjectile projectile;

    public void Attack()
    {
        var obj = Instantiate(projectile, transform.position + Vector3.down, Quaternion.identity);
        obj.SetConfig(Color.red, ProjectileDirectionEnum.Down);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
