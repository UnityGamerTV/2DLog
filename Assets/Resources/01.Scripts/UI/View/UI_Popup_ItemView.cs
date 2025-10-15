using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_ItemView : MonoBehaviour
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;

    [FindComponents(true, "StatIcon1", "StatIcon2", "StatIcon3", "StatIcon4", "StatIcon5", "StatIcon6"), SerializeField]
    private List<Image> statIcons;
    [FindComponents("TopImage"), SerializeField] private Image topImage;
    [FindComponents("ItemImage"), SerializeField] private Image itemImage;
    [FindComponents("BlackImage"), SerializeField] private Image blackImage;
    [FindComponents("ItemName"), SerializeField] private Text itemName;
    [FindComponents("EnhanceText"), SerializeField] private Text enhanceText;
    [FindComponents(true, "StatText1", "StatText2", "StatText3", "StatText4", "StatText5", "StatText6"), SerializeField]
    private List<Text> statTexts;
    [FindComponents("ItemCommentText"), SerializeField] private Text commentText;
    [FindComponents("Model"), SerializeField] private UI_Popup_ItemModel model;

    private readonly string normalSpritePath = "Sprite/Item/ItemPopup_Top_Normal";
    private readonly string randartSpritePath = "Sprite/Item/ItemPopup_Top_Randart";
    private readonly string fixdartSpritePath = "Sprite/Item/ItemPopup_Top_FixDart";
    private readonly string itemSpritePath = "Sprite/Item/Item";
    private Sprite normalSprite;
    private Sprite randartSprite;
    private Sprite fixdartSprite;

    private Sprite[] itemImages;

    public void Init()
    {
        statIcons = new List<Image>();
        statTexts = new List<Text>();

        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        normalSprite = resourceManager.Load<Sprite>(normalSpritePath);
        randartSprite = resourceManager.Load<Sprite>(randartSpritePath);
        fixdartSprite = resourceManager.Load<Sprite>(fixdartSpritePath);
        itemImages = resourceManager.LoadAll<Sprite>(itemSpritePath);

        model.updateAction += UpdateData;
    }

    public void UpdateData(ItemDataComponent component)
    {
        SetTopImage(component);
        SetTopEnhance(component);
    }

    private void SetTopImage(ItemDataComponent component)
    {
        switch (component.data._item_grade)
        {
            case ItemGrade.NORMAL: topImage.sprite = normalSprite; break;
            case ItemGrade.RANDART: topImage.sprite = randartSprite; break;
            case ItemGrade.FIXDART: topImage.sprite = fixdartSprite; break;
        }
    }

    private void SetTopEnhance(ItemDataComponent component)
    {
        if (component.data._current_enhance == 0)
            return;

        enhanceText.text = $"+{component.data._current_enhance}";
    }

    public void Release()
    {
        model.updateAction -= UpdateData;
    }
}
