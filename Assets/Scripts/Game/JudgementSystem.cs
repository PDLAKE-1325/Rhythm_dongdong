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
    [SerializeField] double m_perfect;
    [SerializeField] double m_great;
    [SerializeField] double m_good;
    [SerializeField] double m_bad;
    public double perfect => m_perfect;
    public double great => m_great;
    public double good => m_good;
    public double bad => m_bad;

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

    [Header("판정소리")]
    [SerializeField] AudioClip note_audio;

    bool onJudge;
    KeyCode lastKey;

    void NoteJudged(GameObject text_prefab, Note note)
    {
        InGameStreamManager.Instance.AddCurNodeIndex();
        SoundManager.Instance.NoteSound();
        Destroy(note.gameObject);
        Instantiate(text_prefab, judge_text_parent);
        GameObject noteDeathObj = Instantiate(note_death_prefab, note_death_parent);
        noteDeathObj.transform.position = note.transform.position;
    }

    public void MissNote(Note note)
    {
        InGameStreamManager.Instance.AddCurNodeIndex();
        StatusManager.Instance.AddJudge("miss");
        Destroy(note.gameObject);
        Instantiate(miss_prefab, judge_text_parent);
        StatusManager.Instance.Damage();
    }

    public void JudgeNoteTime(Note note, KeyCode key)
    {
        if (onJudge) return;
        onJudge = true;
        lastKey = key;

        double note_time = note.note_data.time;
        double inputTime = SoundManager.Instance.GetMusicTime();

        double time_space = MathF.Abs((float)(inputTime - note_time));

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
    void Update()
    {
        if (onJudge && Input.GetKeyUp(lastKey)) onJudge = false;
    }
}
