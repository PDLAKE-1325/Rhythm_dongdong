using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
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
    Dictionary<LaneType, Transform> lane_end_pos = new();

    void SetLaneEndTransform()
    {
        lane_end_pos.Add(LaneType.Up, end_up);
        lane_end_pos.Add(LaneType.Down, end_down);
        lane_end_pos.Add(LaneType.Left, end_left);
        lane_end_pos.Add(LaneType.Right, end_right);
    }

    void CheckNodeToGenerate()
    {
        int len = InGameStreamManager.Instance.music_sheet.Count;
        int cur_spawn_idx = InGameStreamManager.Instance.current_node_spawn_index;
        float time = InGameStreamManager.Instance.current_time;


        if (cur_spawn_idx < len)
        {
            NoteData curSpawnNote = InGameStreamManager.Instance.music_sheet[cur_spawn_idx];
            if (curSpawnNote.time <= time + note_spawn_ahead_time)
            {
                GenerateNote(curSpawnNote, cur_spawn_idx);
                InGameStreamManager.Instance.AddCurNodeSpawnIndex();
            }
        }
    }

    void GenerateNote(NoteData data, int cur_spawn_index)
    {
        GameObject new_node = Instantiate(note_prefab, note_parent);

        Note note_script;
        new_node.TryGetComponent(out note_script);

        note_script.Init(data, lane_end_pos[data.lane], cur_spawn_index);
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
