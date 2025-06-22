using UnityEngine;

public class BoomerangMovement : MonoBehaviour, IProjectileMovement
{
    Transform player;
    Vector3 forwardDirection;
    Vector3 playerPosition;
    Vector3 newPosition;
    bool returning = false;
    float timer = 0f;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        forwardDirection = transform.up.normalized;
    }
    public void Move(ProjectileBase projectile)
    {
        playerPosition = new Vector3(player.position.x, player.position.y + 1, player.position.z);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.rotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y + 360 * Time.fixedDeltaTime, 0);

        if (!returning)
        {
            newPosition = rb.position + forwardDirection * projectile.Weapon.stats.projectileSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if (timer >= projectile.Weapon.stats.projectileLifetime/3)
            {
                returning = true;
            }
        }
        else
        {
            Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
            newPosition = rb.position + directionToPlayer * projectile.Weapon.stats.projectileSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
            if (Vector3.Distance(transform.position, playerPosition) < 1f)
            {
                Destroy(gameObject);
            }
        }
        timer += Time.fixedDeltaTime;
    }
}
