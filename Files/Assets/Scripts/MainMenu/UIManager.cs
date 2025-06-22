using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject selectUI;
    public GameObject settingsUI;
    public static UIManager Instance;
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
    public void ShowUI(GameObject ui)
    {
        if (ui != null)
        {
            ui.SetActive(true);
        }
    }
    public void HideUI(GameObject ui)
    {
        if (ui != null)
        {
            ui.SetActive(false);
        }
    }
}
