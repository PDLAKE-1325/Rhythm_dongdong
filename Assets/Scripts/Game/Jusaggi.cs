using UnityEngine;

public class Jusaggi : MonoBehaviour
{
    [SerializeField, Range(7f, 35f)] float rotate_speed = 10;
    Vector2 direction;

    void Rotate()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * rotate_speed);
    }

    void SetTargetRot()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector2.left;
        }
    }

    void Update()
    {
        SetTargetRot();
        Rotate();
    }
}
