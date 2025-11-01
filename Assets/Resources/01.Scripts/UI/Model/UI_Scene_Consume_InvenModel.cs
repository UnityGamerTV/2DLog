using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Consume_InvenModel : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    public bool _onDetail
    {
        get => onDetail;
        set
        {
            onDetail = value;
            eventManager.PostNotification(EVENT_CONSUME_INVEN_UI.ON_CLICK_READING_GLASS, this, onDetail);
        }
    }
    [SerializeField] private bool onDetail;

    public Dictionary<ConsumeType, InvenItemData> _consumeInven
    {
        get => consumeInven;
        set
        {
            consumeInven = value;
            updateAction.Invoke(consumeInven);
        }
    }
    [SerializeField] private Dictionary<ConsumeType, InvenItemData> consumeInven;

    public Action<Dictionary<ConsumeType, InvenItemData>> updateAction;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        consumeInven = new();
    }

    public void UpdateData(Dictionary<ConsumeType, InvenItemData> data) => _consumeInven = data;

    public void ResponseGlassData() => eventManager.PostNotification(EVENT_ITEM_POPUP_UI.RESPONDED_READING_GLASS_DATA, this, _onDetail);

    public void Release()
    {

    }
}
