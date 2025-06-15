using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    [Header("진행 파라미터")]
    [SerializeField] float pre_time_delay;

    [Header("노트 참조")]
    [SerializeField] Transform note_parent;
    [SerializeField] GameObject note_prefab;

    [Header("노트 생성 타이밍")]
    [SerializeField] float note_spawn_ahead_time;

    [Header("노트 라인별 목적지")]
    [SerializeField] Transform end_up;
    [SerializeField] Transform end_down;
    [SerializeField] Transform end_left;
    [SerializeField] Transform end_right;
    Dictionary<LaneType, Transform> lane_end_pos;

    private List<NoteData> notes;
    private int current_spawn_note_index;

    void SetLaneEndTransform()
    {
        if (end_up == null ||
            end_down == null ||
            end_left == null ||
            end_right == null) return;
        lane_end_pos[LaneType.Up] = end_up;
        lane_end_pos[LaneType.Down] = end_down;
        lane_end_pos[LaneType.Left] = end_left;
        lane_end_pos[LaneType.Right] = end_right;
    }

    public void OnMusicStart()
    {
        current_spawn_note_index = 0;
        int current_level = LevelManager.Instance.currentLevel;
        notes = MusicDataManager.Instance.music_data[current_level].sheet_music.notes;
    }

    void CheckNodeToGenerate()
    {
        int len = notes.Count;
        float time = InGameStreamManager.Instance.current_time;

        if (current_spawn_note_index < len &&
            notes[current_spawn_note_index].time <= time + note_spawn_ahead_time)
        {
            GenerateNote(notes[current_spawn_note_index]);
            current_spawn_note_index++;
        }
    }

    void GenerateNote(NoteData data)
    {
        GameObject new_node = Instantiate(note_prefab, note_parent);

        Note note_script;
        new_node.TryGetComponent(out note_script);

        note_script.Init(data, lane_end_pos[data.lane]);
    }

    void Start()
    {
        SetLaneEndTransform();
    }
    void Update()
    {
        if (InGameStreamManager.Instance.music_started)
            CheckNodeToGenerate();
    }
}
