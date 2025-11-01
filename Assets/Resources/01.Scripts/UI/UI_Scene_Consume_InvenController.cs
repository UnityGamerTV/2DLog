using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Consume_InvenController : UI_Scene, IListener
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [FindComponents("Service"), SerializeField] private UI_Scene_Consume_InvenService service;
    [FindComponents("ReadingGlassButton"), SerializeField] private UnityEngine.UI.Button button;

    public override void Init()
    {
        base.Init();

        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        button.onClick.AddListener(OnClickReadingGlassButton);

        service.Init();

        eventManager.AddListener(EVENT_ITEM_POPUP_UI.REQUEST_READING_GLASS_DATA, this);
        eventManager.AddListener(EVENT_CONSUME_INVEN_UI.RESPONDED_CONSUME_INVENTORY_DATA, this);
        eventManager.PostNotification(EVENT_CONSUME_INVEN_UI.REQUEST_CONSUME_INVENTORY_DATA, this); // 소모품 인벤토리 데이터 요청 이벤트
    }

    public override void Open()
    {
        base.Open();
        eventManager.PostNotification(EVENT_CONSUME_INVEN_UI.REQUEST_CONSUME_INVENTORY_DATA, this); // 소모품 인벤토리 데이터 요청 이벤트
    }

    public void OnClickReadingGlassButton() => service.OnClickReadingGlassButton();

    public void OnEvent<TEnum>(TEnum eventType, Component sender, object param) where TEnum : Enum
    {
        switch (eventType)
        {
            case EVENT_CONSUME_INVEN_UI.RESPONDED_CONSUME_INVENTORY_DATA: service.UpdateData((Dictionary<ConsumeType, InvenItemData>)param); break;
            case EVENT_ITEM_POPUP_UI.REQUEST_READING_GLASS_DATA: service.ResponseGlassData(); break;
        }
    }

    public override void Close()
    {
        base.Close();
    }

    public override void Release()
    {
        base.Release();

        service.Release();

        button.onClick.RemoveListener(OnClickReadingGlassButton);
        eventManager.RemoveListener(EVENT_ITEM_POPUP_UI.REQUEST_READING_GLASS_DATA, this);
        eventManager.RemoveListener(EVENT_CONSUME_INVEN_UI.RESPONDED_CONSUME_INVENTORY_DATA, this);
    }
}
