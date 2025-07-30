using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using UnityEngine.Tilemaps;
using UnityEditor;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

public class MapTest : MonoBehaviour
{
    bool showTextField = false;
    string userInput = "Map/Base2/Map_001";
    private void OnGUI()
    {
        // Make a background box
        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.fontSize = 30;

        GUI.Box(new Rect(10, 10, 200, 480), "Map Test", boxStyle);

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

        if (GUI.Button(new Rect(20, 440, 180, 100), "NextMobAnim", buttonStyle))
        {
            NextMobAnim();
        }

        GUIStyle textFieldStyle = new GUIStyle(GUI.skin.textField);
        textFieldStyle.fontSize = 30;

        if (showTextField)
        {
            userInput = GUI.TextField(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, 100), userInput, textFieldStyle);
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + 250 / 2, 180, 100), "확인", buttonStyle))
            {
                LoadMap(userInput);
                showTextField = false; // 한글 테스트
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 250 / 2, 180, 100), "취소", buttonStyle))
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

    Dictionary<string, ItemData> amuletDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> armourDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> axeDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> bootsDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> bowDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> gloveDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> helmetDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> maceDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> robeDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> ringDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> shieldDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> spearDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> staffDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> swordDic = new Dictionary<string, ItemData>();
    Dictionary<string, ItemData> etcDic = new Dictionary<string, ItemData>();
    //
    Dictionary<string, PotionData> potionDic = new Dictionary<string, PotionData>();
    //
    Dictionary<string, MagicData> magicDic = new Dictionary<string, MagicData>();
    //
    Dictionary<string, ScrollData> scrollDic = new Dictionary<string, ScrollData>();
    //
    Dictionary<string, LanguageData> languageDic = new Dictionary<string, LanguageData>();
    //
    Dictionary<string, MonsterData> monsterDic = new Dictionary<string, MonsterData>();
    bool isRead = false;

    public void ReadData()
    {
        if (isRead)
            return;

        string jsonFilePath = String.Empty;
        string jsonContent = String.Empty;

        // LanguageData

        // JSON 경로 노트북
        jsonFilePath = $"Assets/Resources/Data/Language/LanguageData.json";
        // JSON ���� �б�
        jsonContent = File.ReadAllText(jsonFilePath);
        // JSON�� ��ü�� ��ȯ
        List<LanguageData> Languagelist = JsonConvert.DeserializeObject<List<LanguageData>>(jsonContent);

        foreach (var LanguageData in Languagelist)
        {
            languageDic.Add(LanguageData._key, LanguageData);
        }

        //

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
            // JSON 경로
            jsonFilePath = $"Assets/Resources/Data/{itemArray[i].ToString()}/{itemArray[i].ToString()}Data.json";
            if (!File.Exists(jsonFilePath))
            {
                Debug.LogError($"JSON 파일 경로가 잘못되었습니다: {jsonFilePath}");
                return;
            }

            // JSON Read
            jsonContent = File.ReadAllText(jsonFilePath);
            // JSON 디시리얼라이즈
            List<ItemData> itemlist = JsonConvert.DeserializeObject<List<ItemData>>(jsonContent);
            
            foreach (var itemData in itemlist)
            {
                // ItemData �Է�
                itemDataList[i].Add(itemData._nickName, itemData);
            }
        }
        // PotionData

        // JSON 경로
        jsonFilePath = $"Assets/Resources/Data/Potion/PotionData.json";
        // JSON Read
        jsonContent = File.ReadAllText(jsonFilePath);
        // JSON 디시리얼라이즈
        List<PotionData> potionlist = JsonConvert.DeserializeObject<List<PotionData>>(jsonContent);

        foreach (var potionData in potionlist)
        {
            // ItemData 
            potionDic.Add(potionData._nickName, potionData);
        }

        // MagicData

        // JSON 경로
        jsonFilePath = $"Assets/Resources/Data/Magic/MagicData.json";
        // JSON Read
        jsonContent = File.ReadAllText(jsonFilePath);
        // JSON 디시리얼라이즈
        List<MagicData> magiclist = JsonConvert.DeserializeObject<List<MagicData>>(jsonContent);

        foreach (var magicData in magiclist)
        {
            magicDic.Add(magicData._nickName, magicData);
        }


        // ScrollData

        // JSON 경로
        jsonFilePath = $"Assets/Resources/Data/Scroll/ScrollData.json";
        // JSON Read
        jsonContent = File.ReadAllText(jsonFilePath);
        // JSON�� 디시리얼라이즈
        List<ScrollData> scrolllist = JsonConvert.DeserializeObject<List<ScrollData>>(jsonContent);

        foreach (var scrollData in scrolllist)
        {
            scrollDic.Add(scrollData._nickName, scrollData);
        }

        // MonsterData

        // JSON 경로 노트북
        jsonFilePath = $"Assets/Resources/Data/Monster/MonsterData.json";
        // JSON ���� �б�
        jsonContent = File.ReadAllText(jsonFilePath);
        // JSON�� ��ü�� ��ȯ

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
        {
            monsterDic.Add(monsterData._name, monsterData);
        }

        isRead = true;
    }


    public Grid currentGrid { get; private set; }
    bool[,] isCollision;
    bool[,] isItem; // ���� ���������� �� �� �ʿ��Ѱ�?
    bool[,] isMonster; // ���� ���������� �� �� �ʿ��Ѱ�?
    GameObject map;
    GameObject dummy;
    MapData mapData;
    Tilemap baseTilemap;
    Sprite[] itemSprites;
    Sprite[] monsterSprites;
    List<Animator> animators;

    public void LoadMap(string userInput)
    {
        ReadData();
        map = CreateMap(userInput);
        Init(map);
        mapData = ParsingJsonMap(userInput);
        ParsingTextMap(userInput, mapData);
    }

    private GameObject CreateMap(string userInput)
    {
        var resource = Resources.Load<GameObject>($"Prefabs/{userInput}");
        return GameObject.Instantiate(resource);
    }

    private void Init(GameObject map)
    {
        // TODO
        // �� �����͸� ���ÿ� ���� �ؾߵ�
        currentGrid = map.GetComponent<Grid>();
        GameObject collisionMap = Util.FindChild(map, "CollisionMap", true);
        GameObject itemTile = Util.FindChild(map, "Item", true);
        GameObject monsterTile = Util.FindChild(map, "Monster", true);
        baseTilemap = Util.FindChild<Tilemap>(map, "BaseMap", true);
        itemSprites = Resources.LoadAll<Sprite>("Sprite/Item/Item");
        monsterSprites = Resources.LoadAll<Sprite>("Sprite/Monster/Monster");
        animators = new List<Animator>();

        if (collisionMap)
            collisionMap.SetActive(false);

        if (itemTile)
            itemTile.SetActive(false);

        if (monsterTile)
            monsterTile.SetActive(false);
    }

    private MapData ParsingJsonMap(string userInput)
    {
        // JSON 경로
        string jsonFilePath = $"Assets/Resources/{userInput}.json";

        // JSON Read
        string jsonContent = File.ReadAllText(jsonFilePath);

        // JSON 디시리얼라이즈
        MapData mapData = JsonConvert.DeserializeObject<MapData>(jsonContent);

        // 로그
        Debug.Log($"Map Name: {mapData.name}");
        Debug.Log($"Bounds: xMin={mapData.bounds.xMin}, xMax={mapData.bounds.xMax}, yMin={mapData.bounds.yMin}, yMax={mapData.bounds.yMax}");
        Debug.Log("Monster List:");
        foreach (var monster in mapData.monsterList)
        {
            Debug.Log(monster);
        }

        return mapData;
    }

    public void ParsingTextMap(string userInput, MapData mapData)
    {
        // Txt 파일 경로
        string textFilePath = $"Assets/Resources/{userInput}.txt";

        // Read Txt
        string[] lines = File.ReadAllLines(textFilePath);

        // xMin, xMax, yMin, yMax 
        string[] boundsLine = lines[0].Split(':');
        int xMin = int.Parse(boundsLine[1].Trim());
        int xMax = int.Parse(lines[1].Split(':')[1].Trim());
        int yMin = int.Parse(lines[2].Split(':')[1].Trim());
        int yMax = int.Parse(lines[3].Split(':')[1].Trim());

        // CollisionTiles
        List<string> collisionTiles = new List<string>();
        int row = 5;
        while (row < lines.Length && lines[row] != "itemTiles")
        {
            collisionTiles.Add(lines[row]);
            row++;
        }

        // itemTiles 
        List<string> itemTiles = new List<string>();
        row++;
        while (row < lines.Length && lines[row] != "monsterTiles")
        {
            itemTiles.Add(lines[row]);
            row++;
        }

        // monsterTiles
        List<string> monsterTiles = new List<string>();
        row++;
        while (row < lines.Length)
        {
            monsterTiles.Add(lines[row]);
            row++;
        }

        // 
        Debug.Log($"Bounds: xMin={xMin}, xMax={xMax}, yMin={yMin}, yMax={yMax}");
        Debug.Log("Collision Tiles:");

        // 보정
        int xCount = xMax - xMin + 1;
        int yCount = yMax - yMin + 1;

        isCollision = new bool[yCount, xCount];

        for (int y = 0; y < yCount; y++)
        {
            string line = collisionTiles[y];
            int flippedY = yCount - 1 - y;
            for (int x = 0; x < xCount; x++)
            {
                if (line[x] == '1')
                    isCollision[flippedY, x] = true;
                else
                    isCollision[flippedY, x] = false;
            }
        }

        isItem = new bool[itemTiles.Count, xCount];
        int itemIndex = 0;
        GameObject dummyItem = Resources.Load<GameObject>("Prefabs/Item/item");

        for (int y = 0; y < itemTiles.Count; y++)
        {
            string line = itemTiles[y];
            int flippedY = yCount - 1 - y;
            for (int x = 0; x < xCount; x++)
            {
                // 좌표 읽는 순서 방향 보정
                int cellX = x + xMin;
                int cellY = flippedY + yMin;

                // 비트맵 셀 월드 변환
                Vector3 worldPos = baseTilemap.CellToWorld(new Vector3Int(cellX, cellY, 0))
                                   + baseTilemap.cellSize / 2f;
                if (line[x] == '0')
                {
                   // 해당 타일에 맞게 생성
                    GameObject item = GameObject.Instantiate(dummyItem);
                    SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = Array.Find(itemSprites, sprite => sprite.name.Equals(mapData.itemList[itemIndex]));
                    Animator animator = item.GetComponent<Animator>();
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"Animations/Item/{mapData.itemList[itemIndex]}");
                    // 데이터 입력
                    string spriteName = spriteRenderer.sprite.name;
                    switch (CheckItemType(spriteName))
                    {
                        case ItemDataType.ITEM: 
                            ItemData itemData = AddItemData(spriteName);
                            var myItemData = item.AddComponent<ItemDataComponent>();
                            CopyItemData(myItemData, itemData);
                            break;
                        case ItemDataType.POTION:
                            PotionData potionData = AddPotionData(spriteName);
                            var myPotionData = item.AddComponent<PotionDataComponent>();
                            CopyPotionData(myPotionData, potionData);
                            break;
                        case ItemDataType.SCROLL:
                            ScrollData scrollData = AddScrollData(spriteName);
                            var myScrollData= item.AddComponent<ScrollDataComponent>();
                            CopyScrollData(myScrollData, scrollData);
                            break;
                        case ItemDataType.MAGIC:
                            MagicData magicData = AddMagicData(spriteName);
                            var myMagicData = item.AddComponent<MagicDataComponent>();
                            CopyMagicData(myMagicData, magicData);
                            break;

                    }
                    //
                    item.transform.position = worldPos;
                    item.name = mapData.itemList[itemIndex];
                    item.transform.SetParent(map.transform);
                    itemIndex++;
                }
            }
        }

        isMonster = new bool[monsterTiles.Count, xCount];
        int monsterIndex = 0;
        GameObject dummyMonster = Resources.Load<GameObject>("Prefabs/Monster/monster");

        for (int y = 0; y < monsterTiles.Count; y++)
        {
            string line = monsterTiles[y];
            int flippedY = yCount - 1 - y;
            for (int x = 0; x < xCount; x++)
            {
                // 좌표 보정
                int cellX = x + xMin;
                int cellY = flippedY + yMin;

                // 월드 좌표로 변경
                Vector3 worldPos = baseTilemap.CellToWorld(new Vector3Int(cellX, cellY, 0))
                                   + baseTilemap.cellSize / 2f;
                if (line[x] == '0')
                {
                    // 생성
                    GameObject monster = GameObject.Instantiate(dummyMonster);
                    SpriteRenderer spriteRenderer = monster.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = Array.Find(monsterSprites, sprite => sprite.name.Equals(mapData.monsterList[monsterIndex]));
                    Animator animator = monster.GetComponent<Animator>();
                    string spriteName = spriteRenderer.sprite.name;
                    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"Animations/Monster/{mapData.monsterList[monsterIndex]}");
                    animators.Add(animator);

                    //
                    // 데이터 입력
                    MonsterData monsterData = AddMonsterData(spriteName);
                    var myMonsterData = monster.AddComponent<MonsterDataComponent>();
                    CopyMonsterData(myMonsterData, monsterData);
                    //
                    monster.transform.position = worldPos;
                    monster.name = $"{mapData.monsterList[monsterIndex]}{monsterIndex + 1}";
                    monster.transform.SetParent(map.transform);
                    monsterIndex++;
                }
            }
        }
    }

    // � ������ ���������� Ȯ���ϴ� �޼���

    enum ItemDataType
    {
        ITEM,
        POTION,
        SCROLL,
        MAGIC,
        NONE
    }

    ItemDataType CheckItemType(string spriteName)
    {
        if (spriteName.Contains("Potion"))
            return ItemDataType.POTION;
        else if (spriteName.Contains("scroll"))
            return ItemDataType.SCROLL;
        else if (spriteName.Contains("fire") || 
                 spriteName.Contains("cold") ||
                 spriteName.Contains("earth") ||
                 spriteName.Contains("poison") ||
                 spriteName.Contains("nec") ||
                 spriteName.Contains("sum"))
            return ItemDataType.MAGIC;
        else
            return ItemDataType.ITEM;
    }

    ItemData itemData = null;
    PotionData potionData = null;
    ScrollData scrollData = null;
    MagicData magicData = null;
    MonsterData monsterData = null;
    LanguageData languageData = null;

    ItemData AddItemData(string spriteName)
    {
        if (amuletDic.TryGetValue(spriteName, out itemData))
            itemData = amuletDic[spriteName];
        else if (armourDic.TryGetValue(spriteName, out itemData))
            itemData = armourDic[spriteName];
        else if (axeDic.TryGetValue(spriteName, out itemData))
            itemData = axeDic[spriteName];
        else if (bootsDic.TryGetValue(spriteName, out itemData))
            itemData = bootsDic[spriteName];
        else if (bowDic.TryGetValue(spriteName, out itemData))
            itemData = bowDic[spriteName];
        else if (gloveDic.TryGetValue(spriteName, out itemData))
            itemData = gloveDic[spriteName];
        else if (helmetDic.TryGetValue(spriteName, out itemData))
            itemData = helmetDic[spriteName];
        else if (maceDic.TryGetValue(spriteName, out itemData))
            itemData = maceDic[spriteName];
        else if (ringDic.TryGetValue(spriteName, out itemData))
            itemData = ringDic[spriteName];
        else if (robeDic.TryGetValue(spriteName, out itemData))
            itemData = robeDic[spriteName];
        else if (shieldDic.TryGetValue(spriteName, out itemData))
            itemData = shieldDic[spriteName];
        else if (spearDic.TryGetValue(spriteName, out itemData))
            itemData = spearDic[spriteName];
        else if (staffDic.TryGetValue(spriteName, out itemData))
            itemData = staffDic[spriteName];
        else if (swordDic.TryGetValue(spriteName, out itemData))
            itemData = swordDic[spriteName];
        else if (etcDic.TryGetValue(spriteName, out itemData))
            itemData = etcDic[spriteName];

        if (languageDic.TryGetValue(spriteName, out languageData))
        {
            itemData._name = languageDic[spriteName]._ko;
            itemData._comment = languageDic[$"{spriteName}_comment"]._ko;
        }



        return itemData;
    }

    // 나중에 제너릭으로만들것 지금은 보류
    // T AddOtherItemData<T>(string spriteName, Dictionary<string, T> dataDic) where T : class
    // {
    //     if (dataDic.TryGetValue(spriteName, out T data))
    //         data = dataDic[spriteName];
    //     
    //     if (languageDic.TryGetValue(spriteName, out languageData))
    //     {
    //         data._name = languageDic[spriteName]._ko;
    //         data._comment = languageDic[$"{spriteName}_comment"]._ko;
    //     }
    // }
    
    PotionData AddPotionData(string spriteName)
    {
        if (potionDic.TryGetValue(spriteName, out potionData))
            potionData = potionDic[spriteName];

        if (languageDic.TryGetValue(spriteName, out languageData))
        {
            potionData._name = languageDic[spriteName]._ko;
            potionData._comment = languageDic[$"{spriteName}_comment"]._ko;
        }

        return potionData;
    }

    ScrollData AddScrollData(string spriteName)
    {
        if (scrollDic.TryGetValue(spriteName, out scrollData))
            scrollData = scrollDic[spriteName];

        if (languageDic.TryGetValue(spriteName, out languageData))
        {
            scrollData._name = languageDic[spriteName]._ko;
            scrollData._comment = languageDic[$"{spriteName}_comment"]._ko;
        }

        return scrollData;
    }
    
    MagicData AddMagicData(string spriteName)
    {
        if (magicDic.TryGetValue(spriteName, out magicData))
            magicData = magicDic[spriteName];
        
        if (languageDic.TryGetValue(spriteName, out languageData))
        {
            magicData._name = languageDic[spriteName]._ko;
            magicData._comment = languageDic[$"{spriteName}_comment"]._ko;
        }
        return magicData;
    }

    MonsterData AddMonsterData(string spriteName)
    {
        if (monsterDic.TryGetValue(spriteName, out monsterData))
            monsterData = monsterDic[spriteName];

        if (languageDic.TryGetValue(spriteName, out languageData))
        {
            monsterData._name = languageDic[spriteName]._ko;
            monsterData._comment = languageDic[$"{spriteName}_comment"]._ko;
        }
        return monsterData;
    }

    void CopyItemData(ItemDataComponent target, ItemData newItemData)
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

    void CopyPotionData(PotionDataComponent target, PotionData newPotionData)
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

    void CopyScrollData(ScrollDataComponent target, ScrollData newScrollData)
    {
        target._no = newScrollData._no;
        target._name = newScrollData._name;
        target._avoid = newScrollData._avoid;
        target._enhance = newScrollData._enhance;
        target._nickName = newScrollData._nickName;
        target._comment = newScrollData._comment;
    }

    void CopyMagicData(MagicDataComponent target, MagicData newMagicData)
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

    void CopyMonsterData(MonsterDataComponent target, MonsterData newMonsterData)
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
   


    public void DestroyMap()
    {
        GameObject findMap = GameObject.Find(map.name);
        if (findMap != null)
        {
            GameObject.Destroy(findMap);
            currentGrid = null;
        }
    }

    int StateNum = 1;
    public void NextMobAnim()
    {
        foreach (var one in animators)
        {
            one.SetInteger("State", StateNum);
        }

        if (StateNum < 3)
        {
            StateNum++;
        }
        else
        {
            StateNum = 0;
        }
    }

    // ĳ�� ����
    //Sprite GetItemSprite(string name)
    //{
    //    if (!spriteCache.TryGetValue(name, out var sprite))
    //    {
    //        sprite = Resources.Load<Sprite>($"Sprite/Item/{name}");
    //        if (sprite == null)
    //        {
    //            Debug.LogWarning($"[��������Ʈ ����] Sprite/Item/{name}");
    //        }
    //        spriteCache[name] = sprite;
    //    }
    //    return sprite;
    //}
}
