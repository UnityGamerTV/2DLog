using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

sealed public class LogUtilToggle : MonoBehaviour
{
    [MenuItem("Build/Debug Log/Enable DEBUG_LOG")]
    public static void EnableDebugLog()
    {
        SetDebugLogDefine(true);
    }

    [MenuItem("Build/Debug Log/Disable DEBUG_LOG")]
    public static void DisableDebugLog()
    {
        SetDebugLogDefine(false);
    }

    private static void SetDebugLogDefine(bool enable)
    {
        var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup).Split(';').ToList();

        if (enable && !defines.Contains("DEBUG_LOG"))
            defines.Add("DEBUG_LOG");
        else if (!enable)
            defines.RemoveAll(d => d == "DEBUG_LOG");

        PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, string.Join(";", defines));
    }
}
