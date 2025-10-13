using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인벤 데이터
public partial class PlayerDataComponent : DecoratorDataComponent
{
    // 소모품 인벤
    public Dictionary<ConsumeType, InvenItemData> _consumeInven { get { return consumeInven; } set { consumeInven = value; } }
    [SerializeField] private Dictionary<ConsumeType, InvenItemData> consumeInven = new();// 초기화를 같은 파일에서 안할경우 헷갈릴수 있음

    // 리스트로 장비 인벤 한다면?
    public List<InvenItemData> _equipmentInven
    {
        get { return equipmentInven; }
        set
        {
            equipmentInven = value;
            eventManager.PostNotification(EVENT_PLAYER.PLAYER_EQUIP_INVENTORY_UPDATED, this, equipmentInven);
        }
    }
    [SerializeField] private List<InvenItemData> equipmentInven = new();
    //
    //public List<ItemDataComponent> _equipmentInven { get { return equipmentInven; } set { value = equipmentInven; } }
    //[SerializeField] private List<ItemDataComponent> equipmentInven = new();

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

        var itemCount = 1;
        var invenSlotCount = 15;

        if (equipmentInven.Count >= invenSlotCount)
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
        invenItemData._itemData = itemData;
        invenItemData._itemCount = itemCount; // 획득 시 수량 1 고정

        equipmentInven.Add(invenItemData);
    }
}

/// <summary>
/// 인벤토리 데이터 공용 전달 매개변수
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public struct InventoryData<TKey, TValue>
{
    public TKey key;
    public TValue value;
}
