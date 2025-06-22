using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public GameObject weaponObj;
    public WeaponBase Weapon { get; private set; }
    IProjectileMovement movement;
    List<IImpactEffect> impactEffects;

    void Awake()
    {
        Weapon = weaponObj.GetComponent<WeaponBase>();
        Destroy(gameObject, Weapon.stats.projectileLifetime);
    }

    void Start()
    {
        movement = GetComponent<IProjectileMovement>();
        impactEffects = new List<IImpactEffect>(GetComponents<IImpactEffect>());
    }

    void FixedUpdate()
    {
        movement?.Move(this);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (var effect in impactEffects)
        {
            effect?.OnHit(Weapon, collision.collider);
        }
    }
}
