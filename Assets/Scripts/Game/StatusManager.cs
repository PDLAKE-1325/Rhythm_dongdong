
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
    public int healh { get; private set; }

    [Header("체력")]
    [SerializeField] float hp_recovery_rate;

    [Header("참조")]
    [SerializeField] Text combo_text;
    [SerializeField] Transform heathObj;

    public void OnMusicStart()
    {
        healh = 1;
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

    void Update()
    {
        SetComboText();
    }
}
