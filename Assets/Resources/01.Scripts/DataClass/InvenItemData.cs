using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인벤에서 가지고 있을 아이템 데이터
public class InvenItemData
{
    public ItemData _itemData { get { return itemData; } set { itemData = value; } }
    [SerializeField] private ItemData itemData;

    public int _itemCount { get { return itemCount; } set { itemCount = value; } }
    [SerializeField] private int itemCount;
}
