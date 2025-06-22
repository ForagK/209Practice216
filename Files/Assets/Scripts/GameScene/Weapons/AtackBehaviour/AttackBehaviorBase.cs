using UnityEngine;

public abstract class AttackBehaviorBase : ScriptableObject
{
    public abstract void Attack(Transform origin, WeaponStats stats);
}
