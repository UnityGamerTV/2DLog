using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Ability_InvenView : MonoBehaviour
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    //[Singleton(typeof(DataManager))] public DataManager dataManager; 안쓰면 지울것

    [FindComponents("Model"), SerializeField] private UI_Scene_Ability_InvenModel model;
    [FindComponents("Slot1", "Slot2", "Slot3", "Slot4", "Slot5", "Slot6", "Slot7", "Slot8")]
    [SerializeField] private List<UI_Scene_Ability_Inven_Slot> slots = new ();
    [FindComponents("ReadingGlassButton"), SerializeField] private Button readingGlassButton;

    private readonly string itemSpritePath = "Sprite/item/item";
    private readonly string abilitySpritePath = "Sprite/Ability";
    private readonly string emptySlotSpritePath = "Sprite/Ability/Ability_EmptySlot";
    private readonly string slotSpritePath = "Sprite/Common/Common_Slot_BG";

    private Dictionary<AbilityType, Sprite> abilitySpriteDic;
    private Sprite emptySlotSprite;
    private Sprite slotSprite;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);

        abilitySpriteDic = new();

        emptySlotSprite = resourceManager.Load<Sprite>(emptySlotSpritePath);
        slotSprite = resourceManager.Load<Sprite>(slotSpritePath);

        SlotInit();
        GetSpriteDic();

        readingGlassButton.onClick.AddListener(OnButtonClick);

        model.changeAbilityAction += SetSlot; // 어빌리티 변경 시
    }
    
    private void SlotInit()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].Init();
    }

    private void GetSpriteDic()
    {
        var abilitySprites = resourceManager.LoadAll<Sprite>(abilitySpritePath);
        var enums = Enum.GetValues(typeof(AbilityType));

        // axe 같은 아이콘 등록
        for (int i = 0; i < abilitySprites.Length; i++)
        {
            var text = abilitySprites[i].name;
            var parts = text.Split('_');
            if (parts.Length == 3)
            {
                var result = parts[1]; // 두 번째 요소
                var abilityNumber = "2";
                var resultText = $"{result}{abilityNumber}";

                foreach (var one in enums)
                {
                    if (resultText.Equals(one.ToString()))
                    {
                        abilitySpriteDic.Add((AbilityType)one, abilitySprites[i]);
                        continue;
                    }
                }
            }
        }

        // item 스프라이트에서 ability 아이콘 등록
        var itemSprites = resourceManager.LoadAll<Sprite>(itemSpritePath);
        for (int i = 0; i < itemSprites.Length; i++)
        {
            foreach (var one in enums)
            {
                if (itemSprites[i].name.Equals(one.ToString()))
                {
                    abilitySpriteDic.Add((AbilityType)one, itemSprites[i]);
                    continue;
                }
            }
        }
    }

    private void SetSlot()
    {
        SetSlotAbility(); // 슬롯 어빌리티 타입 주입
        SetSlotImage(); // 슬롯 이미지 주입
        SetSlotData(); // 슬롯 데이터 주입
    }

    private void SetSlotAbility()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < model._abilityList.Count)
                slots[i].SetAbility(model._abilityList[i]);
            else
                slots[i].SetAbility(AbilityType.NONE);
        }
    }

    private void SetSlotImage()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var ability = slots[i].GetAbility();
            if (ability == AbilityType.NONE)
                slots[i].SetSlotImage(null, emptySlotSprite);
            else
                slots[i].SetSlotImage(abilitySpriteDic[ability], slotSprite);
        }
    }

    private void SetSlotData()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var ability = slots[i].GetAbility();
            if (ability == AbilityType.NONE)
                continue;
            if (ability == AbilityType.bow2 ||
                ability == AbilityType.sword2 ||
                ability == AbilityType.spear2 ||
                ability == AbilityType.axe2 ||
                ability == AbilityType.mace2)
            {
                var data = model.GetMeleeData(ability);
                slots[i].SetSlotData(data);
            }
            else
            {
                var data = model.GetMeleeData(ability);
                slots[i].SetSlotData(data);
            }
        }
    }

    public void OnButtonClick()
    {
        model._onDetail = !model._onDetail;
        if (model._onDetail)
            OnDetail();
        else
            OffDetail();
    }

    private void OnDetail()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].OnDetail();
    }

    private void OffDetail()
    {
        for (int i = 0; i < slots.Count; i++)
            slots[i].OffDetail();
    }

    public void Release()
    {
        model.changeAbilityAction -= SetSlotAbility;
    }
}
