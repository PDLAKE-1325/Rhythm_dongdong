using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SplitAndPaste : MonoBehaviour
{
    private enum SaveMusic
    {
        Power,
        Halo,
        OIIA,
        Candy,
    }
    [Header("주 설정")]
    [SerializeField] bool use;
    [SerializeField] bool randomLane;
    [SerializeField] SaveMusic saveMusicType;
    [SerializeField] float default_speed;
    [SerializeField] string input;
    [SerializeField] NoteTimeSave dataSO;
    List<NoteData> notes = new();
    List<double> times = new();
    LaneType[] laneOffset = { LaneType.Up, LaneType.Left, LaneType.Right, LaneType.Down };
    void Start()
    {
        if (!use) return;
        times = ParseStringToDoubleList(input);
        foreach (double time in times)
        {
            int rnd = 2;
            if (randomLane)
                rnd = Random.Range(0, 4);

            NoteData newNote = new(time, default_speed, laneOffset[rnd], NoteType.Default);
            notes.Add(newNote);
        }
        if (saveMusicType == SaveMusic.Power)
        {
            dataSO.MorePlastic_Power = notes;
        }
        else if (saveMusicType == SaveMusic.Halo)
        {
            dataSO.Tako_Halo = notes;
        }
        else if (saveMusicType == SaveMusic.OIIA)
        {
            dataSO.WW_OIIAOIIA = notes;
        }
        else if (saveMusicType == SaveMusic.Candy)
        {
            dataSO.BEATPELLAHOUSE_CandyThief = notes;
        }
#if UNITY_EDITOR
        // 에셋을 변경된 상태로 표시
        EditorUtility.SetDirty(dataSO);

        // 모든 변경 사항 저장
        AssetDatabase.SaveAssets();
        Debug.Log("GameData 저장 완료");
#else
        Debug.LogWarning("런타임 빌드에서는 ScriptableObject 저장이 지원되지 않음.");
#endif
    }

    List<double> ParseStringToDoubleList(string input)
    {
        List<double> result = new List<double>();
        string[] parts = input.Split('|');

        foreach (string part in parts)
        {
            if (double.TryParse(part, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                result.Add(value / 1000);
            }
        }

        return result;
    }
}
