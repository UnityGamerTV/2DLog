using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_StatusController : UI_Popup, IListener
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [Singleton(typeof(UIManager))] private UIManager uiManager;
    [FindComponents("CloseButton"), SerializeField] private Button closeButton;
    [FindComponents("Service"), SerializeField] private UI_Popup_StatusService service;

    public override void Init()
    {
        base.Init();

        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);
        
        service.Init();

        closeButton.onClick.AddListener(Hide);
        eventManager.AddListener(EVENT_STATUS_POPUP_UI.RESPONDED_EQUIP_DATA, this);
        Open();
    }

    public override void Open()
    {
        eventManager.PostNotification(EVENT_STATUS_POPUP_UI.REQUEST_EQUIP_DATA, this);
    }

    public void Hide()
    {
        uiManager.ClosePopupUI(UI_POPUP_ENUM.UI_Popup_Status);
    }

    public void UpdateEquip(List<ItemDataComponent> itemDataComponents) => service.UpdateEquip(itemDataComponents);


    public void OnEvent<TEnum>(TEnum eventType, Component sender, object param) where TEnum : Enum
    {
        switch (eventType)
        {
            case EVENT_STATUS_POPUP_UI.RESPONDED_EQUIP_DATA: UpdateEquip((List<ItemDataComponent>)param); break;
        }
    }

    public override void Release()
    {
        base.Release();

        closeButton.onClick.RemoveListener(Hide);
        eventManager.PostNotification(EVENT_STATUS_POPUP_UI.REQUEST_EQUIP_DATA, this);
    }

}
