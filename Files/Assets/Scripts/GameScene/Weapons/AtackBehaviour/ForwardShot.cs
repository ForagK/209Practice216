using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Attack Behaviors/Forward Shot")]
public class ForwardShot : AttackBehaviorBase
{
    public override void Attack(Transform origin, WeaponStats stats)
    {
        Instantiate(stats.projectilePrefab, new Vector3(origin.position.x, origin.position.y + 1, origin.position.z), Quaternion.Euler(90, origin.eulerAngles.y, origin.eulerAngles.z));
    }
}
