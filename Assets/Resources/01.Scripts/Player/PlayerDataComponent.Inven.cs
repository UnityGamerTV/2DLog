using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인벤 데이터
public partial class PlayerDataComponent : DecoratorDataComponent
{
    // 소모품 인벤
    public Dictionary<ConsumeType, InvenItemData> _consumeInven { get { return consumeInven; } set { consumeInven = value; } }
    [SerializeField] private Dictionary<ConsumeType, InvenItemData> consumeInven = new();

    // 장비 인벤
    public Dictionary<EquipmentType, InvenItemData> _equipmentInven 
    { 
        get { return equipmentInven; } 
        set 
        {
            equipmentInven = value;   
            eventManager.PostNotification(EVENT_PLAYER.PLAYER_EQUIP_INVENTORY_UPDATED, this);
        } 
    }
    [SerializeField] private Dictionary<EquipmentType, InvenItemData> equipmentInven = new();

    // 어빌리티 인벤
    public List<AbilityType> _abilityTypes 
    {
        get { return abilityTypes; } 
        set 
        { 
            abilityTypes = value; 
            eventManager.PostNotification(EVENT_ABILITY_INVEN_UI.SET_ABILITY, this); 
        } 
    }
    [SerializeField] private List<AbilityType> abilityTypes = new();

    // 재화 
    public int _gold { get { return gold; } set { gold = value; } }
    [SerializeField] private int gold;
    public int _diamond { get { return diamond; } set { diamond = value; } }
    [SerializeField] private int diamond;


    public void GetItemComplete()
    {
        var baseData = fieldManager._getItemController._baseDataComponent as ItemDataComponent;
        if (baseData == null)
            return;

        ItemData itemData = new ItemData();
        itemData._id = baseData._id;
        itemData._name = baseData._name;
        itemData._max_hp = baseData._max_hp;
        itemData._max_mp = baseData._max_mp;
        itemData._min_attack = baseData._min_attack;
        itemData._max_attack = baseData._max_attack;
        itemData._defense = baseData._defense;
        itemData._min_magic_attack = baseData._min_magic_attack;
        itemData._max_magic_attack = baseData._max_magic_attack;
        itemData._fire_resist = baseData._fire_resist;
        itemData._cold_resist = baseData._cold_resist;
        itemData._earth_resist = baseData._earth_resist;
        itemData._dark_resist = baseData._dark_resist;
        itemData._poison_resist = baseData._poison_resist;
        itemData._evasion = baseData._evasion;
        itemData._element_type = baseData._element_type;
        itemData._hand_type = baseData._hand_type;
        itemData._current_enhance = baseData._current_enhance;
        itemData._max_enhance = baseData._max_enhance;
        itemData._required_skill_level = baseData._required_skill_level;
        itemData._nickname = baseData._nickname;
        itemData._comment = baseData._comment;

        InvenItemData invenItemData = new InvenItemData();
        invenItemData._itemDataType = fieldManager._getItemController._itemDataType;
        invenItemData._baseDataComponent = itemData;
        invenItemData._itemCount = 1; // 획득 시 수량 1 고정

        // 해당 Enum 을 기준으로 인벤(딕셔너리)에 저장
        foreach (var one in Enum.GetValues(typeof(EquipmentType)))
        {
            if (one.ToString().Equals(itemData._nickname))
            {
                equipmentInven.Add((EquipmentType)one, invenItemData);
                break;
            }
        }
    }
}
