using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Attack Behaviors/Backward Shot")]
public class Backward : AttackBehaviorBase
{
    public override void Attack(Transform origin, WeaponStats stats)
    {
        Quaternion backward = Quaternion.Euler(90, origin.eulerAngles.y, origin.eulerAngles.z) * Quaternion.Euler(1, 1, 180f);
        Instantiate(stats.projectilePrefab, new Vector3(origin.position.x, origin.position.y + 1, origin.position.z), backward);
    }
}
