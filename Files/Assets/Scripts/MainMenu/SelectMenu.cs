using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{
    public WeaponBase SelectedWeapon
    {
        get { return defWeapon.defaultWeapon; }
        set { defWeapon.defaultWeapon = value; }
    }
    public LevelInfo SelectedLevel { get; set; }
    [SerializeField] DefWeapon defWeapon;

    List<WeaponBase> weapons = new();
    public List<WeaponBase> Weapons {  get { return weapons; } }

    List<LevelInfo> levels = new();
    public List<LevelInfo> Levels { get { return levels; } }

    List<WeaponCard> weaponCards = new();
    public List<WeaponCard> WeaponCards {  get { return weaponCards; } }

    [SerializeField] GameObject weaponCardPrefab;

    List<LevelCard> levelCards = new();
    public List<LevelCard> LevelCards { get { return levelCards; } }
    [SerializeField] GameObject levelCardPrefab;

    [SerializeField] Transform weaponCardContainer;
    [SerializeField] Transform levelCardContainer;
    [SerializeField] TextMeshProUGUI coinAmountText;
    void Awake()
    {
        SelectedWeapon = null;
        SelectedLevel = null;
        weapons = new List<WeaponBase>(Resources.LoadAll<WeaponBase>("Prefabs/Weapons"));
        weapons.Sort((x, y) => x.stats.cost.CompareTo(y.stats.cost));
        levels = new List<LevelInfo>(Resources.LoadAll<LevelInfo>("ScriptableObjects/Levels"));
        levels.Sort((x, y) => x.levelNumber.CompareTo(y.levelNumber));
        UpdateGUI();
        ShowWeapons();
        ShowLevels();
    }
    void ShowWeapons()
    {
        foreach (WeaponBase weapon in weapons)
        {
            GameObject cardObj = Instantiate(weaponCardPrefab, weaponCardContainer);
            WeaponCard weaponCard = cardObj.GetComponent<WeaponCard>();
            weaponCard.Init(weapon, defWeapon);
            WeaponCards.Add(weaponCard);
        }
    }

    void ShowLevels()
    {
        foreach (LevelInfo level in levels)
        {
            GameObject cardObj = Instantiate(levelCardPrefab, levelCardContainer);
            LevelCard levelCard = cardObj.GetComponent<LevelCard>();
            levelCard.Init(level);
            LevelCards.Add(levelCard);
        }
    }

    void OnEnable()
    {
        UpdateGUI();
    }
    public void BackToMainMenu()
    {
        UIManager.Instance.HideUI(UIManager.Instance.selectUI);
        UIManager.Instance.ShowUI(UIManager.Instance.mainMenuUI);
        SelectedWeapon = null;
        SelectedLevel = null;
    }
    public void StartGame()
    {
        if (SelectedWeapon != null && SelectedLevel != null)
        {
            SaveManager.Instance.Save(SaveManager.Instance.SaveData);
            UnityEngine.SceneManagement.SceneManager.LoadScene(SelectedLevel.levelName);
        }
    }
    public void UpdateGUI()
    {
        coinAmountText.text = "Coins: " + SaveManager.Instance.SaveData.coinAmount.ToString();
        foreach (WeaponCard card in WeaponCards)
        {
            card.SelectCard();
        }
        foreach (LevelCard card in LevelCards)
        {
            card.SelectCard();
        }
    }
}
