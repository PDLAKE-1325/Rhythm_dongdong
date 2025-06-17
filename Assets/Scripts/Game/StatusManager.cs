
using System;
using UnityEngine;
using UnityEngine.UI;
public class StatusManager : Singleton<StatusManager>
{
    protected override void Awake()
    {
        base.Awake();
    }


    [HideInInspector]
    public int judge_perfect { get; private set; }
    [HideInInspector]
    public int judge_great { get; private set; }
    [HideInInspector]
    public int judge_good { get; private set; }
    [HideInInspector]
    public int judge_bad { get; private set; }
    [HideInInspector]
    public int judge_miss { get; private set; }

    [HideInInspector]
    public int combo { get; private set; }
    [HideInInspector]
    public float health { get; private set; }

    [Header("체력")]
    [SerializeField, Range(0, 1)] float miss_damage = 0.2f;
    [SerializeField] float cant_recover_time;
    [SerializeField] float hp_recovery_speed;

    [Header("참조")]
    [SerializeField] Text combo_text;
    [SerializeField] RectTransform healthObj;
    [SerializeField] GameObject hpParentObj;

    [Header("소리")]
    [SerializeField] AudioClip damaged_audio;

    public void OnMusicStart()
    {
        ResetValues();
    }

    public void OnMusicEnd()
    {
        hpParentObj.SetActive(false);
        ResetValues();
    }

    void ResetValues()
    {
        healthObj.sizeDelta = new Vector2(1500, healthObj.sizeDelta.y);
        health = 1;
        combo = 0;
        judge_perfect = 0;
        judge_great = 0;
        judge_good = 0;
        judge_bad = 0;
        judge_miss = 0;
    }

    public void AddJudge(string judge)
    {
        judge_perfect += (judge == "perfect") ? 1 : 0;
        judge_great += (judge == "great") ? 1 : 0;
        judge_good += (judge == "good") ? 1 : 0;
        judge_bad += (judge == "bad") ? 1 : 0;
        judge_miss += (judge == "miss") ? 1 : 0;

        if (judge == "miss")
        {
            combo = 0;
        }
        else
        {
            combo++;
        }
    }

    void SetComboText()
    {
        if (!InGameStreamManager.Instance.in_game)
        {
            combo = 0;
            combo_text.gameObject.SetActive(false);
            return;
        }
        combo_text.text = $"{combo} Combo";
        combo_text.gameObject.SetActive(combo != 0);
    }

    void SetHealth()
    {
        if (!InGameStreamManager.Instance.music_started) return;

        healthObj.sizeDelta = new Vector2(1500 * health, healthObj.sizeDelta.y);

        if (health == 0)
        {
            GameControlFuntions.Instance.GameEnd();
        }
    }

    float cant_recover;

    public void Damage()
    {
        if (!InGameStreamManager.Instance.music_started) return;

        health = Mathf.Max(0, health - miss_damage);
        cant_recover = cant_recover_time;

        SoundManager.Instance.PlaySFX(damaged_audio);
    }

    void HpRecover()
    {
        if (!InGameStreamManager.Instance.music_started) return;

        hpParentObj.SetActive(health != 1);

        if (cant_recover != 0)
        {
            cant_recover = Mathf.Max(0, cant_recover - Time.deltaTime);
            return;
        }

        health = MathF.Min(1, health + Time.deltaTime * hp_recovery_speed);
    }

    void Update()
    {
        SetComboText();
        HpRecover();
        SetHealth();
    }

    void Start()
    {
        ResetValues();
    }
}
