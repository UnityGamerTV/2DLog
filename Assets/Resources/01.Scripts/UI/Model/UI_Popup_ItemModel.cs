using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_Popup_ItemModel : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    public ItemDataComponent _itemDataComponent { get => itemDataComponent; set { itemDataComponent = value; updateAction.Invoke(itemDataComponent); } }
    [SerializeField] private ItemDataComponent itemDataComponent;

    public Action<ItemDataComponent> updateAction;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);
    }

    public void SetItemData(ItemDataComponent itemDataComponent)
    {
        this._itemDataComponent = itemDataComponent;
    }

    public void Release()
    {

    }
}
