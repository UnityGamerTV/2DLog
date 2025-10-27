using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_Status_Slot : MonoBehaviour
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(UIManager))] private UIManager uiManager;

    [FindComponents(true, "SlotBG"), SerializeField] private Image slotBG;
    [FindComponents(true, "SlotBG"), SerializeField] private Button button;
    [FindComponents(true, "ItemImage"), SerializeField] private Image itemImage;
    [FindComponents(true, "BlackImage"), SerializeField] private Image blackImage;
    [FindComponents(true, "PositionText"), SerializeField] private Text positonText;
    [FindComponents(true, "EnhanceText"), SerializeField] private Text enhanceText;

    public ItemDataComponent _itemDataComponent { get { return itemDataComponent; } set { itemDataComponent = value; } } //SetData(itemDataComponent); } }
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

        button.interactable = false;
        slotBG.sprite = emptySlotSprite;
        positonText.gameObject.SetActive(true);
        itemImage.gameObject.SetActive(false);
        enhanceText.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(false);
    }

    public void SetData(ItemDataComponent component)
    {
        if (component == null)
        {
            enhanceText.text = string.Empty;
            itemImage.sprite = null;
            slotBG.sprite = emptySlotSprite;
            positonText.gameObject.SetActive(true);
            enhanceText.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(false);
            blackImage.gameObject.SetActive(false);
            button.interactable = false;
            return;
        }

        var enhance = component._data._current_enhance.ToString();
        var name = component._data._nickname;
        var grade = component._data._item_grade;

        if (Enum.TryParse<EquipmentType>(name, out var equipmentType))
        {
            enhanceText.text = $"+{enhance}";
            itemSprite = Array.Find(itemSprites, s => s.name.Equals(name));
            itemImage.sprite = itemSprite;
            itemImage.gameObject.SetActive(true);
            positonText.gameObject.SetActive(false);
            button.interactable = true;
            switch (grade)
            {
                case ItemGrade.NORMAL: slotBG.sprite = normalSlotSprite; break;
                case ItemGrade.RANDART: slotBG.sprite = randartSlotSprite; break;
                case ItemGrade.FIXDART: slotBG.sprite = fixdartSlotSprite; break;
            }
        }
    }

    public void OnButtonClick()
    {
        var popup = uiManager.ShowPopupUI<UI_Popup_ItemController>(UI_POPUP_ENUM.UI_Popup_Item);

        popup.SetItemData(_itemDataComponent);
    }

    private void Release()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }
}
