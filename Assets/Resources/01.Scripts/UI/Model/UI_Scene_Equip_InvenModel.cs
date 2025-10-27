using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Equip_InvenModel : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    public bool _onDetail 
    { 
        get => onDetail;  
        set 
        { 
            onDetail = value; 
            eventManager.PostNotification(EVENT_EQUIP_INVEN_UI.ON_CLICK_READING_GLASS, this, onDetail); 
        } 
    }
    [SerializeField] private bool onDetail;

    public List<ItemDataComponent> _equipmentInven 
    { 
        get => equipmentInven;  
        set 
        { 
            equipmentInven = value; 
            updateAction.Invoke(equipmentInven); 
        } 
    }
    [SerializeField] private List<ItemDataComponent> equipmentInven;

    public Action<List<ItemDataComponent>> updateAction;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        equipmentInven = new();
    }

    public void UpdateData(List<ItemDataComponent> data) => _equipmentInven = data;

    public void ResponseGlassData() => eventManager.PostNotification(EVENT_ITEM_POPUP_UI.RESPONDED_READING_GLASS_DATA, this, _onDetail);

    public void Release()
    {

    }
}
