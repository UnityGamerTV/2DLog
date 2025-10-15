using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Equip_Inven_Slot : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [Singleton(typeof(UIManager))] private UIManager uiManager;
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;

    [FindComponents("Model"), SerializeField] private UI_Scene_Equip_InvenModel model;
    [FindComponents(true, "SlotButton"), SerializeField] private Button button;
    [FindComponents(true, "SlotButton"), SerializeField] private Image slotBG;
    [FindComponents(true, "ItemImage"), SerializeField] private Image itemImage;
    [FindComponents(true, "BlackImage"), SerializeField] private Image blackImage;
    [FindComponents(true, "EnhanceText"), SerializeField] private Text enhanceText;

    public ItemDataComponent _itemDataComponent { get { return itemDataComponent; } set { itemDataComponent = value; SetData(itemDataComponent); } }
    [SerializeField] private ItemDataComponent itemDataComponent;

    private readonly string emptySlotPath = "Sprite/Inven/Inven_EmptySlot";
    private readonly string normalSlotPath = "Sprite/Inven/Inven_NormalSlot";
    private readonly string randartSlotPath = "Sprite/Inven/Inven_RandartSlot";
    private readonly string fixdartSlotPath = "Sprite/Inven/Inven_FixdartSlot";
    private string itemPath = "Sprite/Item/Item";
    private Sprite[] itemSprites;
    private Sprite emptySlotSprite;
    private Sprite normalSlotSprite;
    private Sprite randartSlotSprite;
    private Sprite fixdartSlotSprite;
    private Sprite itemSprite;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);

        button.onClick.AddListener(OnButtonClick);

        emptySlotSprite = resourceManager.Load<Sprite>(emptySlotPath);
        normalSlotSprite = resourceManager.Load<Sprite>(normalSlotPath);
        randartSlotSprite = resourceManager.Load<Sprite>(randartSlotPath);
        fixdartSlotSprite = resourceManager.Load<Sprite>(fixdartSlotPath);
        itemSprites = resourceManager.LoadAll<Sprite>($"{itemPath}");

        slotBG.sprite = emptySlotSprite;
        itemImage.gameObject.SetActive(false);
        enhanceText.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(false);
    }

    public void OnDetail()
    {
        if (_itemDataComponent == null)
            return;

        enhanceText.gameObject.SetActive(true);
        blackImage.gameObject.SetActive(true);
    }

    public void OffDetail()
    {
        enhanceText.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(false);
    }

    public void SetData(ItemDataComponent component) 
    {
        var enhance = component.data._current_enhance.ToString();
        var name = component.data._nickname;
        var grade = component.data._item_grade;

        if (Enum.TryParse<EquipmentType>(name, out var equipmentType))
        {
            enhanceText.text = $"+{enhance}";
            itemSprite = Array.Find(itemSprites, s => s.name.Equals(name));
            itemImage.sprite = itemSprite;
            itemImage.gameObject.SetActive(true);

            switch (grade)
            {
                case ItemGrade.NORMAL: slotBG.sprite = normalSlotSprite; break;
                case ItemGrade.RANDART: slotBG.sprite = randartSlotSprite; break; 
                case ItemGrade.FIXDART: slotBG.sprite = fixdartSlotSprite; break;
            }
        }
        else
        {
            enhanceText.text = string.Empty;
            itemImage.sprite = null;
            slotBG.sprite = emptySlotSprite;
        }
    }

    public void RemoveData()
    {

    }

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
        var popup = uiManager.ShowPopupUI<UI_Popup_ItemController>(UI_POPUP_ENUM.UI_Popup_Item);

        popup.SetItemData(_itemDataComponent);
    }

    public void Release()
    {

    }
}
