using System.Collections;
using UnityEngine;

public class GameControlFuntions : Singleton<GameControlFuntions>
{

    protected override void Awake()
    {
        base.Awake();
    }

    [SerializeField] JudgementSystem judgementSystem;
    [SerializeField] Animator master_animator;


    public void OutTitle()
    {
        master_animator.SetTrigger("title");
    }

    public void GameStart()
    {
        foreach (MusicData item in MusicDataManager.Instance.music_data)
            item.stage.SetActive(false);
        MusicDataManager.Instance.music_data
            [LevelManager.Instance.currentLevel].stage.SetActive(true);

        InGameStreamManager.Instance.SetCurSceneState("game");

        master_animator.SetTrigger("start");
    }

    public void GameEnd()
    {
        print("end");
        InGameStreamManager.Instance.OnMusicEnd();
        StatusManager.Instance.OnMusicEnd();
        SoundManager.Instance.VolLerpZeroBGM();
        StartCoroutine(_GameEnd());
    }

    IEnumerator _GameEnd()
    {
        yield return new WaitForSeconds(InGameStreamManager.Instance.music_result_show_time);
        master_animator.SetTrigger("end");
        InGameStreamManager.Instance.SetCurSceneState("menu");
    }

    public void ActuallyStart()
    {
        SoundManager.Instance.OnMusicStart();
        StatusManager.Instance.OnMusicStart();
        InGameStreamManager.Instance.OnMusicStart();
    }
}
