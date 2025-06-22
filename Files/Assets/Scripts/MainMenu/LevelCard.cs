using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    [SerializeField] SelectMenu selectMenu;
    [SerializeField] TextMeshProUGUI selectName;
    [SerializeField] Image icon;
    [SerializeField] Button button;
    LevelInfo levelInfo;
    public LevelInfo LevelInfo { get { return levelInfo; } }
    public void Init(LevelInfo levelInfo)
    {
        this.levelInfo = levelInfo;
        selectMenu = FindFirstObjectByType<SelectMenu>();
        selectName.text = levelInfo.levelName;
        icon.sprite = Resources.Load<Sprite>("Textures/Levels/" + levelInfo.levelName);
        if (SaveManager.Instance.SaveData.maxUnlockedLevel >= levelInfo.levelNumber)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
        }
        else
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
        }
        button.onClick.AddListener(() =>
        {
            if (SaveManager.Instance.SaveData.maxUnlockedLevel >= levelInfo.levelNumber)
            {
                selectMenu.SelectedLevel = levelInfo;
            }
            selectMenu.UpdateGUI();
        });
    }
    public void SelectCard()
    {
        if (selectMenu.SelectedLevel == levelInfo)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Selected";
            button.interactable = false;
        }
        else if (SaveManager.Instance.SaveData.maxUnlockedLevel >= levelInfo.levelNumber)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Select";
            button.interactable = true;
        }
    }
}
