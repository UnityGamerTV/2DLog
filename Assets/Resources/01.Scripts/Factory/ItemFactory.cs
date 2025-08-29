using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEditor.Progress;

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
