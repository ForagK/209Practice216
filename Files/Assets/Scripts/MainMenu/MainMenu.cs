using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SettingsMenu settingsMenu;

    void Start()
    {
        settingsMenu.Init();
        settingsMenu.LoadSettings();
    }

    public void StartGame()
    {
        UIManager.Instance.HideUI(UIManager.Instance.mainMenuUI);
        UIManager.Instance.ShowUI(UIManager.Instance.selectUI);
    }
    public void ShowSettings()
    {
        UIManager.Instance.HideUI(UIManager.Instance.mainMenuUI);
        UIManager.Instance.ShowUI(UIManager.Instance.settingsUI);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
