using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicDataManager : Singleton<MusicDataManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    public List<MusicData> music_data;
}
