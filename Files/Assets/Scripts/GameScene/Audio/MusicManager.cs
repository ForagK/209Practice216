using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    AudioSource audioSource;

    [SerializeField] AudioClip mainMenuTheme;
    [SerializeField] AudioClip bossTheme;
    [SerializeField] AudioClip victoryTheme;
    [SerializeField] AudioClip defeatTheme;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        PlayMainMenuMusic();
    }
    public void PlayMainMenuMusic()
    {
        audioSource.clip = mainMenuTheme;
        audioSource.Play();
    }
    public void PlayBattleMusic()
    {
        audioSource.clip =  Resources.Load<AudioClip>("Music/" + SceneManager.GetActiveScene().name + "Battle");
        audioSource.Play();
    }
    public void PlayBossMusic()
    {
        audioSource.clip = bossTheme;
        audioSource.Play();
    }
    public void PlayVictoryMusic()
    {
        audioSource.clip = victoryTheme;
        audioSource.Play();
    }
    public void PlayDefeatMusic()
    {
        audioSource.clip = defeatTheme;
        audioSource.Play();
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
