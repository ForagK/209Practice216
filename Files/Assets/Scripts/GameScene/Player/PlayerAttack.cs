using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayerStats.Instance.Weapons.Add(PlayerStats.Instance.DefaultWeapon); 
        foreach (var weapon in PlayerStats.Instance.Weapons)
        {
            weapon.Initialize();
        }
    }
    void Update()
    {
        foreach (var weapon in PlayerStats.Instance.Weapons)
        {
            weapon.TryAttack(transform);
        }
    }
}