using UnityEngine;

public interface IImpactEffect
{
    void OnHit(WeaponBase weapon, Collider target);
}
