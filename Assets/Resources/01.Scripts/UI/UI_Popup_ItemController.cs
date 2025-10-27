using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_ItemController : UI_Popup, IListener
{
    [Singleton(typeof(UIManager))] private UIManager uiManager;
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [FindComponents("OKButton"), SerializeField] private UnityEngine.UI.Button okButton;
    [FindComponents("CancelButton"), SerializeField] private UnityEngine.UI.Button cancelButton;
    [FindComponents("CloseButton"), SerializeField] private UnityEngine.UI.Button closeButton;
    [FindComponents("Service"), SerializeField] private UI_Popup_ItemService service;

    public override void Init()
    {
        base.Init();

        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);
        service.Init();

        okButton.onClick.AddListener(OnClickApply);
        cancelButton.onClick.AddListener(OnClickDismiss);
        closeButton.onClick.AddListener(Hide);
        eventManager.AddListener(EVENT_ITEM_POPUP_UI.RESPONDED_READING_GLASS_DATA, this);
        eventManager.AddListener(EVENT_EQUIP_INVEN_UI.ON_CLICK_READING_GLASS, this);
        eventManager.AddListener(EVENT_ITEM_POPUP_UI.ON_CLICK_DISMISS, this);
        eventManager.PostNotification(EVENT_ITEM_POPUP_UI.REQUEST_READING_GLASS_DATA, this);
    }

    public override void Open() // 열 때 자동 호출
    {
        base.Open();
        eventManager.PostNotification(EVENT_ITEM_POPUP_UI.REQUEST_READING_GLASS_DATA, this);
    }

    public void Hide() => uiManager.ClosePopupUI(UI_POPUP_ENUM.UI_Popup_Item);
    
    public override void Close() // 닫을 때 자동 호출
    {
        base.Close();
    }

    public void SetItemData(ItemDataComponent ItemDataComponent) => service.SetItemData(ItemDataComponent);

    private void OnClickApply() => service.OnClickApply();

    private void OnClickDismiss() => service.OnClickDismiss();

    public void OnEvent<TEnum>(TEnum eventType, Component sender, object param) where TEnum : Enum
    {
        switch(eventType)
        {
            case EVENT_EQUIP_INVEN_UI.ON_CLICK_READING_GLASS: service.OnDetail((bool)param); break;
            case EVENT_ITEM_POPUP_UI.RESPONDED_READING_GLASS_DATA: service.OnDetail((bool)param); break;
            case EVENT_ITEM_POPUP_UI.ON_CLICK_DISMISS: Hide(); break;
        }
    }

    public override void Release()
    {
        base.Release();

        service.Release();

        closeButton.onClick.RemoveListener(Hide);
        cancelButton.onClick.RemoveListener(OnClickDismiss);
        eventManager.RemoveListener(EVENT_ITEM_POPUP_UI.REQUEST_READING_GLASS_DATA, this);
        eventManager.RemoveListener(EVENT_ITEM_POPUP_UI.RESPONDED_READING_GLASS_DATA, this);
        eventManager.RemoveListener(EVENT_ITEM_POPUP_UI.ON_CLICK_DISMISS, this);
        eventManager.RemoveListener(EVENT_EQUIP_INVEN_UI.ON_CLICK_READING_GLASS, this);
    }
}
