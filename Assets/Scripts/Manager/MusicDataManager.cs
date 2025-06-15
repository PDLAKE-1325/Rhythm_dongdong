using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataManager : MonoBehaviour
{
    public static MusicDataManager Instance { get; private set; }
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

    public List<MusicData> music_data;
}
