using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Level Config")]
public class WeaponLevelInfo : ScriptableObject
{
    [System.Serializable]
    public class LevelData
    {
        public int level;
        public int damage;
        public List<AttackBehaviorBase> attackBehaviors;
    }

    public List<LevelData> levels;

    public List<AttackBehaviorBase> GetBehaviorsForLevel(int level)
    {
        List<AttackBehaviorBase> result = new();
        foreach (var data in levels)
        {
            if (data.level <= level)
                result.AddRange(data.attackBehaviors);
        }
        return result;
    }

    public int GetDamageForLevel(int level)
    {
        var data = levels.FindLast(l => l.level <= level);
        return data != null ? data.damage : 0;
    }
}
