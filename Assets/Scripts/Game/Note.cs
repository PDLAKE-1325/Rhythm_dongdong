using UnityEngine;

public class Note : MonoBehaviour
{
    public NoteData note_data;
    Vector3 move_dir;

    public void Init(NoteData data, Transform end_pos)
    {
        note_data = data;

        float cur_time = InGameStreamManager.Instance.current_time;
        float distance = data.speed * (data.time - cur_time);
        Vector2 move_vector = new();

        switch (data.lane)
        {
            case LaneType.Up:
                move_dir = Vector2.down;
                move_vector = Vector2.up;
                break;
            case LaneType.Down:
                move_dir = Vector2.up;
                move_vector = Vector2.down;
                break;
            case LaneType.Left:
                move_dir = Vector2.right;
                move_vector = Vector2.left;
                break;
            case LaneType.Right:
                move_dir = Vector2.left;
                move_vector = Vector2.right;
                break;
        }

        transform.position = end_pos.position + (Vector3)(move_vector * distance);
    }

    void Move()
    {
        transform.position += move_dir * Time.deltaTime * note_data.speed;
    }

    void Damage()
    {
        float distance = Vector2.Distance(transform.position, Vector2.zero);
        if (distance <= InGameStreamManager.Instance.note_damage_distance)
        {
            JudgementSystem.Instance.MissNote(this);
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && note_data.lane == LaneType.Up
        || Input.GetKeyDown(KeyCode.S) && note_data.lane == LaneType.Down
        || Input.GetKeyDown(KeyCode.A) && note_data.lane == LaneType.Left
        || Input.GetKeyDown(KeyCode.D) && note_data.lane == LaneType.Right)
        {
            JudgementSystem.Instance.JudgeNoteTime(this);
        }
    }

    void Update()
    {
        CheckInput();
        Move();
        Damage();
    }
}
