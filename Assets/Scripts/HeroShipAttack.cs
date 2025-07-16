using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroShipAttack : MonoBehaviour
{
    [SerializeField] private LaserProjectile projectile;

    private float nextAttack = 0;
    private bool canAttack;

    void Start()
    {
        canAttack = true;
        nextAttack = 0;
    }

    void Update()
    {
        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var obj = Instantiate(projectile, transform.position + (Vector3.up + (Vector3.up * 0.1f)), Quaternion.identity);
                obj.SetConfig(Color.white, ProjectileDirectionEnum.Up);

                canAttack = false;
            }
        }

        if (nextAttack > 2)
        {
            canAttack = true;
            nextAttack = 0;
        }

        nextAttack += Time.deltaTime;

        // Debug.Log("CanAttack: [" + canAttack + "]");
        // Debug.Log("Time.time: [" + Time.time.ToString() + "]");
        // Debug.Log("Time.deltaTime: [" + Time.deltaTime.ToString() + "]");
        // Debug.Log("NextAttack: [" + nextAttack.ToString() + "]");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
