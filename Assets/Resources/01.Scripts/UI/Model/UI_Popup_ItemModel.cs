using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UI_Popup_ItemModel : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    public ItemDataComponent _itemDataComponent { get => itemDataComponent; set { itemDataComponent = value; updateAction.Invoke(itemDataComponent); } }
    [SerializeField] private ItemDataComponent itemDataComponent;
    [SerializeField] private ItemData itemData;

    public Action<ItemDataComponent> updateAction;
    public void Init()
    {
        //InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        itemData = new();
        itemDataComponent = gameObject.AddComponent<ItemDataComponent>();
        itemDataComponent._data = itemData;
    }

    public void SetItemData(ItemDataComponent itemDataComponent) => _itemDataComponent = itemDataComponent;

    public void OnClickDismiss()
    {
        eventManager.PostNotification(EVENT_ITEM_POPUP_UI.ON_CLICK_DISMISS, this, _itemDataComponent);
    }

    public void Release()
    {

    }
}
