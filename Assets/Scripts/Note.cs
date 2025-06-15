using UnityEngine;

public class Note : MonoBehaviour
{
    NoteData note_data;
    Vector3 move_vector;
    float damage_time;

    public void Init(NoteData data, Transform end_pos)
    {
        note_data = data;
        damage_time = InGameStreamManager.Instance.note_deth_time;

        float cur_time = InGameStreamManager.Instance.current_time;
        float distance = data.speed * (data.time - cur_time);
        Vector2 move_dir = new();

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

        transform.position = end_pos.position + (Vector3)(move_dir * distance);
    } // 체력 다 줄면 죽음
    void Move()
    {
        transform.position += move_vector * Time.deltaTime;
    }
    void Damage()
    {
        float cur_time = InGameStreamManager.Instance.current_time;
        if (cur_time >= note_data.time + damage_time)
        {
            //데미지 스크립트

            Destroy(gameObject);
        }
    }
    void Update()
    {
        Move();
        Damage();
    }
}
