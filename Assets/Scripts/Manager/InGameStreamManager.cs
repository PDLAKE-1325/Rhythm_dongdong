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
    public float current_time { get; private set; }
    public int current_node_index { get; private set; }
    public bool music_started { get; private set; }
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
