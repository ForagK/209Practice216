using UnityEngine;

public class StraightMovement : MonoBehaviour, IProjectileMovement
{
    public void Move(ProjectileBase projectile)
    {
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 forwardDirection = transform.up.normalized;
        Vector3 newPosition = rb.position + forwardDirection * projectile.Weapon.stats.projectileSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
