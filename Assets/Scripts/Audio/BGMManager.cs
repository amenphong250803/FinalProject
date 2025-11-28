using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    [Header("Audio Source dùng để phát nhạc nền")]
    public AudioSource bgmSource;

    private void Awake()
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
    }

    // Phát nhạc bình thường
    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        bgmSource.clip = clip;
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // Dừng nhạc
    public void StopBGM()
    {
        bgmSource.Stop();
    }

    // ⭐ HÀM QUAN TRỌNG: Fade sang nhạc mới
    public void FadeTo(AudioClip newClip, float duration = 1f)
    {
        if (newClip == null)
        {
            Debug.LogWarning("FadeTo gọi nhạc Null!");
            return;
        }

        StartCoroutine(FadeBGM(newClip, duration));
    }

    private IEnumerator FadeBGM(AudioClip newClip, float duration)
    {
        float startVolume = bgmSource.volume;

        // FADE OUT
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        bgmSource.volume = 0f;
        bgmSource.clip = newClip;
        bgmSource.Play();

        // FADE IN
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(0f, startVolume, t / duration);
            yield return null;
        }

        bgmSource.volume = startVolume;
    }
}
