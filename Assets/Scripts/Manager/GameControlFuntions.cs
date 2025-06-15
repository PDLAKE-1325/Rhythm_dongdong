using UnityEngine;

public class GameControlFuntions : MonoBehaviour
{
    public static GameControlFuntions Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] NoteGenerator noteGenerator;

    public void GameStart()
    {
        noteGenerator.OnMusicStart();
        SoundManager.Instance.OnMusicStart();
        InGameStreamManager.Instance.OnMusicStart();
    }
}
