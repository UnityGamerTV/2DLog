using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Equip_Inven_Slot : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [Singleton(typeof(UIManager))] private UIManager uiManager;

    [FindComponents("Model"), SerializeField] private UI_Scene_Equip_InvenModel model;
    [FindComponents(true, "SlotButton"), SerializeField] private Button button;
    [FindComponents(true, "SlotButton"), SerializeField] private Image slotBG;
    [FindComponents(true, "ItemImage"), SerializeField] private Image itemImage;
    [FindComponents(true, "BlackImage"), SerializeField] private Image blackImage;
    [FindComponents(true, "EnhanceText"), SerializeField] private Text enhanceText;

    private ItemData itemData;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
        InjectUtil.InjectComponents(this);

        button.onClick.AddListener(OnButtonClick);
    }

    public void OnDetail()
    {
        enhanceText.gameObject.SetActive(true);
        blackImage.gameObject.SetActive(true);
    }

    public void OffDetail()
    {
        enhanceText.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(false);
    }

    public void SetItemData() { }

    public void GetItemData() { }

    public void SetSlotImage(Sprite item, Sprite slotBG)
    {
        itemImage.sprite = item;
        this.slotBG.sprite = slotBG;
    }

    public void SetSlotData(ItemData data)
    {
        enhanceText.text = $"+{data._current_enhance.ToString()}";
    }

    public void OnButtonClick()
    {
        // 아이템 디테일 열기
        var popup = uiManager.ShowPopupUI<UI_Popup_ItemController>(UI_POPUP_ENUM.UI_Popup_Item);
        popup.SetItemData(itemData);
    }

    public void Release()
    {

    }
}
