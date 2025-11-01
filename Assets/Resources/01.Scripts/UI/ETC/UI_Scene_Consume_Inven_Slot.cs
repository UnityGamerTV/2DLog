using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Consume_Inven_Slot : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [Singleton(typeof(UIManager))] private UIManager uiManager;
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;

    [FindComponents("Model"), SerializeField] private UI_Scene_Consume_InvenModel model;
    [FindComponents(true, "SlotButton"), SerializeField] private Button button;
    [FindComponents(true, "SlotButton"), SerializeField] private Image slotBG;
    [FindComponents(true, "ItemImage"), SerializeField] private Image itemImage;
    [FindComponents(true, "BlackImage"), SerializeField] private Image blackImage;
    [FindComponents(true, "CountText"), SerializeField] private Text countText;

    public InvenItemData _invenItemData { get { return invenItemData; } set { invenItemData = value; SetData(invenItemData); } }
    [SerializeField] private InvenItemData invenItemData;

    private readonly string emptySlotPath = "Sprite/Inven/Inven_EmptySlot";
    private readonly string normalSlotPath = "Sprite/Inven/Inven_NormalSlot";
    private string itemPath = "Sprite/Item/Item";
    private Sprite[] itemSprites;
    private Sprite emptySlotSprite;
    private Sprite normalSlotSprite;
    private Sprite itemSprite;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);

        button.onClick.AddListener(OnButtonClick);

        emptySlotSprite = resourceManager.Load<Sprite>(emptySlotPath);
        normalSlotSprite = resourceManager.Load<Sprite>(normalSlotPath);
        itemSprites = resourceManager.LoadAll<Sprite>($"{itemPath}");

        button.interactable = false;
        slotBG.sprite = emptySlotSprite;
        itemImage.gameObject.SetActive(false);
        countText.gameObject.SetActive(false);
        blackImage.gameObject.SetActive(false);
    }

    public void OnDetail(bool isDetail)
    {
        if (_invenItemData == null)
            return;

        countText.gameObject.SetActive(isDetail);
        blackImage.gameObject.SetActive(isDetail);
    }

    public void SetData(InvenItemData data)
    {
        if (data == null)
        {
            countText.text = string.Empty;
            itemImage.sprite = null;
            slotBG.sprite = emptySlotSprite;
            countText.gameObject.SetActive(false);
            itemImage.gameObject.SetActive(false);
            blackImage.gameObject.SetActive(false);
            button.interactable = false;
            return;
        }

        var count = data._itemCount.ToString();
        var name = data._itemData._nickname;

        if (Enum.TryParse<ConsumeType>(name, out var consumeType))
        {
            countText.text = $"x{count}";
            itemSprite = Array.Find(itemSprites, s => s.name.Equals(name));
            itemImage.sprite = itemSprite;
            itemImage.gameObject.SetActive(true);
            slotBG.sprite = normalSlotSprite;
            button.interactable = true;
        }
    }

    public void OnButtonClick()
    {
        var popup = uiManager.ShowPopupUI<UI_Popup_ItemController>(UI_POPUP_ENUM.UI_Popup_Item);

        if (_invenItemData != null && _invenItemData._itemDataComponent != null)
        {
            popup.SetItemData(_invenItemData._itemDataComponent);
        }
    }

    public void Release()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }
}
