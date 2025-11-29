using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    [Header("Audio Source")]
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

    public void PlayBGM(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        bgmSource.clip = clip;
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void FadeTo(AudioClip newClip, float duration = 1f)
    {
        if (newClip == null)
        {
            return;
        }

        StartCoroutine(FadeBGM(newClip, duration));
    }

    private IEnumerator FadeBGM(AudioClip newClip, float duration)
    {
        float startVolume = bgmSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        bgmSource.volume = 0f;
        bgmSource.clip = newClip;
        bgmSource.Play();

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(0f, startVolume, t / duration);
            yield return null;
        }

        bgmSource.volume = startVolume;
    }
}
