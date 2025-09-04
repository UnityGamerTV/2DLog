using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

public enum LanguageType
{
    Korean,
    English,
}

public static class LanguageManager
{
    public static LanguageType _CurrentLanguage { get { return CurrentLanguage; } set { CurrentLanguage = value; } }
    private static LanguageType CurrentLanguage;
}

public partial class DataManager : Singleton<DataManager>, IManager
{
    Dictionary<string, ItemData> amuletDic = new();
    Dictionary<string, ItemData> armourDic = new();
    Dictionary<string, ItemData> axeDic = new();
    Dictionary<string, ItemData> bootsDic = new();
    Dictionary<string, ItemData> bowDic = new();
    Dictionary<string, ItemData> gloveDic = new();
    Dictionary<string, ItemData> helmetDic = new();
    Dictionary<string, ItemData> maceDic = new();
    Dictionary<string, ItemData> robeDic = new();
    Dictionary<string, ItemData> ringDic = new();
    Dictionary<string, ItemData> shieldDic = new();
    Dictionary<string, ItemData> spearDic = new();
    Dictionary<string, ItemData> staffDic = new();
    Dictionary<string, ItemData> swordDic = new();
    Dictionary<string, ItemData> etcDic = new();
    Dictionary<string, PotionData> potionDic = new();
    Dictionary<string, MagicData> magicDic = new();
    Dictionary<string, ScrollData> scrollDic = new();
    Dictionary<string, MonsterData> monsterDic = new();
    Dictionary<string, LanguageData> languageDic = new();
    Dictionary<int, RandartData> randartDic = new();
    Dictionary<string, RandartOptionTable> randartOptionTableDic = new();
    public Dictionary<string,RandartOptionTable> _randartOptionTableDic { get {  return _randartOptionTableDic; } }

    public void Init()
    {
        // 언어 설정
        LanguageManager._CurrentLanguage = LanguageType.Korean;

        LoadLanguageData();
        LoadRandartData();
        LoadRandartOptionTable();
        LoadItemData();
        LoadData<PotionData>("Potion", potionDic);
        LoadData<MagicData>("Magic", magicDic);
        LoadData<ScrollData>("Scroll", scrollDic);
        LoadMonsterData();
        
    }

    // LanguageData
    private void LoadLanguageData()
    {
        string jsonFilePath = String.Empty;
        string jsonContent = String.Empty;

        // JSON 경로 
        jsonFilePath = $"Assets/Resources/Data/Language/LanguageData.json";
        // JSON Read
        jsonContent = File.ReadAllText(jsonFilePath);
        // JSON 디시리얼라이즈
        List<LanguageData> Languagelist = JsonConvert.DeserializeObject<List<LanguageData>>(jsonContent);

        foreach (var LanguageData in Languagelist)
            languageDic.Add(LanguageData._key, LanguageData);
    }

    private void LoadRandartData()
    {
        string jsonFilePath = String.Empty;
        string jsonContent = String.Empty;

        // JSON 경로 
        jsonFilePath = $"Assets/Resources/Data/Randart/RandartData.json";
        // JSON Read
        jsonContent = File.ReadAllText(jsonFilePath);
        // JSON 디시리얼라이즈
        List<RandartData> randartlist = JsonConvert.DeserializeObject<List<RandartData>>(jsonContent);

        foreach (var randartData in randartlist)
            randartDic.Add(randartData._no, randartData);
    }

    private void LoadRandartOptionTable()
    {
        string jsonFilePath = String.Empty;
        string jsonContent = String.Empty;

        // JSON 경로 
        jsonFilePath = $"Assets/Resources/Data/Table/RandartOption/RandartOptionTable.json";
        // JSON Read
        jsonContent = File.ReadAllText(jsonFilePath);
        // JSON 디시리얼라이즈
        List<RandartOptionTable> randartOptionTableList = JsonConvert.DeserializeObject<List<RandartOptionTable>>(jsonContent);

        foreach (var randartOptionTable in randartOptionTableList)
            randartOptionTableDic.Add(randartOptionTable._range, randartOptionTable);
    }

    private void LoadItemData()
    {
        string[] itemArray =
        {
            "Amulet", "Armour", "Axe", "Boots", "Bow", "Glove",
            "Helmet", "Mace", "Ring", "Robe", "Shield",
            "Spear", "Staff", "Sword", "Etc"
        };

        var itemDataList = new List<Dictionary<string, ItemData>>()
        {
            amuletDic, armourDic, axeDic, bootsDic, bowDic, gloveDic,
            helmetDic, maceDic, ringDic, robeDic, shieldDic,
            spearDic, staffDic, swordDic, etcDic
        };

        // ItemData
        for (int i = 0; i < itemArray.Length; i++)
        {
            LoadData<ItemData>(itemArray[i].ToString(), itemDataList[i]);
        }
    }

    private void LoadData<T>(string folderName, Dictionary<string, T> targetDic) where T : class, INickname
    {
        // JSON 경로
        string jsonFilePath = $"Assets/Resources/Data/{folderName}/{folderName}Data.json";

        if (!File.Exists(jsonFilePath))
        {
            LogUtil.LogError($"JSON 파일 경로가 잘못되었습니다: {jsonFilePath}");
            return;
        }

        // JSON Read
        string jsonContent = File.ReadAllText(jsonFilePath);
        // JSON 디시리얼라이즈
        List<T> dataList = JsonConvert.DeserializeObject<List<T>>(jsonContent);

        foreach (var data in dataList)
        {
            targetDic.Add(data._nickName, data);
        }

        LogUtil.Log($" {typeof(T).Name} {dataList.Count}개 로드됨");
    }

    private void LoadMonsterData()
    {
        string jsonFilePath = String.Empty;
        string jsonContent = String.Empty;

        // JSON 경로 노트북
        jsonFilePath = $"Assets/Resources/Data/Monster/MonsterData.json";
        // JSON Read
        jsonContent = File.ReadAllText(jsonFilePath);
        // JSON 디시리얼라이즈

        // 소문자 enum 데이터 대문자로 변환
        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter(new CamelCaseNamingStrategy(), allowIntegerValues: false)
            }
        };
        List<MonsterData> monsterList = JsonConvert.DeserializeObject<List<MonsterData>>(jsonContent, settings);

        foreach (var monsterData in monsterList)
            monsterDic.Add(monsterData._name, monsterData);
    }

    // 선택 언어에 따라 변경
    // 게임 시작 시 설정
    // 언어 재 선택시 DataManager 재호출 필요 (Init)
    private string GetLocalizedText(string spriteName)
    {
        if (!languageDic.TryGetValue(spriteName, out var data))
            return string.Empty;

        switch (LanguageManager._CurrentLanguage)
        {
            case LanguageType.Korean:
                return data._ko;
            case LanguageType.English:
                return data._en; 
        }
        return string.Empty;
    }

    public RandartOptionTable GetRandartTableOption(int randartOptionCount = 1)
    {
        RandartOptionTable randartOptionTable = null;

        if (randartOptionCount == 1)
            randartOptionTable = randartOptionTableDic["Option1"];
        else if (randartOptionCount == 2)
            randartOptionTable = randartOptionTableDic["Option2"];

        return randartOptionTable;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="option2">option2</param>
    /// <param name="option1">option1</param>
    public RandartData SumRandartData(RandartData option2, RandartData option1)
    {
        option2._name += option1._name;
        option2._max_hp += option1._max_hp;
        option2._max_mp += option1._max_mp;
        option2._min_attack += option1._min_attack;
        option2._max_attack += option1._max_attack;
        option2._defence += option1._defence;
        option2._min_magic_attack += option1._min_magic_attack;
        option2._max_magic_attack += option1._max_magic_attack;
        option2._fire_res += option1._fire_res;
        option2._cold_res += option1._cold_res;
        option2._earth_res += option1._earth_res;
        option2._dark_res += option1._dark_res;
        option2._poison_res += option1._poison_res;
        option2._avoid += option1._avoid;

        return option2;
    }


    public void Release()
    {
        
    }
}
