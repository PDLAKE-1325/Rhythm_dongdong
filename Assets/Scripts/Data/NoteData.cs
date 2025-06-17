public enum NoteType { Default, Move }
public enum LaneType { Up, Down, Left, Right }

[System.Serializable]
public class NoteData
{
    public double time;
    public float speed;
    public LaneType lane;
    public NoteType type;

    public NoteData(double time, float speed, LaneType lane, NoteType type)
    {
        this.time = time;
        this.speed = speed;
        this.lane = lane;
        this.type = type;
    }
}