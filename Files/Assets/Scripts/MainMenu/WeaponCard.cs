using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCard : MonoBehaviour
{
    [SerializeField] SelectMenu selectMenu;
    [SerializeField] TextMeshProUGUI selectName;
    [SerializeField] Image icon;
    [SerializeField] Button button;
    WeaponBase weapon;
    public void Init(WeaponBase weapon, DefWeapon defWeapon)
    {
        this.weapon = weapon;
        selectMenu = FindFirstObjectByType<SelectMenu>();
        selectName.text = weapon.stats.weaponName;
        icon.sprite = Resources.Load<Sprite>("Textures/Upgrades/" + weapon.stats.weaponName);
        if (SaveManager.Instance.SaveData.unlockedWeapons.Contains(weapon))
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
        }
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = weapon.stats.cost + " coins";
        }
        button.onClick.AddListener(() =>
        {
            if (SaveManager.Instance.SaveData.unlockedWeapons.Contains(weapon))
            {
                defWeapon.defaultWeapon = weapon;
            }
            else if (SaveManager.Instance.SaveData.coinAmount >= weapon.stats.cost)
            {
                SaveManager.Instance.SaveData.coinAmount -= weapon.stats.cost;
                SaveManager.Instance.SaveData.unlockedWeapons.Add(weapon);
                defWeapon.defaultWeapon = weapon;
                SaveManager.Instance.Save(SaveManager.Instance.SaveData);
            }
            selectMenu.UpdateGUI();
        });
    }
    public void SelectCard()
    {
        if (selectMenu.SelectedWeapon == weapon)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
            button.interactable = false;
        }
        else if (SaveManager.Instance.SaveData.unlockedWeapons.Contains(weapon))
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
            button.interactable = true;
        }
    }
}
