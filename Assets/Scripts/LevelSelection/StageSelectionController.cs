using System;
using System.Collections;
using UnityEngine;

public class StageSelectionController : MonoBehaviour
{
    readonly int MAX_LEVEL = 3;

    [Header("트랜스폼")]
    [SerializeField] Animator square_animator;
    [SerializeField] Transform images_pivot;
    [SerializeField] Transform texts_pivot;

    [Header("오브젝트")]
    [SerializeField] GameObject left_arrow;
    [SerializeField] GameObject right_arrow;
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject InGame;

    [Header("속도")]
    [SerializeField] float change_speed = 5;
    [SerializeField] float change_cool = 1;

    [Header("효과음")]
    [SerializeField] AudioClip change_audio;

    float cur_cool;
    int cur_level;

    void ChangeLevel()
    {
        if (!InGameStreamManager.Instance.in_menu || cur_cool != 0) return;

        if (Input.GetKey(KeyCode.D) && cur_level != MAX_LEVEL)
        {
            cur_cool = change_cool;
            square_animator.SetTrigger("right");
            SoundManager.Instance.PlaySFX(change_audio);
            cur_level++;
        }
        else if (Input.GetKey(KeyCode.A) && cur_level > 0)
        {
            cur_cool = change_cool;
            square_animator.SetTrigger("left");
            SoundManager.Instance.PlaySFX(change_audio);
            cur_level--;
        }
    }
    void MovePivot()
    {
        float x = cur_level * 750;
        images_pivot.localPosition = Vector2.Lerp(images_pivot.localPosition, new Vector2(-x, images_pivot.localPosition.y), change_speed);
        texts_pivot.localPosition = Vector2.Lerp(images_pivot.localPosition, new Vector2(-x, texts_pivot.localPosition.y), change_speed);
    }
    void CoolDown()
    {
        if (cur_cool == 0) return;
        cur_cool = MathF.Max(0, cur_cool - Time.deltaTime);
    }
    void SetArrow()
    {
        left_arrow.SetActive(cur_level != 0);
        right_arrow.SetActive(cur_level != MAX_LEVEL);
    }

    void SelectLevel()
    {
        if (!Input.GetKeyDown(KeyCode.Tab) || InGameStreamManager.Instance.in_game) return;
        LevelManager.Instance.SetCurrentLevel(cur_level);
        GameControlFuntions.Instance.GameStart();
    }

    void Update()
    {
        SetArrow();
        if (InGameStreamManager.Instance.in_title) return;
        ChangeLevel();
        CoolDown();
        MovePivot();
        SelectLevel();
    }
}
