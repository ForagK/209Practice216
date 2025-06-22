using TMPro;
using UnityEditor;
using UnityEngine;

public class UIEnd : MonoBehaviour
{
    [SerializeField] GameObject endUI;
    [SerializeField] TextMeshProUGUI endText;
    public void ShowMenu()
    {
        GameManager.Instance.Pause();
        if (GameManager.Instance.Won)
        {
            endText.text = "You Won!";
        }
        else
        {
            endText.text = "You Lost";
        }
        endUI.SetActive(true);
    }
}
