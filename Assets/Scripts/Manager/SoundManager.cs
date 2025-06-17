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
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float bgmVolume = 0.7f;
    [Range(0f, 1f)] public float sfxVolume = 0.7f;

    [Header("Musics")]
    [SerializeField] List<AudioClip> musics;

    private void Update()
    {
        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;
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
        int level = LevelManager.Instance.currentLevel;
        PlayBGM(musics[level]);
    }
}
