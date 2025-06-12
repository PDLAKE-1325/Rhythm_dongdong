using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    [Header("입력 설정")]
    public KeyCode upKey = KeyCode.W;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode downKey = KeyCode.S;
    public KeyCode rightKey = KeyCode.D;
    
    [Header("판정 설정")]
    public float perfectRange = 0.1f;  // 퍼펙트 판정 범위 (초)
    public float goodRange = 0.3f;     // 좋음 판정 범위 (초)
    public float okRange = 0.5f;       // 보통 판정 범위 (초)
    
    [Header("참조")]
    public Transform northTarget;      // 북쪽 타겟 위치
    public Transform eastTarget;       // 동쪽 타겟 위치
    public Transform southTarget;      // 남쪽 타겟 위치
    public Transform westTarget;       // 서쪽 타겟 위치
    
    // 히트 박스 위치 설정
    private Vector2 northHitPos = new Vector2(0, 150);
    private Vector2 eastHitPos = new Vector2(150, 0);
    private Vector2 southHitPos = new Vector2(0, -150);
    private Vector2 westHitPos = new Vector2(-150, 0);
    
    private Dictionary<KeyCode, string> keyDirections;
    private Dictionary<string, Vector2> hitPositions;
    
    void Start()
    {
        // 키와 방향 매핑
        keyDirections = new Dictionary<KeyCode, string>
        {
            { upKey, "north" },
            { leftKey, "west" },
            { downKey, "south" },
            { rightKey, "east" }
        };
        
        // 방향과 히트 박스 위치 매핑
        hitPositions = new Dictionary<string, Vector2>
        {
            { "north", northHitPos },
            { "east", eastHitPos },
            { "south", southHitPos },
            { "west", westHitPos }
        };
        
        // 타겟 위치 설정 (타겟 오브젝트가 있는 경우)
        if (northTarget) northTarget.position = northHitPos;
        if (eastTarget) eastTarget.position = eastHitPos;
        if (southTarget) southTarget.position = southHitPos;
        if (westTarget) westTarget.position = westHitPos;
    }
    
    void Update()
    {
        // 키 입력 확인
        foreach (var key in keyDirections.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                string direction = keyDirections[key];
                CheckNoteHit(direction);
            }
        }
    }
    
    // 노트 판정 확인
    void CheckNoteHit(string direction)
    {
        if (!hitPositions.ContainsKey(direction))
        {
            Debug.LogWarning($"{direction} 방향의 히트 위치가 설정되지 않았습니다.");
            return;
        }
        
        Vector2 hitPos = hitPositions[direction];
        
        // 타겟 주변의 노트 검색
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitPos, 1f);
        
        NoteController closestNote = null;
        float closestDistance = float.MaxValue;
        
        // 가장 가까운 노트 찾기
        foreach (var collider in colliders)
        {
            NoteController note = collider.GetComponent<NoteController>();
            if (note != null)
            {
                // 방향 확인 (노트의 방향과 입력 방향이 일치하는지)
                Vector2 noteDir = note.direction.normalized;
                Vector2 expectedDir = GetDirectionVector(direction).normalized;
                
                // 방향이 일치하면 거리 계산
                if (Vector2.Dot(noteDir, expectedDir) > 0.5f)
                {
                    float distance = Vector2.Distance(collider.transform.position, hitPos);
                    if (distance < closestDistance)
                    {
                        closestNote = note;
                        closestDistance = distance;
                    }
                }
            }
        }
        
        // 노트 판정
        if (closestNote != null)
        {
            JudgeNote(closestNote, closestDistance);
        }
        else
        {
            Debug.Log("노트 없음 - 미스!");
        }
    }
    
    // 노트 판정 로직
    void JudgeNote(NoteController note, float distance)
    {
        // 거리에 따른 판정
        if (distance < perfectRange)
        {
            Debug.Log("퍼펙트!");
            // 점수 추가, 이펙트 등
        }
        else if (distance < goodRange)
        {
            Debug.Log("좋음!");
            // 점수 추가, 이펙트 등
        }
        else if (distance < okRange)
        {
            Debug.Log("보통!");
            // 점수 추가, 이펙트 등
        }
        else
        {
            Debug.Log("미스!");
            // 미스 처리
        }
        
        // 노트 제거
        Destroy(note.gameObject);
    }
    
    // 방향 문자열을 벡터로 변환
    Vector2 GetDirectionVector(string direction)
    {
        switch (direction.ToLower())
        {
            case "north": return Vector2.up;
            case "east": return Vector2.right;
            case "south": return Vector2.down;
            case "west": return Vector2.left;
            default: return Vector2.zero;
        }
    }
} 