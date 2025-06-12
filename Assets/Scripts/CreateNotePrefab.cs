#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class CreateNotePrefab : MonoBehaviour
{
    [MenuItem("Tools/리듬게임/노트 프리팹 생성")]
    public static void CreatePrefab()
    {
        // 기본 게임 오브젝트 생성
        GameObject noteObj = new GameObject("Note");
        
        // 스프라이트 렌더러 추가
        SpriteRenderer renderer = noteObj.AddComponent<SpriteRenderer>();
        
        // 기본 스프라이트 설정 (기본 스프라이트가 없으므로 Unity 기본 스프라이트 사용)
        // 실제로는 프로젝트에 있는 스프라이트를 사용해야 합니다
        
        // 노트 컨트롤러 추가
        noteObj.AddComponent<NoteController>();
        
        // 콜라이더 추가 (필요시)
        noteObj.AddComponent<BoxCollider2D>();
        
        // 프리팹 저장 경로 확인
        string prefabPath = "Assets/Prefabs";
        if (!AssetDatabase.IsValidFolder(prefabPath))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }
        
        // 프리팹 생성
        string completePath = prefabPath + "/Note.prefab";
        
        // 이미 존재하는 프리팹 확인
        bool exists = AssetDatabase.LoadAssetAtPath<GameObject>(completePath) != null;
        
        // 프리팹 생성 또는 업데이트
        PrefabUtility.SaveAsPrefabAsset(noteObj, completePath);
        
        // 씬에서 임시 오브젝트 제거
        DestroyImmediate(noteObj);
        
        Debug.Log(exists ? "노트 프리팹이 업데이트되었습니다." : "노트 프리팹이 생성되었습니다.");
    }
}
#endif 