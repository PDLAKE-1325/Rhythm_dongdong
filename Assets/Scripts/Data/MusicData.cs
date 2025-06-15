using UnityEngine;

[System.Serializable]
public class MusicData
{
    public SheetMusic sheet_music;
    public string title, composer;
    public int difficulty_star_cnt;
    public Sprite main_image;
    public Sprite preview_image;
    public Sprite in_game_background_image;
    public AudioClip music_source;
}
