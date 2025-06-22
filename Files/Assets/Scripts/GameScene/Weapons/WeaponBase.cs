using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public WeaponStats stats;
    public int CurrentLevel { get; private set; } = 1;
    private float attackTimer = 0f;
    public void Initialize()
    {
        CurrentLevel = 1;
        attackTimer = stats.attackSpeed - 2;
    }
    public void Upgrade()
    {
        if (CurrentLevel < stats.maxLevel)
        {
            CurrentLevel++;
        }
    }
    public void TryAttack(Transform transform)
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= stats.attackSpeed)
        {
            var behaviors = stats.levelInfo.GetBehaviorsForLevel(CurrentLevel);
            foreach (var behavior in behaviors)
            {
                behavior?.Attack(transform, stats);
            }
            SoundManager.Instance.PlayPlayerShootSound();
            attackTimer = 0f;
        }
    }
}