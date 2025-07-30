using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class CSVToJsonEditor : EditorWindow
{
    // 노트북 경로

    // 빌드 시 삭제
    string csvPath = $"C:/Users/김용현/OneDrive/바탕 화면/유니티/2D Log 2021/Assets/CSV/AmuletData.csv";
    string jsonPath = $"C:/Users/김용현/OneDrive/바탕 화면/유니티/2D Log 2021/Assets/Resources/Data/Amulet/AmuletData.json";

    [MenuItem("Tools/CSV TO JSON")]
    public static void ShowWindow()
    {
        Debug.Log("윈도우 호출됨");
        var window = CreateInstance<CSVToJsonEditor>();
        window.titleContent = new GUIContent("CSV to JSON Converter");
        window.position = new Rect(300, 300, 1000, 600);
        window.ShowUtility();  // 💥 Show() 대신 ShowUtility()
    }

    void OnGUI()
    {
        GUILayout.Label("CSV TO JSON", EditorStyles.boldLabel);

        GUILayout.Space(10);

        csvPath = EditorGUILayout.TextField("CSV 경로 : ", csvPath);
        jsonPath = EditorGUILayout.TextField("JSON 경로 : ", jsonPath);

        GUILayout.Space(10);
        if (GUILayout.Button("ConvertCSVToJson"))
        {
            ConvertCSVToJson(csvPath, jsonPath);
        }
    }

    void ConvertCSVToJson(string csvFilePath, string jsonFilePath)
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV 를 찾을 수 없습니다.: " + csvFilePath);
            return;
        }

        var lines = File.ReadAllLines(csvFilePath);
        if (lines.Length < 2)
        {
            Debug.LogWarning("CSV 데이터가 없습니다.");
            return;
        }

        string[] headers = lines[0].Split(',');
        var dataList = new List<Dictionary<string, string>>();

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');
            var entry = new Dictionary<string, string>();

            for (int j = 0; j < headers.Length && j < values.Length; j++)
            {
                entry[headers[j]] = values[j];
            }

            dataList.Add(entry);
        }

        string json = JsonConvert.SerializeObject(dataList, Formatting.Indented);
        File.WriteAllText(jsonFilePath, json, System.Text.Encoding.UTF8);

        Debug.Log($"CSV JSON 변환 성공: {jsonFilePath}");
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }
}
