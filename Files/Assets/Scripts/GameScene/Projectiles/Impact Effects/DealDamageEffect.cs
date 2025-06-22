using UnityEngine;

public class DealDamageEffect : MonoBehaviour, IImpactEffect
{
    public void OnHit(WeaponBase weapon, Collider target)
    {
        if (target.gameObject.CompareTag("Enemy"))
        {
            EnemyBase enemy = target.gameObject.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.GetDamaged(weapon.stats.levelInfo.GetDamageForLevel(weapon.CurrentLevel));
            }
        }
    }
}
