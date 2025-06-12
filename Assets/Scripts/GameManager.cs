using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public note_manager noteManager;  // 노트 매니저 참조
    
    [Header("게임 설정")]
    public float noteSpeed = 5f;      // 노트 속도
    public float noteTime = 3f;       // 노트가 목표에 도달하는 시간
    public float noteDistance = 10f;  // 노트 생성 거리
    public float hitDistance = 150f;  // 히트 박스 거리
    
    [Header("디버그")]
    public bool autoSpawn = false;    // 자동 노트 생성 활성화
    public float spawnInterval = 2f;  // 노트 생성 간격
    
    private string[] directions = { "north", "east", "south", "west" };
    private float spawnTimer = 0f;
    
    void Start()
    {
        // 노트 매니저 참조 확인
        if (noteManager == null)
        {
            noteManager = FindObjectOfType<note_manager>();
            if (noteManager == null)
            {
                Debug.LogError("노트 매니저를 찾을 수 없습니다.");
            }
        }
        
        // 히트 박스 시각화 (디버그용)
        CreateHitBoxVisualizers();
    }
    
    void Update()
    {
        // 자동 노트 생성
        if (autoSpawn && noteManager != null)
        {
            spawnTimer += Time.deltaTime;
            
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                SpawnRandomNote();
            }
        }
        
        // 키 입력 처리 (필요시)
        // 이미 note_manager에서 처리하고 있으므로 여기서는 생략
    }
    
    // 히트 박스 시각화 (디버그용)
    void CreateHitBoxVisualizers()
    {
        CreateHitBoxVisualizer("NorthHitBox", new Vector3(0, hitDistance, 0), Color.green);
        CreateHitBoxVisualizer("EastHitBox", new Vector3(hitDistance, 0, 0), Color.red);
        CreateHitBoxVisualizer("SouthHitBox", new Vector3(0, -hitDistance, 0), Color.blue);
        CreateHitBoxVisualizer("WestHitBox", new Vector3(-hitDistance, 0, 0), Color.yellow);
    }
    
    // 히트 박스 시각화 도우미 함수
    void CreateHitBoxVisualizer(string name, Vector3 position, Color color)
    {
        GameObject visualizer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        visualizer.name = name;
        visualizer.transform.position = position;
        visualizer.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        // 렌더러 색상 설정
        Renderer renderer = visualizer.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
    
    // 랜덤 방향으로 노트 생성
    public void SpawnRandomNote()
    {
        if (noteManager != null)
        {
            int randomIndex = Random.Range(0, directions.Length);
            noteManager.SpawnNoteInDirection(directions[randomIndex]);
        }
    }
    
    // 특정 방향으로 노트 생성
    public void SpawnNoteInDirection(string direction)
    {
        if (noteManager != null)
        {
            noteManager.SpawnNoteInDirection(direction);
        }
    }
    
    // 게임 시작
    public void StartGame()
    {
        // 게임 시작 로직
        autoSpawn = true;
    }
    
    // 게임 종료
    public void EndGame()
    {
        // 게임 종료 로직
        autoSpawn = false;
    }
} 