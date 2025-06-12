using UnityEngine;
using System.Collections;

public class note_manager : MonoBehaviour
{
    public GameObject notePrefab;  // 노트 프리팹 참조 (인스펙터에서 할당)
    
    private GameManager gameManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 프리팹이 할당되었는지 확인
        if (notePrefab == null)
        {
            Debug.LogError("노트 프리팹이 할당되지 않았습니다. 인스펙터에서 할당해주세요.");
        }
        
        // GameManager 참조 가져오기
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager를 찾을 수 없습니다. 기본 설정값을 사용합니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 테스트용 키 입력 (필요시 사용)
        if (Input.GetKeyDown(KeyCode.W)) SpawnNoteInDirection("north");
        if (Input.GetKeyDown(KeyCode.A)) SpawnNoteInDirection("west");
        if (Input.GetKeyDown(KeyCode.S)) SpawnNoteInDirection("south");
        if (Input.GetKeyDown(KeyCode.D)) SpawnNoteInDirection("east");
    }

    // 노트 생성 함수
    public void SpawnNote(Vector2 targetPos, Vector2 dir, float speed, float t)
    {
        // 시작 위치 계산 (목표 위치에서 방향의 반대 방향으로 거리 계산)
        Vector2 startPos = targetPos - dir * speed * t;
        
        // 노트 생성
        GameObject note = Instantiate(notePrefab, new Vector3(startPos.x, startPos.y, 0), Quaternion.identity);
        
        // NoteController 컴포넌트 가져와서 초기화
        NoteController controller = note.GetComponent<NoteController>();
        if (controller != null)
        {
            controller.Initialize(targetPos, dir, speed, t);
        }
        else
        {
            Debug.LogError("노트 프리팹에 NoteController 컴포넌트가 없습니다.");
            // 컴포넌트가 없는 경우 코루틴으로 이동 (대체 방법)
            StartCoroutine(MoveNote(note, startPos, targetPos, t));
        }
    }
    
    // 노트 이동 코루틴 (NoteController가 없는 경우 대체 방법)
    IEnumerator MoveNote(GameObject note, Vector2 startPos, Vector2 targetPos, float duration)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < duration && note != null)
        {
            // 선형 보간을 통한 이동
            float t = elapsedTime / duration;
            note.transform.position = Vector2.Lerp(startPos, targetPos, t);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // 최종 위치 설정 (정확한 도달을 위해)
        if (note != null)
        {
            note.transform.position = new Vector3(targetPos.x, targetPos.y, 0);
        }
    }
    
    // 동서남북 방향에 따른 노트 생성 도우미 함수
    public void SpawnNoteInDirection(string direction)
    {
        Vector2 targetPos = Vector2.zero;
        Vector2 dir = Vector2.zero;
        float distance = gameManager != null ? gameManager.noteDistance : 10f;  // 노트 생성 위치
        float speed = gameManager != null ? gameManager.noteSpeed : 5f;        // 노트 속도
        float time = gameManager != null ? gameManager.noteTime : 3f;          // 노트 이동 시간
        
        switch (direction.ToLower())
        {
            case "north":
            case "n":
            case "up":
                targetPos = new Vector2(0, distance);
                dir = Vector2.up;
                break;
            case "south":
            case "s":
            case "down":
                targetPos = new Vector2(0, -distance);
                dir = Vector2.down;
                break;
            case "east":
            case "e":
            case "right":
                targetPos = new Vector2(distance, 0);
                dir = Vector2.right;
                break;
            case "west":
            case "w":
            case "left":
                targetPos = new Vector2(-distance, 0);
                dir = Vector2.left;
                break;
        }
        
        SpawnNote(targetPos, dir, speed, time);
    }
}
