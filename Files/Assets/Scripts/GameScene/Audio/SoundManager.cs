using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    AudioSource audioSource;
    [SerializeField] Transform cam;

    [SerializeField] AudioClip playerShoot;
    [SerializeField] AudioClip enemyDeath;
    [SerializeField] AudioClip levelUp;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main != null ? Camera.main.transform : null;
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            cam = mainCam.transform;
        }
    }
    void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    public void PlayPlayerShootSound()
    {
        PlaySound(playerShoot);
    }
    public void PlayEnemyDeathSound()
    {
        PlaySound(enemyDeath);
    }
    public void PlayLevelUpSound()
    {
        PlaySound(levelUp);
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
