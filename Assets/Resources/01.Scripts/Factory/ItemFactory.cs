using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : FactoryBase
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(FieldManager))] private FieldManager fieldManager;
    [Singleton(typeof(DataManager))] private DataManager dataManager;

    private readonly string ITEM_PATH = "Prefabs/Item/item";
    private readonly string ITEM_SPRITE_PATH = "Sprite/Item/Item";

    private Sprite[] itemSprites;

    // String 사용을 줄이기 위한 캐싱용
    private Dictionary<string, string> itemPathDic;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        itemSprites = resourceManager.LoadAll<Sprite>(ITEM_SPRITE_PATH);
        SetItemPathDic();
    }

    public FieldObjBase CreateObj(Vector3 worldPos, GameObject map, MapData mapData, int itemIndex)
    {
        // 생성
        var item = resourceManager.Instantiate(ITEM_PATH);
        var spriteRenderer = item.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Array.Find(itemSprites, sprite => sprite.name.Equals(mapData.itemList[itemIndex]));
        var animator = item.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(GetItemPathDic(mapData.itemList[itemIndex]));
        var itemController = item.GetComponent<ItemController>();

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
}
