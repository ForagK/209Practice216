using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] GameObject upgradeCardPrefab;
    [SerializeField] Transform cardContainer;
    List<UpgradesBase> upgrades;
    List<Vector2> positions = new List<Vector2> { new Vector2(-620, 0), new Vector2(0, 0), new Vector2(620, 0) };

    void Start()
    {
        upgrades = new List<UpgradesBase>(Resources.LoadAll<UpgradesBase>("ScriptableObjects/Upgrades"));
    }
    public void ShowShop()
    {
        GameManager.Instance.Pause();
        MusicManager.Instance.SetVolume(0.25f);

        List<UpgradesBase> upgradeList = new List<UpgradesBase>(
            upgrades.Where(upgrade =>
            {
                if (upgrade is WeaponUpgradesBase weaponUpgrade)
                {
                    return !weaponUpgrade.IsMaxedOut();
                }
                return true;
            })
        );

        int cardCount = Mathf.Min(3, upgradeList.Count);

        for (int i = 0; i < cardCount; i++)
        {
            int randomIndex = Random.Range(0, upgradeList.Count);
            InstCard(upgradeList[randomIndex], i);
            upgradeList.RemoveAt(randomIndex);
        }
    }

    public void HideShop()
    {
        GameManager.Instance.Resume();
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }
        MusicManager.Instance.SetVolume(0.5f);
    }
    void InstCard(UpgradesBase upgrade, int pos)
    {
        GameObject cardObj = Instantiate(upgradeCardPrefab, cardContainer);
        UpgradeCard card = cardObj.GetComponent<UpgradeCard>();
        card.Init(upgrade, this);
        card.transform.localPosition = positions[pos];
    }
}
