#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SetupGame : MonoBehaviour
{
    [MenuItem("Tools/리듬게임/게임 설정")]
    public static void Setup()
    {
        // 필요한 오브젝트 생성
        CreateGameManager();
        CreateNoteManager();
        CreateInputManager();
        
        // 노트 프리팹 확인
        CheckNotePrefab();
        
        Debug.Log("게임 설정이 완료되었습니다!");
    }
    
    // GameManager 생성
    static void CreateGameManager()
    {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        if (gameManagerObj == null)
        {
            gameManagerObj = new GameObject("GameManager");
            gameManagerObj.AddComponent<GameManager>();
            Debug.Log("GameManager가 생성되었습니다.");
        }
        else if (gameManagerObj.GetComponent<GameManager>() == null)
        {
            gameManagerObj.AddComponent<GameManager>();
            Debug.Log("GameManager 컴포넌트가 추가되었습니다.");
        }
    }
    
    // NoteManager 생성
    static void CreateNoteManager()
    {
        GameObject noteManagerObj = GameObject.Find("NoteManager");
        if (noteManagerObj == null)
        {
            noteManagerObj = new GameObject("NoteManager");
            note_manager manager = noteManagerObj.AddComponent<note_manager>();
            
            // 노트 프리팹 할당
            string prefabPath = "Assets/Prefabs/Note.prefab";
            GameObject notePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (notePrefab != null)
            {
                manager.notePrefab = notePrefab;
                Debug.Log("노트 프리팹이 NoteManager에 할당되었습니다.");
            }
            else
            {
                Debug.LogWarning("노트 프리팹을 찾을 수 없습니다. 먼저 노트 프리팹을 생성해주세요.");
            }
            
            Debug.Log("NoteManager가 생성되었습니다.");
        }
        else if (noteManagerObj.GetComponent<note_manager>() == null)
        {
            noteManagerObj.AddComponent<note_manager>();
            Debug.Log("note_manager 컴포넌트가 추가되었습니다.");
        }
        
        // GameManager에 NoteManager 참조 설정
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        note_manager noteManager = GameObject.FindObjectOfType<note_manager>();
        
        if (gameManager != null && noteManager != null)
        {
            gameManager.noteManager = noteManager;
            Debug.Log("GameManager에 NoteManager 참조가 설정되었습니다.");
        }
    }
    
    // InputManager 생성
    static void CreateInputManager()
    {
        GameObject inputManagerObj = GameObject.Find("InputManager");
        if (inputManagerObj == null)
        {
            inputManagerObj = new GameObject("InputManager");
            inputManagerObj.AddComponent<InputManager>();
            Debug.Log("InputManager가 생성되었습니다.");
        }
        else if (inputManagerObj.GetComponent<InputManager>() == null)
        {
            inputManagerObj.AddComponent<InputManager>();
            Debug.Log("InputManager 컴포넌트가 추가되었습니다.");
        }
    }
    
    // 노트 프리팹 확인 및 생성
    static void CheckNotePrefab()
    {
        string prefabPath = "Assets/Prefabs/Note.prefab";
        GameObject notePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        
        if (notePrefab == null)
        {
            // 프리팹 폴더 확인
            if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
                Debug.Log("Prefabs 폴더가 생성되었습니다.");
            }
            
            // 노트 프리팹 생성
            GameObject noteObj = new GameObject("Note");
            
            // 스프라이트 렌더러 추가
            SpriteRenderer renderer = noteObj.AddComponent<SpriteRenderer>();
            
            // 콜라이더 추가
            BoxCollider2D collider = noteObj.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(1, 1);
            
            // NoteController 추가
            noteObj.AddComponent<NoteController>();
            
            // 프리팹 저장
            PrefabUtility.SaveAsPrefabAsset(noteObj, prefabPath);
            
            // 임시 오브젝트 제거
            GameObject.DestroyImmediate(noteObj);
            
            Debug.Log("노트 프리팹이 생성되었습니다.");
        }
        else
        {
            // NoteController 컴포넌트 확인
            NoteController controller = notePrefab.GetComponent<NoteController>();
            if (controller == null)
            {
                // 프리팹 수정 시작
                GameObject tempObj = PrefabUtility.InstantiatePrefab(notePrefab) as GameObject;
                tempObj.AddComponent<NoteController>();
                
                // 프리팹 저장
                PrefabUtility.SaveAsPrefabAsset(tempObj, prefabPath);
                
                // 임시 오브젝트 제거
                GameObject.DestroyImmediate(tempObj);
                
                Debug.Log("노트 프리팹에 NoteController 컴포넌트가 추가되었습니다.");
            }
            
            // BoxCollider2D 컴포넌트 확인
            BoxCollider2D collider = notePrefab.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                // 프리팹 수정 시작
                GameObject tempObj = PrefabUtility.InstantiatePrefab(notePrefab) as GameObject;
                BoxCollider2D newCollider = tempObj.AddComponent<BoxCollider2D>();
                newCollider.size = new Vector2(1, 1);
                
                // 프리팹 저장
                PrefabUtility.SaveAsPrefabAsset(tempObj, prefabPath);
                
                // 임시 오브젝트 제거
                GameObject.DestroyImmediate(tempObj);
                
                Debug.Log("노트 프리팹에 BoxCollider2D 컴포넌트가 추가되었습니다.");
            }
        }
    }
}
#endif 