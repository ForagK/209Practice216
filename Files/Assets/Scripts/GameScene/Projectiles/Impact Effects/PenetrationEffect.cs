using UnityEngine;

public class PenetrationEffect : MonoBehaviour, IImpactEffect
{
    int penetration;
    void Awake()
    {
        penetration = 0;
    }
    public void OnHit(WeaponBase weapon, Collider target)
    {
        if (penetration >= weapon.stats.penetrationCount)
        {
            Destroy(gameObject);
        }
        penetration++;
    }
}
