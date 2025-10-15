using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_ItemController : UI_Popup
{
    [Singleton(typeof(UIManager))] private UIManager uiManager;

    [FindComponents("OKButton"), SerializeField] private Button okButton;
    [FindComponents("CancelButton"), SerializeField] private Button cancelButton;
    [FindComponents("CloseButton"), SerializeField] private Button closeButton;
    [FindComponents("CommentToggle"), SerializeField] private Toggle commentToggle;
    [FindComponents("InfoToggle"), SerializeField] private Toggle infoToggle;
    [FindComponents("Service"), SerializeField] private UI_Popup_ItemService service;

    public override void Init()
    {
        base.Init();

        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        service.Init();

        closeButton.onClick.AddListener(Hide);
    }

    public override void Open()
    {
        base.Open();

    }

    public void Hide()
    {
        uiManager.ClosePopupUI(UI_POPUP_ENUM.UI_Popup_Item);
    }

    public override void Close()
    {
        base.Close();
    }

    public void SetItemData(ItemDataComponent ItemDataComponent) => service.SetItemData(ItemDataComponent);

    public override void Release()
    {
        base.Release();
        closeButton.onClick.RemoveListener(Hide);
    }
}
