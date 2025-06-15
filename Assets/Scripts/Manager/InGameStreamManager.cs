using UnityEngine;

public class InGameStreamManager : MonoBehaviour
{
    public static InGameStreamManager Instance { get; private set; }
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
    [HideInInspector] public float current_time { get; private set; }
    [HideInInspector] public int current_node_index { get; private set; }
    [HideInInspector] public bool music_started { get; private set; }

    [Header("노트 지나서 데미지 받는 시간")]
    public float note_deth_time;

    [Header("시작하고 음악 나오는 딜레이")]
    public float music_start_delay;

    public void OnMusicStart()
    {
        current_time = 0;
        current_node_index = 0;
        music_started = true;
    }
    public void OnMusicEnd()
    {
        music_started = false;
    }
    public void AddCurNodeIndex()
    {
        ++current_node_index;
    }
    void Update()
    {
        if (music_started)
            current_time += Time.deltaTime;
    }
}
