using UnityEngine;

public class Note : MonoBehaviour
{
    NoteData note_data;
    public void Init(NoteData data, Transform end_pos)
    {
        note_data = data;

        float cur_time = InGameStreamManager.Instance.current_time;
        float distance = data.speed * (data.time - cur_time);
        Vector2 move_dir = new();

        switch (data.lane)
        {
            case LaneType.Up:
                move_dir = Vector2.down; break;
            case LaneType.Down:
                move_dir = Vector2.up; break;
            case LaneType.Left:
                move_dir = Vector2.right; break;
            case LaneType.Right:
                move_dir = Vector2.left; break;
        }

        transform.position = end_pos.position + (Vector3)(move_dir * distance);
    } // 체력 다 줄면 죽음
    void Move()
    {

    }
    void Update()
    {
        Move();
    }
}
