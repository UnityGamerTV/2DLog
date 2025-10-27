using UnityEngine;

public class UI_Popup_ItemService : MonoBehaviour
{
    [FindComponents("View"), SerializeField] private UI_Popup_ItemView view;
    [FindComponents("Model"), SerializeField] private UI_Popup_ItemModel model;

    [Singleton(typeof(EventManager))] private EventManager eventManager;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        model.Init();
        view.Init();
    }

    public void SetItemData(ItemDataComponent itemDataComponent) => model.SetItemData(itemDataComponent);

    public void OnClickApply() 
        => eventManager.PostNotification(EVENT_ITEM_POPUP_UI.ON_CLICK_APPLY, this, model._itemDataComponent);

    public void OnClickDismiss() => model.OnClickDismiss();

    public void OnDetail(bool onDetail) => view.OnDetail(onDetail);
    public void Release()
    {
        model.Release();
        view.Release();
    }
}
