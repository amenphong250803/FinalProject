using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    [Header("Audio Source dùng để phát nhạc nền")]
    public AudioSource bgmSource;

    private void Awake()
    {
        // Singleton để đảm bảo chỉ có 1 BGM chạy trong toàn bộ game
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Phát nhạc nền
    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        if (bgmSource == null)
            bgmSource = GetComponent<AudioSource>();

        bgmSource.volume = volume;

        // Nếu đang phát đúng bài rồi thì không restart
        if (bgmSource.clip == clip && bgmSource.isPlaying)
            return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // Dừng nhạc nền
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
