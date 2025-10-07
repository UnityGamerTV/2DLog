using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Ability_Inven_Slot : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [FindComponents("Model"), SerializeField] private UI_Scene_Ability_InvenModel model;
    [FindComponents(true, "AbilityButton"), SerializeField] private Button button;
    [FindComponents(true, "AbilityButton"), SerializeField] private Image abilityImage;
    [FindComponents(true, "SlotBG"), SerializeField] private Image slotBG;
    [FindComponents(true, "EnhanceText"), SerializeField] private Text enhanceText;

    private AbilityType slotAbility;
    private Color32 onDetailColor = new Color32(123, 123, 123, 255);
    private Color32 offDetailColor = new Color32(255, 255, 255, 255);

    public void Init()
    {
        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        button.onClick.AddListener(OnButtonClick);
    }

    public void OnDetail()
    {
        enhanceText.gameObject.SetActive(true);
        abilityImage.color = onDetailColor;
    }

    public void OffDetail()
    {
        enhanceText.gameObject.SetActive(false);
        abilityImage.color = offDetailColor;
    }

    public void SetAbility(AbilityType abilityType) => slotAbility = abilityType;

    public AbilityType GetAbility() => slotAbility;

    public void SetSlotImage(Sprite ability, Sprite slotBG)
    {
        abilityImage.sprite = ability;
        this.slotBG.sprite = slotBG;
    }

    public void SetSlotData(BaseAbilityData data)
    {
        enhanceText.text = $"mp :{data._mp_consume}";
    }

    public void OnButtonClick()
    {
        if (model._onDetail)
            eventManager.PostNotification(EVENT_ABILITY_INVEN_UI.OPEN_ABILITY_DETAIL_UI, this, slotAbility); // 디테일 창 오픈
        else
            eventManager.PostNotification(EVENT_ABILITY_INVEN_UI.USE_ABILITY, this, slotAbility); // 어빌리티 사용
    }

    public void Release()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }

}
