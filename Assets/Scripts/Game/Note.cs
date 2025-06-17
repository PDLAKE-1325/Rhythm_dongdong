using UnityEngine;

public class Note : MonoBehaviour
{
    public NoteData note_data;
    Vector3 move_dir;
    int my_index;

    public void Init(NoteData data, Transform end_pos, int index)
    {
        note_data = data;
        my_index = index;

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

    bool indexAdded;
    void CheckPass()
    {
        float cur_time = InGameStreamManager.Instance.current_time;
        float bad_judge = JudgementSystem.Instance.bad;
        if (note_data.time < cur_time - bad_judge && !indexAdded)
        {
            indexAdded = true;
            InGameStreamManager.Instance.AddCurNodeIndex();
        }

        float distance = Vector2.Distance(transform.position, Vector2.zero);
        if (distance <= InGameStreamManager.Instance.note_damage_distance)
        {
            if (!indexAdded)
            {
                indexAdded = true;
                InGameStreamManager.Instance.AddCurNodeIndex();
            }
            JudgementSystem.Instance.MissNote(this);
        }
    }

    void CheckInput()
    {
        if (InGameStreamManager.Instance.current_node_index != my_index) return;

        if (Input.GetKeyDown(KeyCode.W) && note_data.lane == LaneType.Up)
        {
            JudgementSystem.Instance.JudgeNoteTime(this, KeyCode.W);
        }
        else if (Input.GetKeyDown(KeyCode.S) && note_data.lane == LaneType.Down)
        {
            JudgementSystem.Instance.JudgeNoteTime(this, KeyCode.S);
        }
        else if (Input.GetKeyDown(KeyCode.A) && note_data.lane == LaneType.Left)
        {
            JudgementSystem.Instance.JudgeNoteTime(this, KeyCode.A);
        }
        else if (Input.GetKeyDown(KeyCode.D) && note_data.lane == LaneType.Right)
        {
            JudgementSystem.Instance.JudgeNoteTime(this, KeyCode.D);
        }
    }

    void Update()
    {
        CheckInput();
        Move();
        CheckPass();
    }
}
