using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemDataType
{
    ITEM,
    POTION,
    SCROLL,
    MAGIC,
    NONE
}


public class ItemController : FieldObjBase, IController
{

    public ItemDataType _itemDataType { get { return itemDataType; } set { itemDataType = value; } }
    [SerializeField] private ItemDataType itemDataType;

    public BaseDataComponent _baseDataComponent { get { return baseDataComponent; } set { baseDataComponent = value; } }
    [SerializeField] private BaseDataComponent baseDataComponent;

    public void Init()
    {
        
    }


    public void Release()
    {
        
    }
}
