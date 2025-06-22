using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int coinAmount = 0;
    public List<WeaponBase> unlockedWeapons = new List<WeaponBase>();
    public int maxUnlockedLevel = 1;
}
