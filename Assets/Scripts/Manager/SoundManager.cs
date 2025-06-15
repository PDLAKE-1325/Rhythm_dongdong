using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    private AudioSource bgmSource;
    private AudioSource sfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float bgmVolume = 0.7f;
    [Range(0f, 1f)] public float sfxVolume = 0.7f;

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
        StartCoroutine(_OnMusicStart());
    }
    IEnumerator _OnMusicStart()
    {
        float start_delay = InGameStreamManager.Instance.music_start_delay;
        AudioClip music = MusicDataManager.Instance.music_data
        [LevelManager.Instance.currentLevel].music_source;
        yield return new WaitForSeconds(start_delay);
        PlayBGM(music);
    }
}
