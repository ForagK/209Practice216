using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Upgrades/Weapon Upgrade")]
public class WeaponUpgradesBase : UpgradesBase
{
    [SerializeField] private WeaponStats weaponToUpgrade;

    void OnEnable()
    {
        UpName = weaponToUpgrade.weaponName;
        Description = weaponToUpgrade.description;
        Value = 1;
    }

    public override void Apply()
    {
        List<WeaponBase> playerWeapons = PlayerStats.Instance.Weapons;

        WeaponBase ownedWeapon = playerWeapons.FirstOrDefault(w => w.stats == weaponToUpgrade);

        if (ownedWeapon == null)
        {
            GameObject newWeaponObj = Resources.Load<GameObject>("Prefabs/Weapons/" + weaponToUpgrade.weaponName);
            playerWeapons.Add(newWeaponObj.GetComponent<WeaponBase>());
        }
        else
        {
            ownedWeapon.Upgrade();
        }
    }

    public bool IsMaxedOut()
    {
        var playerWeapons = PlayerStats.Instance.Weapons;
        var ownedWeapon = playerWeapons.FirstOrDefault(w => w.stats == weaponToUpgrade);

        if (ownedWeapon != null)
        {
            return ownedWeapon.CurrentLevel >= weaponToUpgrade.maxLevel;
        }

        return false;
    }
}
