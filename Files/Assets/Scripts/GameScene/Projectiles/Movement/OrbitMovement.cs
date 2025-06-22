using UnityEngine;

public class OrbitMovement : MonoBehaviour, IProjectileMovement
{
    ProjectileBase projectile;
    float orbitRadius = 2f;

    private float currentAngle;

    void Start()
    {
        Vector3 offset = transform.position - PlayerStats.Instance.transform.position;
        offset.y = 0;
        currentAngle = Mathf.Atan2(offset.z, offset.x);
    }

    public void Move(ProjectileBase projectile)
    {
        Transform player = PlayerStats.Instance.transform;

        currentAngle += projectile.Weapon.stats.projectileSpeed * Time.fixedDeltaTime;

        if (currentAngle > Mathf.PI * 2f)
            currentAngle -= Mathf.PI * 2f;

        Vector3 offset = new Vector3(Mathf.Cos(currentAngle), 0, Mathf.Sin(currentAngle)) * orbitRadius;

        Vector3 newPosition = player.position + offset;
        newPosition.y = transform.position.y;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.MovePosition(newPosition);
    }
}
