using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인벤에서 가지고 있을 아이템 데이터
public class InvenItemData
{
    public ItemData _baseDataComponent { get { return baseDataComponent; } set { baseDataComponent = value; } }
    [SerializeField] private ItemData baseDataComponent;

    public ItemDataType _itemDataType { get { return itemDataType; } set { itemDataType = value; } }
    [SerializeField] private ItemDataType itemDataType;

    public int _itemCount { get { return itemCount; } set { itemCount = value; } }
    [SerializeField] private int itemCount;
}
