using UnityEngine;

public class MasterParentFuntions : MonoBehaviour
{
    public void TitleAniEnd()
    {
        InGameStreamManager.Instance.TitleAnimaionEnd();
    }
    public void StartAniEnd()
    {
        GameControlFuntions.Instance.ActuallyStart();
    }

    [SerializeField] AudioClip press_audio;
    bool pressed;
    void Update()
    {
        if (InGameStreamManager.Instance.in_title && Input.anyKeyDown && !pressed)
        {
            pressed = true;
            SoundManager.Instance.PlaySFX(press_audio);
            GameControlFuntions.Instance.OutTitle();
        }
    }
}
