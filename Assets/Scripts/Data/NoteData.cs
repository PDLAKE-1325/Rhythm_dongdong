public enum NoteType { Default, Move }
public enum LaneType { Up, Down, Left, Right }

[System.Serializable]
public class NoteData
{
    public float time, speed;
    public LaneType lane;
    public NoteType type;
}