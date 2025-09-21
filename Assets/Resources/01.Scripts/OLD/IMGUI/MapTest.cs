using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class MapTest : MonoBehaviour
{
    bool showTextField = false;
    bool offGUI = false;
    //string userInput = "Map/Base2/Map_001";
    string userInput = "Base2";
    string userInput2 = "Map_001";

    [Singleton(typeof(TestManager))] private TestManager testManager;
    [Singleton(typeof(MapManager))] private MapManager mapManager;
    [Singleton(typeof(UIManager))] private UIManager uiManager;

    private void OnGUI()
    {
        if (offGUI)
            return;

        // Make a background box
        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.fontSize = 30;

        GUI.Box(new Rect(10, 10, 200, 780), "Map Test", boxStyle);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 30;

        if (GUI.Button(new Rect(20, 80, 180, 100), "Map Test", buttonStyle))
        {
            TestMapData();
        }

        if (GUI.Button(new Rect(20, 200, 180, 100),"LoadMap", buttonStyle))
        {
            showTextField = true;
        }

        if (GUI.Button(new Rect(20, 320, 180, 100), "DestroyMap", buttonStyle))
        {
            DestroyMap();
        }

        if (GUI.Button(new Rect(20, 440, 180, 100), "NextAnim", buttonStyle))
        {
            NextMobAnim();
        }

        if (GUI.Button(new Rect(20, 560, 180, 100), "OFF GUI", buttonStyle))
        {
            offGUI = true;
        }

        GUIStyle textFieldStyle = new GUIStyle(GUI.skin.textField);
        textFieldStyle.fontSize = 30;

        if (showTextField)
        {
            userInput = GUI.TextField(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, 100), userInput, textFieldStyle);
            userInput2 = GUI.TextField(new Rect(Screen.width / 4, Screen.height / 2 + 100, Screen.width / 2, 100), userInput2, textFieldStyle);
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + 400 / 2, 180, 100), "확인", buttonStyle))
            {
                LoadMap(userInput, userInput2);
                showTextField = false; // 한글 테스트
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 400 / 2, 180, 100), "취소", buttonStyle))
            {
                showTextField = false;
            }
        }
    }

    public void TestMapData()
    {
        // JSON 경로
        string jsonFilePath = $"Assets/Resources/Map/Base2/Map_001.json";

        // JSON 읽기
        string jsonContent = File.ReadAllText(jsonFilePath);

        // JSON 디시리얼라이즈
        MapData mapData = JsonConvert.DeserializeObject<MapData>(jsonContent);

        // 로그 확인
        Debug.Log($"Map Name: {mapData.name}");
        Debug.Log($"Bounds: xMin={mapData.bounds.xMin}, xMax={mapData.bounds.xMax}, yMin={mapData.bounds.yMin}, yMax={mapData.bounds.yMax}");
        Debug.Log("Monster List:");
        foreach (var monster in mapData.monsterList)
        {
            Debug.Log(monster);
        }


        // 텍스트 맵 데이터
        string textFilePath = $"Assets/Resources/Map/Base2/Map_001.txt";

        // 
        string[] lines = File.ReadAllLines(textFilePath);

        // xMin, xMax, yMin, yMax 
        string[] boundsLine = lines[0].Split(':');
        int xMin = int.Parse(boundsLine[1].Trim());
        int xMax = int.Parse(lines[1].Split(':')[1].Trim());
        int yMin = int.Parse(lines[2].Split(':')[1].Trim());
        int yMax = int.Parse(lines[3].Split(':')[1].Trim());

        // CollisionTiles
        List<string> collisionTiles = new List<string>();
        int i = 0;
        while (i < lines.Length && lines[i] != "itemTiles")
        {
            collisionTiles.Add(lines[i]);
            i++;
        }

        // itemTiles
        List<string> itemTiles = new List<string>();
        while (i < lines.Length && lines[i] != "monsterTiles")
        {
            itemTiles.Add(lines[i]);
            i++;
        }

        // monsterTiles
        List<string> monsterTiles = new List<string>();
        while (i < lines.Length)
        {
            monsterTiles.Add(lines[i]);
            i++;
        }

        // 각 타일 확인
        Debug.Log($"Bounds: xMin={xMin}, xMax={xMax}, yMin={yMin}, yMax={yMax}");
        Debug.Log("Collision Tiles:");
        foreach (var tile in collisionTiles)
        {
            Debug.Log(tile);
        }
        Debug.Log("Item Tiles:");
        foreach (var tile in itemTiles)
        {
            Debug.Log(tile);
        }
        Debug.Log("Monster Tiles:");
        foreach (var tile in monsterTiles)
        {
            Debug.Log(tile);
        }
    }

    public void LoadMap(string mapName, string mapNum)
    {
        InjectUtil.InjectSingleton(this);

        mapManager.GenerateMap(mapName, mapNum);

        var UI_Scene_Ability_Inven = uiManager.ShowSceneUI<UI_Scene_Ability_InvenController>(UI_SCENE_ENUM.UI_Scene_Ability_Inven);
        UI_Scene_Ability_Inven.gameObject.SetActive(false); // 임시 코드
        uiManager.ShowSceneUI<UI_Scene_Bottom_BaseController>(UI_SCENE_ENUM.UI_Scene_Bottom_Base);
        uiManager.ShowSceneUI<UI_Scene_Bottom_DirController>(UI_SCENE_ENUM.UI_Scene_Bottom_Dir);
        uiManager.ShowSceneUI<UI_Scene_Equip_InvenController>(UI_SCENE_ENUM.UI_Scene_Equip_Inven);
        var UI_Bottom_Consume_Inven = uiManager.ShowSceneUI<UI_Scene_Consume_InvenController>(UI_SCENE_ENUM.UI_Scene_Consume_Inven);
        UI_Bottom_Consume_Inven.gameObject.SetActive(false); // 임시 코드
        uiManager.ShowSceneUI<UI_Scene_HotkeyController>(UI_SCENE_ENUM.UI_Scene_Hotkey);
        uiManager.ShowSceneUI<UI_Scene_Top_CurrencyController>(UI_SCENE_ENUM.UI_Scene_Top_Currency);
        uiManager.ShowSceneUI<UI_Scene_Top_VitalController>(UI_SCENE_ENUM.UI_Scene_Top_Vital);
        uiManager.ShowPopupUI<UI_Popup_ItemController>(UI_POPUP_ENUM.UI_Popup_Item);
        var UI_Popup_Skill = uiManager.ShowPopupUI<UI_Popup_SkillController>(UI_POPUP_ENUM.UI_Popup_Skill);
        UI_Popup_Skill.gameObject.SetActive(false); // 임시 코드
        var UI_Popup_Status = uiManager.ShowPopupUI<UI_Popup_StatusController>(UI_POPUP_ENUM.UI_Popup_Status);
        UI_Popup_Status.gameObject.SetActive(false); // 임시 코드
        var UI_Pupup_SkillDetail = uiManager.ShowPopupUI<UI_Popup_SkillDetailController>(UI_POPUP_ENUM.UI_Popup_SkillDetail);
        UI_Pupup_SkillDetail.gameObject.SetActive(false); // 임시 코드
        var uI_Popup_AbilityDetail = uiManager.ShowPopupUI<UI_Popup_AbilityDetailController>(UI_POPUP_ENUM.UI_Popup_AbilityDetail);
        uI_Popup_AbilityDetail.gameObject.SetActive(false); // 임시 코드

    }

    public void DestroyMap()
    {
        GameObject.Destroy(mapManager._map);

        testManager.ResetAnim();
    }

    public void NextMobAnim()
    {
        InjectUtil.InjectSingleton(this);

        testManager.NextMobAnim();
    }
}
