using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인벤에서 가지고 있을 아이템 데이터
public class InvenItemData
{
    public ItemData _itemData { get => itemData; set => itemData = value; } 
    [SerializeField] private ItemData itemData;
    public int _itemCount { get => itemCount; set => itemCount = value; }
    [SerializeField] private int itemCount;
    public ItemDataComponent _itemDataComponent { get => itemDataComponent; set => itemDataComponent = value; }
    [SerializeField] private ItemDataComponent itemDataComponent;
}
