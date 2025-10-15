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
    public List<ItemDataComponent> _equipmentInven { get => equipmentInven; set => equipmentInven = value; } // 해당 이벤트는 사용하지 않을 예정 Push 방식은 UI가 열려 있지않으므로 Full 방식으로 변경, eventManager.PostNotification(EVENT_PLAYER.PLAYER_EQUIP_INVENTORY_UPDATED, this, equipmentInven);
    [SerializeField] private List<ItemDataComponent> equipmentInven = new();
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

    public void GetItem()
    {
        var baseData = fieldManager._getItemController._baseDataComponent as ItemDataComponent;
        if (baseData == null)
            return;

        var newData = this.gameObject.AddComponent<ItemDataComponent>();
        newData.Init(new ItemData()); // TODO 나중에 성능 생각하면 캐싱

        newData.data._id = baseData.data._id;
        newData.data._name = baseData.data._name;
        newData.data._max_hp = baseData.data._max_hp;
        newData.data._max_mp = baseData.data._max_mp;
        newData.data._min_attack = baseData.data._min_attack;
        newData.data._max_attack = baseData.data._max_attack;
        newData.data._defense = baseData.data._defense;
        newData.data._min_magic_attack = baseData.data._min_magic_attack;
        newData.data._max_magic_attack = baseData.data._max_magic_attack;
        newData.data._fire_resist = baseData.data._fire_resist;
        newData.data._cold_resist = baseData.data._cold_resist;
        newData.data._earth_resist = baseData.data._earth_resist;
        newData.data._dark_resist = baseData.data._dark_resist;
        newData.data._poison_resist = baseData.data._poison_resist;
        newData.data._evasion = baseData.data._evasion;
        newData.data._item_grade = baseData.data._item_grade;
        newData.data._equip_slot = baseData.data._equip_slot;
        newData.data._element_type = baseData.data._element_type;
        newData.data._hand_type = baseData.data._hand_type;
        newData.data._current_enhance = baseData.data._current_enhance;
        newData.data._max_enhance = baseData.data._max_enhance;
        newData.data._required_skill_level = baseData.data._required_skill_level;
        newData.data._nickname = baseData.data._nickname;
        newData.data._comment = baseData.data._comment;
        newData._count = baseData._count;
        newData._currentEnhance = baseData._currentEnhance;
        _equipmentInven.Add(newData);
    }

    public void ResposeEquipInven() => eventManager.PostNotification(EVENT_EQUIP_INVEN_UI.RESPONDED_EQUIP_INVENTORY_DATA, this, _equipmentInven);
    
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
