using System.Collections.Generic;
using UnityEngine;

public class InGameStreamManager : Singleton<InGameStreamManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [HideInInspector]
    public float current_time { get; private set; }
    [HideInInspector]
    public int current_node_index { get; private set; }
    [HideInInspector]
    public int current_node_spawn_index { get; private set; }
    [HideInInspector]
    public bool music_started { get; private set; }
    [HideInInspector]
    public List<NoteData> music_sheet { get; private set; }

    [HideInInspector]
    public bool in_title { get; private set; } = true;
    [HideInInspector]
    public bool in_menu { get; private set; }
    [HideInInspector]
    public bool in_game { get; private set; }


    [Header("노트 데미지 받는 거리")]
    public float note_damage_distance;

    [Header("시작하고 음악 나오는 딜레이")]
    public float music_start_delay = 1;

    [Header("끝나고 결과 보여주는 시간")]
    public float music_result_show_time = 3;

    [SerializeField] AudioClip menuMusic;


    public void SetCurSceneState(string scene)
    {
        in_title = scene == "title";
        in_menu = scene == "menu";
        in_game = scene == "game";
    }
    public void TitleAnimaionEnd()
    {
        SetCurSceneState("menu");
    }

    public void OnMusicStart()
    {
        current_time = 0;
        current_node_index = 0;
        current_node_spawn_index = 0;
        music_sheet = MusicDataManager.Instance.music_data
        [LevelManager.Instance.currentLevel].sheet_music.notes;
        music_started = true;
    }
    public void OnMusicEnd()
    {
        music_started = false;
    }
    public void AddCurNodeIndex()
    {
        if (!music_started) return;
        ++current_node_index;
    }
    public void AddCurNodeSpawnIndex()
    {
        if (!music_started) return;
        ++current_node_spawn_index;
    }
    void SetMenuMusic()
    {
        if (in_title || in_menu)
        {
            SoundManager.Instance.bgmVolume = 0.3f;
            SoundManager.Instance.sfxVolume = 1f;
            SoundManager.Instance.PlayBGM(menuMusic);
        }
        else if (in_game)
        {
            SoundManager.Instance.bgmVolume = 0.7f;
            SoundManager.Instance.sfxVolume = 1f;
        }
    }

    void CheckMusicEnd()
    {
        if (!music_started) return;
        if (current_node_index == music_sheet.Count)
        {
            music_started = false;
            GameControlFuntions.Instance.GameEnd();
        }
    }

    void Update()
    {
        print(current_node_index);
        if (music_started)
            current_time += Time.deltaTime;
        SetMenuMusic();
        CheckMusicEnd();
    }
}
