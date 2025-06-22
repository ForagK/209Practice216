using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] UITimer uITimer;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] UIEnd uiEnd;
    [SerializeField] LevelInfo levelInfo;
    float timeLimit = 122f;
    float gameTimer;
    float spawnTimer = 28f;
    int wave = 1;
    bool gameEnded = false;
    public bool Won { get; set; } = false;
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        MusicManager.Instance.PlayMainMenuMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }
    public void RestartGame()
    {
        PlayerStats.Instance.ResetStats();
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelInfo.levelName);
    }
    public void EndGame()
    {
        SaveManager.Instance.SaveData.coinAmount += 10 * wave;
        if (Won)
        {
            MusicManager.Instance.PlayVictoryMusic();
            SaveManager.Instance.SaveData.coinAmount += (100 * levelInfo.levelNumber);
            SaveManager.Instance.SaveData.maxUnlockedLevel = Mathf.Max(SaveManager.Instance.SaveData.maxUnlockedLevel, levelInfo.levelNumber + 1);
        }
        else
        {
            MusicManager.Instance.PlayDefeatMusic();
        }
        SaveManager.Instance.Save(SaveManager.Instance.SaveData);
        uiEnd.ShowMenu();
        PlayerStats.Instance.ResetStats();
    }
    void SpawnEnemies()
    {
        int enemyCount = wave * 8 * (levelInfo.levelNumber);
        enemySpawner.Spawn(enemyCount);
    }
    void UpdateSpawnTimer()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 30)
        {
            SpawnEnemies();
            spawnTimer = 0;
            wave++;
        }
    }
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
        levelInfo = Resources.Load<LevelInfo>("ScriptableObjects/Levels/" + SceneManager.GetActiveScene().name + "LevelInfo");
        MusicManager.Instance.PlayBattleMusic();
        Resume();
        gameTimer = timeLimit;
    }
    void Update()
    {
        gameTimer -= Time.deltaTime;
        uITimer.UpdateTimerDisplay(gameTimer);
        if (gameTimer <= 0 && !gameEnded)
        {
            gameEnded = true;
            enemySpawner.SpawnBoss();
        }
        if(!gameEnded)
        {
            UpdateSpawnTimer();
        }
    }
}