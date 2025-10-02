using UnityEngine;
using UnityEditor;
using System.IO;

public class PrefabRootTagAssigner : AssetPostprocessor
{
    // 프리팹이 임포트될 때 자동 실행
    private static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        foreach (string assetPath in importedAssets)
        {
            // .prefab 파일이고, 타겟 폴더 안에 있는지 확인
            if (assetPath.EndsWith(".prefab") && IsInTargetFolder(assetPath))
            {
                AssignPrefabRootTag(assetPath);
            }
        }
    }

    // 해당 경로가 타겟 폴더 안에 있는지 확인
    private static bool IsInTargetFolder(string path)
    {
        foreach (string folder in TARGET_FOLDERS)
        {
            if (path.StartsWith(folder))
                return true;
        }
        return false;
    }

    private static void AssignPrefabRootTag(string prefabPath)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        if (prefab == null)
            return;

        // 프리팹 루트에 태그 설정
        if (prefab.tag != "PrefabRoot")
        {
            // 태그가 존재하는지 확인
            if (!TagExists("PrefabRoot"))
            {
                AddTag("PrefabRoot");
            }

            prefab.tag = "PrefabRoot";

            // 프리팹 저장
            PrefabUtility.SavePrefabAsset(prefab);

            Debug.Log($"프리팹 루트 태그 설정 완료: {prefabPath}");
        }
    }

    // 태그가 존재하는지 확인
    private static bool TagExists(string tag)
    {
        SerializedObject tagManager = new SerializedObject(
            AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(tag))
                return true;
        }

        return false;
    }

    // 새 태그 추가
    private static void AddTag(string tag)
    {
        SerializedObject tagManager = new SerializedObject(
            AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        // 빈 슬롯 찾기
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (string.IsNullOrEmpty(t.stringValue))
            {
                t.stringValue = tag;
                tagManager.ApplyModifiedProperties();
                Debug.Log($"새 태그 추가: {tag}");
                return;
            }
        }

        // 빈 슬롯이 없으면 새로 추가
        tagsProp.InsertArrayElementAtIndex(tagsProp.arraySize);
        SerializedProperty newTag = tagsProp.GetArrayElementAtIndex(tagsProp.arraySize - 1);
        newTag.stringValue = tag;
        tagManager.ApplyModifiedProperties();
        Debug.Log($"새 태그 추가: {tag}");
    }

    // 특정 폴더의 프리팹만 처리 (여러 폴더 지정 가능)
    private static readonly string[] TARGET_FOLDERS = new string[]
    {
        "Assets/Resources",           // 여기에 원하는 폴더 경로 추가
        // 필요한 만큼 추가...
    };

    // 메뉴에서 수동으로 실행할 수 있는 옵션
    [MenuItem("Tools/Assign PrefabRoot Tag to Target Folders")]
    private static void AssignTagToTargetFolders()
    {
        int count = 0;

        foreach (string folder in TARGET_FOLDERS)
        {
            // 폴더가 존재하는지 확인
            if (!AssetDatabase.IsValidFolder(folder))
            {
                Debug.LogWarning($"폴더를 찾을 수 없습니다: {folder}");
                continue;
            }

            // 특정 폴더 내의 프리팹만 검색
            string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { folder });

            foreach (string guid in prefabGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                AssignPrefabRootTag(path);
                count++;
            }

            Debug.Log($"{folder}: {prefabGuids.Length}개 프리팹 처리 완료");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"총 {count}개의 프리팹에 PrefabRoot 태그를 설정했습니다.");
    }

    // 모든 프리팹에 적용 (기존 기능 유지)
    [MenuItem("Tools/Assign PrefabRoot Tag to ALL Prefabs (Slow)")]
    private static void AssignTagToAllPrefabs()
    {
        if (!EditorUtility.DisplayDialog("경고",
            "모든 프리팹을 처리하면 시간이 오래 걸릴 수 있습니다.\n계속하시겠습니까?",
            "계속", "취소"))
        {
            return;
        }

        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        int count = 0;

        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AssignPrefabRootTag(path);
            count++;

            // 진행상황 표시
            if (count % 10 == 0)
            {
                EditorUtility.DisplayProgressBar("프리팹 태그 설정 중",
                    $"{count}/{prefabGuids.Length}",
                    (float)count / prefabGuids.Length);
            }
        }

        EditorUtility.ClearProgressBar();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"총 {count}개의 프리팹에 PrefabRoot 태그를 설정했습니다.");
    }
}