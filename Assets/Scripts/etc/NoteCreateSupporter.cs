using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoteCreateSupporter : MonoBehaviour
{

    private enum SaveMusic
    {
        Power,
        Halo,
        OIIA,
        Candy,
    }
    [Header("주 설정")]
    [SerializeField] SaveMusic saveMusicType;
    [SerializeField] bool WantTest;

    [Header("기타 세부설정")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] List<AudioClip> bgmClip;
    [SerializeField] NoteTimeSave data_saver;
    [SerializeField] float default_speed;
    [SerializeField] double speed_diff;


    double music_start_time;


    List<NoteData> press_time_save = new();

    void MusicStart()
    {
        if (saveMusicType == SaveMusic.Power)
        {
            bgmSource.clip = bgmClip[0];
        }
        else if (saveMusicType == SaveMusic.Halo)
        {
            bgmSource.clip = bgmClip[1];
        }
        else if (saveMusicType == SaveMusic.OIIA)
        {
            bgmSource.clip = bgmClip[2];
        }
        else if (saveMusicType == SaveMusic.Candy)
        {
            bgmSource.clip = bgmClip[3];
        }

        transform.position = Vector2.zero;

        bgmSource.loop = false;

        bgmSource.SetScheduledEndTime(AudioSettings.dspTime);

        music_start_time = AudioSettings.dspTime + 1.0f;
        bgmSource.PlayScheduled(music_start_time);
    }

    double GetMusicTime()
    {
        return AudioSettings.dspTime - music_start_time;
    }

    void UpdateNoteData()
    {
        if (saveMusicType == SaveMusic.Power)
        {
            data_saver.MorePlastic_Power = press_time_save;
        }
        else if (saveMusicType == SaveMusic.Halo)
        {
            data_saver.Tako_Halo = press_time_save;
        }
        else if (saveMusicType == SaveMusic.OIIA)
        {
            data_saver.WW_OIIAOIIA = press_time_save;
        }
        else if (saveMusicType == SaveMusic.Candy)
        {
            data_saver.BEATPELLAHOUSE_CandyThief = press_time_save;
        }

#if UNITY_EDITOR
        // 에셋을 변경된 상태로 표시
        EditorUtility.SetDirty(data_saver);

        // 모든 변경 사항 저장
        AssetDatabase.SaveAssets();
        Debug.Log("GameData 저장 완료");
#else
        Debug.LogWarning("런타임 빌드에서는 ScriptableObject 저장이 지원되지 않음.");
#endif
    }

    void AddList(LaneType lane, double time)
    {
        MakeClone();
        NoteData newNote = new(time + speed_diff, default_speed, lane, NoteType.Default);
        press_time_save.Add(newNote);
    }

    void PressDetect()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddList(LaneType.Up, GetMusicTime());
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddList(LaneType.Left, GetMusicTime());

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            AddList(LaneType.Down, GetMusicTime());

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            AddList(LaneType.Right, GetMusicTime());
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpdateNoteData();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    [SerializeField] float speed;
    [SerializeField] Text text;
    [SerializeField] Text text_transgor;

    void MoveLeft()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;
        text.text = $"{press_time_save.Count}";
        text_transgor.text = $"{transform.position.x}";
    }

    [SerializeField] GameObject new_obj;
    [SerializeField] Transform new_obj_parent;

    void MakeClone()
    {
        GameObject newObj = Instantiate(new_obj, new_obj_parent);
        newObj.transform.position = transform.position;
    }

    void Restart()
    {
        foreach (Transform item in new_obj_parent)
        {
            Destroy(item.gameObject);
        }
        press_time_save = new();
        MusicStart();
    }

    void Start()
    {
        if (WantTest) SceneManager.LoadScene("Main");
        MusicStart();
    }
    void Update()
    {
        PressDetect();
        MoveLeft();
    }
}
