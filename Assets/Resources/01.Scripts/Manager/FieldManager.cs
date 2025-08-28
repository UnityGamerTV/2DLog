using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 필드 오브젝트 생성, 데이터 주입, 관리
/// </summary>
public class FieldManager : Singleton<FieldManager>, IManager
{
    [Singleton(typeof(ResourceManager))] public ResourceManager resourceManager;
    [Singleton(typeof(DataManager))] public DataManager dataManager;

#if UNITY_EDITOR
    [Singleton(typeof(TestManager))] private TestManager testManager;
#endif

    public PlayerController _playerController { get { return playerController; } set { playerController = value; } }
    [SerializeField] private PlayerController playerController;
    public List<MonsterController> _monsterControllerList { get { return monsterControllerList; } set { monsterControllerList = value; } }
    [SerializeField] private List<MonsterController> monsterControllerList;
    public List<ItemController> _ItemControllerList { get { return itemControllerList; } set { itemControllerList = value; } }
    [SerializeField] private List<ItemController> itemControllerList;

    // String 사용을 줄이기 위한 캐싱용
    private Dictionary<string, string> itemPathDic;
    private Dictionary<string, string> monsterPathDic;

    private Sprite[] itemSprites;
    private Sprite[] monsterSprites;

    private readonly string ITEM_SPRITE_PATH = "Sprite/Item/Item";
    private readonly string MONSTER_SPRITE_PATH = "Sprite/Monster/Monster";
    private readonly string PLAYER_PATH = "Prefabs/Player/Player";
    private readonly string ITEM_PATH = "Prefabs/Item/item";
    private readonly string MONSTER_PATH = "Prefabs/Monster/monster";

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        monsterControllerList = new();
        itemControllerList = new();
        itemPathDic = new();
        monsterPathDic = new();

        itemSprites = resourceManager.LoadAll<Sprite>(ITEM_SPRITE_PATH);
        monsterSprites = resourceManager.LoadAll<Sprite>(MONSTER_SPRITE_PATH);
        SetItemPathDic();
        SetMonsterPathDic();
    }

    private void SetItemPathDic()
    {
        string CONTROLLER_PATH = "Animations/Item/Controller";
        var controllers = resourceManager.LoadAll<RuntimeAnimatorController>(CONTROLLER_PATH);

        foreach (var item in controllers)
        {
            itemPathDic.Add(item.name, $"{CONTROLLER_PATH}/{item.name}");
        }
    }
    private string GetItemPathDic(string spriteName) => itemPathDic[spriteName];

    private void SetMonsterPathDic()
    {
        string CONTROLLER_PATH = "Animations/Monster/Controller";
        var controllers = resourceManager.LoadAll<RuntimeAnimatorController>(CONTROLLER_PATH);

        foreach (var monster in controllers)
        {
            monsterPathDic.Add(monster.name, $"{CONTROLLER_PATH}/{monster.name}");
        }
    }

    private string GetMonsterPathDic(string spriteName) => monsterPathDic[spriteName];


    public void CreatePlayer(Vector3 worldPos, GameObject map)
    {
        var player = resourceManager.Instantiate(PLAYER_PATH);
        var controller = player.GetComponent<PlayerController>();
        controller.Init();
        player.transform.position = worldPos;
        player.transform.SetParent(map.transform);
        playerController = controller;

#if UNITY_EDITOR
        testManager._playerController = playerController;
#endif
    }

    public void CreateItem(Vector3 worldPos, GameObject map, MapData mapData, int itemIndex)
    {
        var item = resourceManager.Instantiate(ITEM_PATH);
        var spriteRenderer = item.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Array.Find(itemSprites, sprite => sprite.name.Equals(mapData.itemList[itemIndex]));
        var animator = item.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(GetItemPathDic(mapData.itemList[itemIndex]));
        var itemController = item.GetComponent<ItemController>();
        itemControllerList.Add(itemController);

        // 데이터 입력
        string spriteName = spriteRenderer.sprite.name;
        ItemDataType itemDataType = CheckItemType(spriteName);
        switch (itemDataType)
        {
            case ItemDataType.ITEM:
                ItemData itemData = dataManager.AddItemData(spriteName);
                var myItemData = item.AddComponent<ItemDataComponent>();
                dataManager.CopyItemData(myItemData, itemData);
                itemController._baseDataComponent = myItemData;
                break;
            case ItemDataType.POTION:
                PotionData potionData = dataManager.AddPotionData(spriteName);
                var myPotionData = item.AddComponent<PotionDataComponent>();
                dataManager.CopyPotionData(myPotionData, potionData);
                itemController._baseDataComponent = myPotionData;
                break;
            case ItemDataType.SCROLL:
                ScrollData scrollData = dataManager.AddScrollData(spriteName);
                var myScrollData = item.AddComponent<ScrollDataComponent>();
                dataManager.CopyScrollData(myScrollData, scrollData);
                itemController._baseDataComponent = myScrollData;
                break;
            case ItemDataType.MAGIC:
                MagicData magicData = dataManager.AddMagicData(spriteName);
                var myMagicData = item.AddComponent<MagicDataComponent>();
                dataManager.CopyMagicData(myMagicData, magicData);
                itemController._baseDataComponent = myMagicData;
                break;

        }
        //
        itemController._itemDataType = itemDataType;
        //
        item.transform.position = worldPos;
        item.name = mapData.itemList[itemIndex];
        item.transform.SetParent(map.transform);
    }

    public GameObject CreateItemObj(Vector3 worldPos, GameObject map, MapData mapData, int itemIndex)
    {
        var item = resourceManager.Instantiate(ITEM_PATH);
        var spriteRenderer = item.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Array.Find(itemSprites, sprite => sprite.name.Equals(mapData.itemList[itemIndex]));
        var animator = item.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(GetItemPathDic(mapData.itemList[itemIndex]));
        var itemController = item.GetComponent<ItemController>();
        itemControllerList.Add(itemController);

        return item;
    }

    public void CreateMonster(Vector3 worldPos, GameObject map, MapData mapData, int monsterIndex)
    {
        // 생성
        var monster = resourceManager.Instantiate(MONSTER_PATH);
        SpriteRenderer spriteRenderer = monster.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Array.Find(monsterSprites, sprite => sprite.name.Equals(mapData.monsterList[monsterIndex]));
        Animator animator = monster.GetComponent<Animator>();
        string spriteName = spriteRenderer.sprite.name;
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(GetMonsterPathDic(mapData.monsterList[monsterIndex]));
#if UNITY_EDITOR
        testManager._animators.Add(animator);
#endif

        // 데이터 입력
        MonsterData monsterData = dataManager.AddMonsterData(spriteName);
        var myMonsterData = monster.AddComponent<MonsterDataComponent>();
        dataManager.CopyMonsterData(myMonsterData, monsterData);
        //
        monster.transform.position = worldPos;
        monster.name = $"{mapData.monsterList[monsterIndex]}{monsterIndex + 1}";
        monster.transform.SetParent(map.transform);
        monsterIndex++;
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


    public void Release()
    {
        
    }
}
