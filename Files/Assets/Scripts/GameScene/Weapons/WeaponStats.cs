using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    public string weaponName;
    public string description;
    public int maxLevel;
    public int currentLevel = 1;
    public float attackSpeed;
    public float projectileSpeed;
    public float projectileLifetime;
    public int penetrationCount;
    public int cost;

    public GameObject projectilePrefab;
    public WeaponLevelInfo levelInfo;
}
