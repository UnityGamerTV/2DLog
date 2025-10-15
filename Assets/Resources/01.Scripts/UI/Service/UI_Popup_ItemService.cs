using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_ItemService : MonoBehaviour
{
    [FindComponents("View"), SerializeField] private UI_Popup_ItemView view;
    [FindComponents("Model"), SerializeField] private UI_Popup_ItemModel model;

    [Singleton(typeof(UIManager))] private UIManager uiManager;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);
    }

    public void SetItemData(ItemDataComponent itemDataComponent) => model.SetItemData(itemDataComponent);

    public void Release()
    {

    }
}
