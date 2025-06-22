using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Attack Behaviors/Distance Shot")]
public class DistanceShot : AttackBehaviorBase
{
    public override void Attack(Transform origin, WeaponStats stats)
    {
        Instantiate(stats.projectilePrefab, new Vector3(origin.position.x, origin.position.y + 2f, origin.position.z), Quaternion.Euler(90, origin.eulerAngles.y, origin.eulerAngles.z));
    }
}
