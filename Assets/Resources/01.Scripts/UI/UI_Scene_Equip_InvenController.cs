using System;
using UnityEngine;

public class UI_Scene_Equip_InvenController : UI_Scene, IListener
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [FindComponents("Service"), SerializeField] private UI_Scene_Equip_InvenService service;
    [FindComponents("ReadingGlassButton"), SerializeField] private UnityEngine.UI.Button button;

    public override void Init()
    {
        base.Init();

        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        button.onClick.AddListener(OnClickReadingGlassButton);

        service.Init();

        eventManager.PostNotification(EVENT_EQUIP_INVEN_UI.REQUEST_EQUIP_INVENTORY_DATA, this); // 장비 인벤 데이터 요청 이벤트
        eventManager.AddListener(EVENT_EQUIP_INVEN_UI.RESPONDED_EQUIP_INVENTORY_DATA, this);
    }

    public override void Open()
    {
        base.Open();
        eventManager.PostNotification(EVENT_EQUIP_INVEN_UI.REQUEST_EQUIP_INVENTORY_DATA, this); // 장비 인벤 데이터 요청 이벤트
    }

    public void OnClickReadingGlassButton() => service.OnClickReadingGlassButton();

    public void OnEvent<TEnum>(TEnum eventType, Component sender, object param) where TEnum : Enum
    {
        switch (eventType)
        {
            case EVENT_EQUIP_INVEN_UI.RESPONDED_EQUIP_INVENTORY_DATA: service.UpdateData(param); break;
        }
    }

    public override void Close()
    {
        base.Close();
    }

    public override void Release()
    {
        base.Release();

        button.onClick.RemoveListener(OnClickReadingGlassButton);

        service.Release();
    }
}
