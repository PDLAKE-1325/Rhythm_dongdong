using System;
using System.Collections.Generic;
using UnityEngine;

public class JudgementSystem : Singleton<JudgementSystem>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [Header("판정시간 (+-)")]
    [SerializeField] float m_perfect;
    [SerializeField] float m_great;
    [SerializeField] float m_good;
    [SerializeField] float m_bad;
    public float perfect => m_perfect;
    public float great => m_great;
    public float good => m_good;
    public float bad => m_bad;

    [Header("트랜트폼 참조")]
    [SerializeField] Transform note_death_parent;
    [SerializeField] Transform judge_text_parent;

    [Header("판정 텍스트 & 노트 죽음 프리펩")]
    [SerializeField] GameObject note_death_prefab;
    [SerializeField] GameObject perfect_prefab;
    [SerializeField] GameObject great_prefab;
    [SerializeField] GameObject good_prefab;
    [SerializeField] GameObject bad_prefab;
    [SerializeField] GameObject miss_prefab;

    void NoteJudged(GameObject text_prefab, Note note)
    {
        Destroy(note.gameObject);
        Instantiate(text_prefab, judge_text_parent);
        GameObject noteDeathObj = Instantiate(note_death_prefab, note_death_parent);
        noteDeathObj.transform.position = note.transform.position;
        InGameStreamManager.Instance.AddCurNodeIndex();
    }

    public void MissNote(Note note)
    {
        StatusManager.Instance.AddJudge("miss");
        Destroy(note.gameObject);
        Instantiate(miss_prefab, judge_text_parent);
    }

    public void JudgeNoteTime(Note note, int note_index)
    {
        if (InGameStreamManager.Instance.current_node_index != note_index) return;

        float note_time = note.note_data.time;
        float inputTime = InGameStreamManager.Instance.current_time;

        float time_space = MathF.Abs(inputTime - note_time);

        if (time_space <= perfect)
        {
            StatusManager.Instance.AddJudge("perfect");
            NoteJudged(perfect_prefab, note);
        }
        else if (time_space <= great)
        {
            StatusManager.Instance.AddJudge("great");
            NoteJudged(great_prefab, note);
        }
        else if (time_space <= good)
        {
            StatusManager.Instance.AddJudge("good");
            NoteJudged(good_prefab, note);
        }
        else if (time_space <= bad)
        {
            StatusManager.Instance.AddJudge("bad");
            NoteJudged(bad_prefab, note);

        }
    }
}
