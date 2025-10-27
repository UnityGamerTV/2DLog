using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup_ItemView : MonoBehaviour
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;

    [FindComponents("StatIcon1", "StatIcon2", "StatIcon3", "StatIcon4", "StatIcon5", "StatIcon6"), SerializeField]
    private List<Image> statIcons = new();
    [FindComponents("TopImage"), SerializeField] private Image topImage;
    [FindComponents("ItemImage"), SerializeField] private Image itemImage;
    [FindComponents("BlackImage"), SerializeField] private Image blackImage;
    [FindComponents("ItemName"), SerializeField] private Text itemName;
    [FindComponents("EnhanceText"), SerializeField] private Text enhanceText;
    [FindComponents("StatText1", "StatText2", "StatText3", "StatText4", "StatText5", "StatText6"), SerializeField]
    private List<Text> statTexts = new();
    [FindComponents("ItemCommentText"), SerializeField] private Text commentText; 
    [FindComponents("InfoToggle"), SerializeField] private Toggle infoToggle;
    [FindComponents("CommentToggle"), SerializeField] private Toggle commentToggle;
    [FindComponents("Model"), SerializeField] private UI_Popup_ItemModel model;

    private readonly string normalSpritePath = "Sprite/Item/ItemPopup_Top_Normal";
    private readonly string randartSpritePath = "Sprite/Item/ItemPopup_Top_Randart";
    private readonly string fixdartSpritePath = "Sprite/Item/ItemPopup_Top_FixDart";
    private readonly string itemSpritePath = "Sprite/Item/Item";
    private readonly string attackIconPath = "Sprite/StatIcon/AtkIcon";
    private readonly string m_attackIconPath = "Sprite/StatIcon/M_AtkIcon";
    private readonly string defenseIconPath = "Sprite/StatIcon/DefenseIcon";
    private readonly string fireIconPath = "Sprite/StatIcon/FireIcon";
    private readonly string coldIconPath = "Sprite/StatIcon/ColdIcon";
    private readonly string darkIconPath = "Sprite/StatIcon/DarkIcon";
    private readonly string poisonIconPath = "Sprite/StatIcon/PoisonIcon";
    private readonly string earthIconPath = "Sprite/StatIcon/EarthIcon";
    private readonly string hpIconPath = "Sprite/StatIcon/HpIcon";
    private readonly string mpIconPath = "Sprite/StatIcon/MpIcon";
    private readonly string evasionIconPath = "Sprite/StatIcon/EvasionIcon";

    private Sprite[] itemImages;
    private Sprite normalSprite, randartSprite, fixdartSprite;
    private Sprite attackIcon, m_attackIcon, defense, fireIcon, coldIcon, darkIcon,
        poisonIcon, earthIcon, hpIcon, mpIcon, evasionIcon;
    private bool isInfo;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        normalSprite = resourceManager.Load<Sprite>(normalSpritePath);
        randartSprite = resourceManager.Load<Sprite>(randartSpritePath);
        fixdartSprite = resourceManager.Load<Sprite>(fixdartSpritePath);
        itemImages = resourceManager.LoadAll<Sprite>(itemSpritePath);
        attackIcon = resourceManager.Load<Sprite>(attackIconPath);
        defense = resourceManager.Load<Sprite>(defenseIconPath);
        m_attackIcon = resourceManager.Load<Sprite>(m_attackIconPath);
        fireIcon = resourceManager.Load<Sprite>(fireIconPath);
        coldIcon = resourceManager.Load<Sprite>(coldIconPath);
        darkIcon = resourceManager.Load<Sprite>(darkIconPath);
        poisonIcon = resourceManager.Load<Sprite>(poisonIconPath);
        earthIcon = resourceManager.Load<Sprite>(earthIconPath);
        hpIcon = resourceManager.Load<Sprite>(hpIconPath);
        mpIcon = resourceManager.Load<Sprite>(mpIconPath);
        evasionIcon = resourceManager.Load<Sprite>(evasionIconPath);

        isInfo = true;
        infoToggle.onValueChanged.AddListener(OnInfoToggle);
        commentToggle.onValueChanged.AddListener(OnCommentToggle);
        
        model.updateAction += UpdateData;
    }

    public void UpdateData(ItemDataComponent component)
    {
        SetTopImage(component);
        SetTopEnhance(component);
        SetTopItem(component);
        SetItemName(component);
        SetStatIcon(component);
        SetStatText(component);
        SetComment(component);
        OnInfoToggle(isInfo);
    }

    private void SetTopImage(ItemDataComponent component)
    {
        switch (component._data._item_grade)
        {
            case ItemGrade.NORMAL: topImage.sprite = normalSprite; break;
            case ItemGrade.RANDART: topImage.sprite = randartSprite; break;
            case ItemGrade.FIXDART: topImage.sprite = fixdartSprite; break;
        }
    }

    private void SetTopEnhance(ItemDataComponent component) => enhanceText.text = $"+{component._data._current_enhance}";

    private void SetTopItem(ItemDataComponent component)
    {
        if (component._data._nickname.Equals(string.Empty))
            return;

        for (int i = 0; i < itemImages.Length; i++)
        {
            if (itemImages[i].name.Equals(component._data._nickname))
            {
                itemImage.sprite = itemImages[i];
                break;
            }
        }
    }

    private void SetItemName(ItemDataComponent component) => itemName.text = component._data._name;
    
    private void SetStatIcon(ItemDataComponent component)
    {
        List<Sprite> sprites = new();

        if (component._data._max_attack > 0)
            sprites.Add(attackIcon);
        if (component._data._defense > 0)
            sprites.Add(defense);
        if (component._data._max_magic_attack > 0)
            sprites.Add(m_attackIcon);
        if (component._data._fire_resist > 0)
            sprites.Add(fireIcon);
        if (component._data._cold_resist > 0)
            sprites.Add(coldIcon);
        if (component._data._dark_resist > 0)
            sprites.Add(darkIcon);
        if (component._data._poison_resist > 0)
            sprites.Add(poisonIcon);
        if (component._data._earth_resist > 0)
            sprites.Add(earthIcon);
        if (component._data._max_hp > 0)
            sprites.Add(hpIcon);
        if (component._data._max_mp > 0)
            sprites.Add(mpIcon);
        if (component._data._evasion > 0)
            sprites.Add(evasionIcon);

        int count = Mathf.Min(statIcons.Count, sprites.Count);

        for (int i = 0; i < count; i++)
        {
            statIcons[i].sprite = sprites[i];
            statIcons[i].gameObject.SetActive(true);
        }

        for (int i = count; i < statIcons.Count; i++)
        {
            statIcons[i].sprite = null;
            statIcons[i].gameObject.SetActive(false);
        }
        
    }

    private void SetStatText(ItemDataComponent component)
    {
        List<string> texts = new();

        if (component._data._max_attack > 0)
            texts.Add($": {component._data._min_attack} ~ {component._data._max_attack}");
        if (component._data._defense > 0)
            texts.Add($" + {component._data._defense}");
        if (component._data._max_magic_attack > 0)
            texts.Add($": {component._data._min_magic_attack} ~ {component._data._max_magic_attack}");
        if (component._data._fire_resist > 0)
            texts.Add($" + {component._data._fire_resist}");
        if (component._data._cold_resist > 0)
            texts.Add($" + {component._data._cold_resist}");
        if (component._data._dark_resist > 0)
            texts.Add($" + {component._data._dark_resist}");
        if (component._data._poison_resist > 0)
            texts.Add($" + {component._data._poison_resist}");
        if (component._data._earth_resist > 0)
            texts.Add($" + {component._data._earth_resist}");
        if (component._data._max_hp > 0)
            texts.Add($" + {component._data._max_hp}");
        if (component._data._max_mp > 0)
            texts.Add($" + {component._data._max_mp}");
        if (component._data._evasion > 0)
            texts.Add($" + {component._data._evasion}");

        int count = Mathf.Min(statTexts.Count, texts.Count);

        for (int i = 0; i < count; i++)
        {
            statTexts[i].text = texts[i];
            statTexts[i].gameObject.SetActive(true);
        }
        
        for (int i = count; i < statTexts.Count; i++)
        {
            statTexts[i].text = string.Empty;
            statTexts[i].gameObject.SetActive(false);
        }
    }

    private void SetComment(ItemDataComponent component) => commentText.text = component._data._comment;
    
    public void OnDetail(bool onDetail)
    {
        blackImage.gameObject.SetActive(onDetail);
        enhanceText.gameObject.SetActive(onDetail);
    }

    private void OnInfoToggle(bool isOn)
    {
        for (int i = 0; i < statTexts.Count; i ++)
        {
            if (statTexts[i].text != string.Empty)
                statTexts[i].gameObject.SetActive(isOn);
            if (statIcons[i].sprite != null)
                statIcons[i].gameObject.SetActive(isOn);
        }
        isInfo = isOn;
    }

    private void OnCommentToggle(bool isOn)
    {
        commentText.gameObject.SetActive(isOn);
    }

    public void Release()
    {
        model.updateAction -= UpdateData;
    }

}
