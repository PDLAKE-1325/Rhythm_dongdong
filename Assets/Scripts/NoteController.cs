using UnityEngine;

public class NoteController : MonoBehaviour
{
    public Vector2 targetPosition; // 목표 위치
    public Vector2 direction;      // 이동 방향
    public float speed;            // 이동 속도
    public float lifetime;         // 생존 시간
    
    private float elapsedTime = 0f;
    private Vector2 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        // 경과 시간 업데이트
        elapsedTime += Time.deltaTime;
        
        // 선형 보간을 통한 이동
        if (elapsedTime < lifetime)
        {
            float t = elapsedTime / lifetime;
            transform.position = Vector2.Lerp(startPosition, targetPosition, t);
        }
        else
        {
            // 목표 위치에 도달했을 때 처리
            transform.position = targetPosition;
            
            // 여기서 추가 로직 구현 (예: 노트 파괴, 점수 계산 등)
            // Destroy(gameObject);
        }
    }
    
    // 노트 초기화 함수
    public void Initialize(Vector2 target, Vector2 dir, float spd, float time)
    {
        targetPosition = target;
        direction = dir;
        speed = spd;
        lifetime = time;
        startPosition = transform.position;
    }
} 