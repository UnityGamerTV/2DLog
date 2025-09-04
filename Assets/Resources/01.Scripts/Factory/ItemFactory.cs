using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : FactoryBase
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(FieldManager))] private FieldManager fieldManager;
    [Singleton(typeof(DataManager))] private DataManager dataManager;
    [Singleton(typeof(ObjectPoolManager))] private ObjectPoolManager objectPoolManager;

    private readonly string ITEM_PATH = "Prefabs/Item/item";
    private readonly string ITEM_SPRITE_PATH = "Sprite/Item/Item";

    private Sprite[] itemSprites;

    // String 사용을 줄이기 위한 캐싱용
    private Dictionary<string, string> itemPathDic;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        itemPathDic = new();

        itemSprites = resourceManager.LoadAll<Sprite>(ITEM_SPRITE_PATH);
        SetItemPathDic();
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

    public FieldObjBase CreateObj(Vector3 worldPos, GameObject map, MapData mapData, int itemIndex)
    {
        // 생성
        var item = GetItem();
        // 기본 설정
        SetPosition(item, worldPos);
        SetName(item, mapData, itemIndex);
        SetParent(item, map);
        SetAnimator(item, mapData, itemIndex);
        var spriteName = SetSprite(item, mapData, itemIndex);
        // 데이터 입력
        var itemController = SetData(item, spriteName);
        return itemController;
    }

    private GameObject GetItem()
    {
        GameObject obj;
        // 오브젝트 풀링 확인
        obj = objectPoolManager.GetObjPool("item");
        if (obj == null)
            obj = resourceManager.Instantiate(ITEM_PATH);

        return obj;
    }

    private void SetPosition(GameObject item, Vector3 worldPos) => item.transform.position = worldPos;
    private void SetName(GameObject item, MapData mapData, int itemIndex) => item.name = mapData.itemList[itemIndex];
    private void SetParent(GameObject item, GameObject map) => item.transform.SetParent(map.transform);

    private void SetAnimator(GameObject item, MapData mapData, int itemIndex)
    {
        var animator = item.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(GetItemPathDic(mapData.itemList[itemIndex]));
    }

    private string SetSprite(GameObject item, MapData mapData, int itemIndex)
    {
        var spriteRenderer = item.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Array.Find(itemSprites, sprite => sprite.name.Equals(mapData.itemList[itemIndex]));
        return spriteRenderer.sprite.name;
    }

    private ItemController SetData(GameObject item, string spriteName)
    {
        var itemController = item.GetComponent<ItemController>();
        // 데이터 입력
        ItemDataType itemDataType = CheckItemType(spriteName);
        switch (itemDataType)
        {
            case ItemDataType.ITEM: SetItemData(itemController, item, spriteName);break;
            case ItemDataType.POTION: SetPotionData(itemController, item, spriteName); break;
            case ItemDataType.SCROLL: SetScrollData(itemController, item, spriteName); break;
            case ItemDataType.MAGIC: SetMagicData(itemController, item, spriteName); break;
        }
        itemController._itemDataType = itemDataType;
        return itemController;
    }

    private ItemDataType CheckItemType(string spriteName)
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

    private void SetItemData(ItemController itemController, GameObject item, string spriteName)
    {
        ItemData itemData = dataManager.AddItemData(spriteName);
        var myItemData = item.AddComponent<ItemDataComponent>();
        dataManager.CopyItemData(myItemData, itemData);
        itemController._baseDataComponent = myItemData;

        // 랜다트 데이터 확인
        RandartData randartData = (CheckRandart(spriteName));
        if (randartData != null)
            dataManager.CopyRandartData(myItemData, randartData);
    }

    private RandartData CheckRandart(string spriteName)
    {

        // 일단 랜다트를 전부 찾아보자
        // 옵션 1개 랜다트 (기본 아이템)
        string[] option1 = { "amulet1", "amulet2", "amulet3", "amulet4", "amulet5", "amulet6", "ring1", 
            "ring2", "ring3", "ring6",
        };
        // 옵션 2개 랜다트 (레어 아이템)
        string[] option2 = { "amulet7", "amulet8", "amulet9", "leader armour2", "ringmail2", 
            "scalemail2", "chainmail3", "crystalplatemail2", "troll leader armour2", "axe7","axe9", 
            "bow3", "helmet3", "helmet10", "helmet15", "mace4", "mace8", "ring5", "ring7","ring9", "ring10",
            "robe3", "spear5", "spear6", "spear8", "sword3", "sword6", "sword12", "sword16"
        };

        for (int i = 0; i < option1.Length; i++) {
            if (string.Equals(spriteName, option1[i]))
            {
                RandartData randartData = null;
                // 랜다트 옵션 1개 넘김
                RandartOptionTable table = dataManager.GetRandartTableOption(1);
                randartData = dataManager.AddRandartData(UnityEngine.Random.Range(table._startNum, table._endNum + 1));
                return randartData;
            }
        }

        for (int i = 0; i < option2.Length; i++)
        {
            if (string.Equals(spriteName, option2[i]))
            {
                RandartData randartData1 = null;
                RandartData randartData2 = null;
                RandartData randartData3 = null;
                // 랜다트 옵션 2개 넘김
                RandartOptionTable table = dataManager.GetRandartTableOption(1);
                randartData1 = dataManager.AddRandartData(UnityEngine.Random.Range(table._startNum, table._endNum + 1));
                RandartOptionTable table2 = dataManager.GetRandartTableOption(2);
                randartData2 = dataManager.AddRandartData(UnityEngine.Random.Range(table2._startNum, table2._endNum + 1));
                randartData3 = dataManager.SumRandartData(randartData1, randartData2);
                return randartData3;
            }
        }
        return null;
    }

    private void SetPotionData(ItemController itemController, GameObject item, string spriteName)
    {
        PotionData potionData = dataManager.AddPotionData(spriteName);
        var myPotionData = item.AddComponent<PotionDataComponent>();
        dataManager.CopyPotionData(myPotionData, potionData);
        itemController._baseDataComponent = myPotionData;
    }

    private void SetScrollData(ItemController itemController, GameObject item, string spriteName)
    {
        ScrollData scrollData = dataManager.AddScrollData(spriteName);
        var myScrollData = item.AddComponent<ScrollDataComponent>();
        dataManager.CopyScrollData(myScrollData, scrollData);
        itemController._baseDataComponent = myScrollData;
    }

    private void SetMagicData(ItemController itemController, GameObject item, string spriteName)
    {
        MagicData magicData = dataManager.AddMagicData(spriteName);
        var myMagicData = item.AddComponent<MagicDataComponent>();
        dataManager.CopyMagicData(myMagicData, magicData);
        itemController._baseDataComponent = myMagicData;
    }
}
