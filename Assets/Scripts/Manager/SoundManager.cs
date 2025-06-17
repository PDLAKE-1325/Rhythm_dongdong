using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [Header("Audio Sources")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource noteSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float bgmVolume = 0.7f;
    [Range(0f, 1f)] public float sfxVolume = 0.7f;

    [Header("Musics")]
    [SerializeField] List<AudioClip> musics;

    double music_start_time;

    private void Update()
    {
        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;
        noteSource.volume = sfxVolume;
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void ChangeBGM_Vol(Slider slider)
    {
        bgmVolume = slider.value;
    }
    public void ChangeSFX_Vol(Slider slider)
    {
        sfxVolume = slider.value;
    }

    public void OnMusicStart()
    {
        bgmSource.SetScheduledEndTime(AudioSettings.dspTime);
        int level = LevelManager.Instance.currentLevel;
        music_start_time = AudioSettings.dspTime + 1.0f;
        bgmSource.clip = musics[level];
        bgmSource.loop = false;
        bgmSource.PlayScheduled(music_start_time);
    }

    public void VolLerpZeroBGM()
    {
        StartCoroutine(LerpToZero(1));
    }

    IEnumerator LerpToZero(float duration)
    {
        float elapsed = 0f;
        float startValue = 0.7f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            bgmVolume = Mathf.Lerp(startValue, 0f, t);
            yield return null;
        }

        bgmVolume = 0;
    }


    public double GetMusicTime()
    {
        return AudioSettings.dspTime - music_start_time;
    }

    public void NoteSound()
    {
        // noteSource.PlayScheduled(AudioSettings.dspTime);
    }
}
