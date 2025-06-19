using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicDataManager : Singleton<MusicDataManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    public List<MusicData> music_data;
    [SerializeField] NoteTimeSave noteSO;

    void Start()
    {
        music_data[0].sheet_music.notes = noteSO.MorePlastic_Power.OrderBy(n => n.time).ToList();
        music_data[1].sheet_music.notes = noteSO.Tako_Halo.OrderBy(n => n.time).ToList();
        music_data[2].sheet_music.notes = noteSO.WW_OIIAOIIA.OrderBy(n => n.time).ToList();
        music_data[3].sheet_music.notes = noteSO.BEATPELLAHOUSE_CandyThief.OrderBy(n => n.time).ToList();
    }

}
