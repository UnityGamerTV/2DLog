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

public class DataManager : Singleton<DataManager>, IManager
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

    public RandartData AddRandartData(int optionNumer)
    {
        RandartData randartData = null;
        if (!randartDic.TryGetValue(optionNumer, out randartData))
            return null;

        randartData._name = GetLocalizedText(randartData._option_name);
        return randartData;
    }

    public ItemData AddItemData(string spriteName)
    {
        ItemData itemData = null;

        List<Dictionary<string, ItemData>> itemDictionaries = new()
        {
            amuletDic, armourDic, axeDic, bootsDic, bowDic,
            gloveDic, helmetDic, maceDic, ringDic, robeDic,
            shieldDic, spearDic, staffDic, swordDic, etcDic
        };

        foreach (var dic in itemDictionaries)
        {
            if (dic.TryGetValue(spriteName, out itemData))
            {
                itemData._name = GetLocalizedText(spriteName);
                itemData._comment = GetLocalizedText($"{spriteName}_comment");
                return itemData;
            }
        }

        return null;

        //if (amuletDic.TryGetValue(spriteName, out itemData))
        //    itemData = amuletDic[spriteName];
        //else if (armourDic.TryGetValue(spriteName, out itemData))
        //    itemData = armourDic[spriteName];
        //else if (axeDic.TryGetValue(spriteName, out itemData))
        //    itemData = axeDic[spriteName];
        //else if (bootsDic.TryGetValue(spriteName, out itemData))
        //    itemData = bootsDic[spriteName];
        //else if (bowDic.TryGetValue(spriteName, out itemData))
        //    itemData = bowDic[spriteName];
        //else if (gloveDic.TryGetValue(spriteName, out itemData))
        //    itemData = gloveDic[spriteName];
        //else if (helmetDic.TryGetValue(spriteName, out itemData))
        //    itemData = helmetDic[spriteName];
        //else if (maceDic.TryGetValue(spriteName, out itemData))
        //    itemData = maceDic[spriteName];
        //else if (ringDic.TryGetValue(spriteName, out itemData))
        //    itemData = ringDic[spriteName];
        //else if (robeDic.TryGetValue(spriteName, out itemData))
        //    itemData = robeDic[spriteName];
        //else if (shieldDic.TryGetValue(spriteName, out itemData))
        //    itemData = shieldDic[spriteName];
        //else if (spearDic.TryGetValue(spriteName, out itemData))
        //    itemData = spearDic[spriteName];
        //else if (staffDic.TryGetValue(spriteName, out itemData))
        //    itemData = staffDic[spriteName];
        //else if (swordDic.TryGetValue(spriteName, out itemData))
        //    itemData = swordDic[spriteName];
        //else if (etcDic.TryGetValue(spriteName, out itemData))
        //    itemData = etcDic[spriteName];

        //itemData._name = GetLocalizedText(spriteName);
        //itemData._comment = GetLocalizedText($"{spriteName}_comment");
        
        //return itemData;
    }

    public PotionData AddPotionData(string spriteName)
    {
        if (!potionDic.TryGetValue(spriteName, out var data))
            return null;

        data._name = GetLocalizedText(spriteName);
        data._comment = GetLocalizedText($"{spriteName}_comment");

        return data;
    }

    public ScrollData AddScrollData(string spriteName)
    {
        if (!scrollDic.TryGetValue(spriteName, out var data))
            return null;

        data._name = GetLocalizedText(spriteName);
        data._comment = GetLocalizedText($"{spriteName}_comment");

        return data;
    }

    public MagicData AddMagicData(string spriteName)
    {
        if (!magicDic.TryGetValue(spriteName, out var data))
            return null;

        data._name = GetLocalizedText(spriteName);
        data._comment = GetLocalizedText($"{spriteName}_comment");

        return data;
    }

    public MonsterData AddMonsterData(string spriteName)
    {
        if (!monsterDic.TryGetValue(spriteName, out var data))
            return null;

        data._name = GetLocalizedText(spriteName);
        data._comment = GetLocalizedText($"{spriteName}_comment");

        return data;
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

    public void CopyRandartData(ItemDataComponent target, RandartData newRandartData)
    {
        target._no = newRandartData._no;
        target._name = newRandartData._name + target._name;
        target._max_hp += newRandartData._max_hp;
        target._max_mp += newRandartData._max_mp;
        target._min_attack += newRandartData._min_attack;
        target._max_attack += newRandartData._max_attack;
        target._defence += newRandartData._defence;
        target._min_magic_attack += newRandartData._min_magic_attack;
        target._max_magic_attack += newRandartData._max_magic_attack;
        target._fire_res += newRandartData._fire_res;
        target._cold_res += newRandartData._cold_res;
        target._earth_res += newRandartData._earth_res;
        target._dark_res += newRandartData._dark_res;
        target._poison_res += newRandartData._poison_res;
        target._avoid += newRandartData._avoid;
    }

    public void CopyItemData(ItemDataComponent target, ItemData newItemData)
    {
        target._no = newItemData._no;
        target._name = newItemData._name;
        target._max_hp = newItemData._max_hp;
        target._max_mp = newItemData._max_mp;
        target._min_attack = newItemData._min_attack;
        target._max_attack = newItemData._max_attack;
        target._defence = newItemData._defence;
        target._min_magic_attack = newItemData._min_magic_attack;
        target._max_magic_attack = newItemData._max_magic_attack;
        target._fire_res = newItemData._fire_res;
        target._cold_res = newItemData._cold_res;
        target._earth_res = newItemData._earth_res;
        target._dark_res = newItemData._dark_res;
        target._poison_res = newItemData._poison_res;
        target._avoid = newItemData._avoid;
        target._item_property_type = newItemData._item_property_type;
        target._hand = newItemData._hand;
        target._enhance_limit = newItemData._enhance_limit;
        target._skill_limit = newItemData._skill_limit;
        target._nickName = newItemData._nickName;
        target._comment = newItemData._comment;
    }

    public void CopyPotionData(PotionDataComponent target, PotionData newPotionData)
    {
        target._no = newPotionData._no;
        target._name = newPotionData._name;
        target._max_hp = newPotionData._max_hp;
        target._current_hp = newPotionData._current_hp;
        target._max_mp = newPotionData._max_mp;
        target._current_mp = newPotionData._current_mp;
        target._min_attack = newPotionData._min_attack;
        target._max_attack = newPotionData._max_attack;
        target._defence = newPotionData._defence;
        target._min_magic_attack = newPotionData._min_magic_attack;
        target._max_magic_attack = newPotionData._max_magic_attack;
        target._fire_res = newPotionData._fire_res;
        target._cold_res = newPotionData._cold_res;
        target._earth_res = newPotionData._earth_res;
        target._dark_res = newPotionData._dark_res;
        target._poison_res = newPotionData._poison_res;
        target._avoid = newPotionData._avoid;
        target._turn = newPotionData._turn;
        target._nickName = newPotionData._nickName;
        target._comment = newPotionData._comment;
    }

    public void CopyScrollData(ScrollDataComponent target, ScrollData newScrollData)
    {
        target._no = newScrollData._no;
        target._name = newScrollData._name;
        target._avoid = newScrollData._avoid;
        target._enhance = newScrollData._enhance;
        target._nickName = newScrollData._nickName;
        target._comment = newScrollData._comment;
    }

    public void CopyMagicData(MagicDataComponent target, MagicData newMagicData)
    {
        target._no = newMagicData._no;
        target._name = newMagicData._name;
        target._base_damage = newMagicData._base_damage;
        target._reach = newMagicData._reach;
        target._range = newMagicData._range;
        target._magic_attack_type = newMagicData._magic_attack_type;
        target._mp_consume = newMagicData._mp_consume;
        target._skill_limit = newMagicData._skill_limit;
        target._magic_property_type = newMagicData._magic_property_type;
        target._turn = newMagicData._turn;
        target._magic_effect_type = newMagicData._magic_effect_type;
        target._max_hp = newMagicData._max_hp;
        target._max_mp = newMagicData._max_mp;
        target._defence = newMagicData._defence;
        target._min_magic_attack = newMagicData._min_magic_attack;
        target._max_magic_attack = newMagicData._max_magic_attack;
        target._fire_res = newMagicData._fire_res;
        target._cold_res = newMagicData._cold_res;
        target._earth_res = newMagicData._earth_res;
        target._dark_res = newMagicData._dark_res;
        target._poison_res = newMagicData._poison_res;
        target._avoid = newMagicData._avoid;
        target._nickName = newMagicData._nickName;
        target._icon = newMagicData._icon;
        target._comment = newMagicData._comment;
    }

    public void CopyMonsterData(MonsterDataComponent target, MonsterData newMonsterData)
    {
        target._name = newMonsterData._name;
        target._current_hp = newMonsterData._max_hp;
        target._max_hp = newMonsterData._max_hp;
        target._min_attack = newMonsterData._min_attack;
        target._max_attack = newMonsterData._max_attack;
        target._defence = newMonsterData._defence;
        target._fire_res = newMonsterData._fire_res;
        target._cold_res = newMonsterData._cold_res;
        target._earth_res = newMonsterData._earth_res;
        target._dark_res = newMonsterData._dark_res;
        target._poison_res = newMonsterData._poison_res;
        target._attackType = newMonsterData._attackType;
        target._magic1 = newMonsterData._magic1;
        target._magic2 = newMonsterData._magic2;
        target._comment = newMonsterData._comment;
    }


    public void Release()
    {
        
    }
}
