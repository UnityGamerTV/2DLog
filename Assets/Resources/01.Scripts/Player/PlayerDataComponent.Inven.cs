using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 인벤 데이터
public partial class PlayerDataComponent : DecoratorDataComponent
{
    // 소모품 인벤
    public Dictionary<ConsumeType, InvenItemData> _consumeInven { get { return consumeInven; } set { consumeInven = value; } }
    [SerializeField] private Dictionary<ConsumeType, InvenItemData> consumeInven = new();// 초기화를 같은 파일에서 안할경우 헷갈릴수 있음

    //public List<ItemDataComponent> _consumeInven { get => consumeInven; set => consumeInven = value; }
    //[SerializeField] private List<ItemDataComponent> consumeInven = new();

    // 장비 인벤 
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

    public bool IsEquipInvenFull()
    {
        int fullCount = 15;
        if (_equipmentInven.Count >= fullCount)
            return true;

        return false;
    }

    public bool IsConsumeInvenFull()
    {
        int fullCount = 15;
        if (_consumeInven.Count >= fullCount)
            return true;

        return false;
    }

    public void GetItem()
    {
        var component = fieldManager._getItemController._baseDataComponent as ItemDataComponent;
        if (component == null)
            return;

        // 아이템 타입 판별
        var inventoryType = component._data._inventory_type;
        if (inventoryType == null)
            return;

        switch (inventoryType)
        {
            case InventoryType.EQUIPMENT: AddEquipItem(component); break;
            case InventoryType.CONSUME: AddConsumeItem(component); break;
        }
    }

    private void AddEquipItem(ItemDataComponent baseData)
    {
        if (IsEquipInvenFull())
            return;

        var newData = CopyItemData(baseData);
        _equipmentInven.Add(newData);
        eventManager.PostNotification(EVENT_EQUIP_INVEN_UI.RESPONDED_EQUIP_INVENTORY_DATA, this, _equipmentInven);
    }

    private void AddConsumeItem(ItemDataComponent baseData)
    {
        // 1. Enum 변환
        if (!Enum.TryParse<ConsumeType>(baseData._data._nickname, out var consumeType))
            return;

        // 2. 이미 있는 종류면 수량만 증가
        if (_consumeInven.TryGetValue(consumeType, out var invenItemData))
        {
            invenItemData._itemCount += baseData._count;
            eventManager.PostNotification(EVENT_CONSUME_INVEN_UI.RESPONDED_CONSUME_INVENTORY_DATA, this, _consumeInven);
            return;
        }

        // 3. 새로운 종류 추가 전 슬롯 제한 체크
        if (IsConsumeInvenFull())
            return;

        // 4. 실제로 추가할 때만 컴포넌트 생성
        var newData = CopyItemData(baseData);
        _consumeInven.Add(consumeType, new InvenItemData
        {
            _itemData = newData._data,
            _itemCount = newData._count,
            _itemDataComponent = newData
        });
        eventManager.PostNotification(EVENT_CONSUME_INVEN_UI.RESPONDED_CONSUME_INVENTORY_DATA, this, _consumeInven);
    }

    private ItemDataComponent CopyItemData(ItemDataComponent baseData)
    {
        var newData = gameObject.AddComponent<ItemDataComponent>();
        newData.Init(new ItemData()); // TODO 나중에 성능 생각하면 캐싱
        newData._data._id = baseData._data._id;
        newData._data._name = baseData._data._name;
        newData._data._max_hp = baseData._data._max_hp;
        newData._data._max_mp = baseData._data._max_mp;
        newData._data._min_attack = baseData._data._min_attack;
        newData._data._max_attack = baseData._data._max_attack;
        newData._data._defense = baseData._data._defense;
        newData._data._min_magic_attack = baseData._data._min_magic_attack;
        newData._data._max_magic_attack = baseData._data._max_magic_attack;
        newData._data._fire_resist = baseData._data._fire_resist;
        newData._data._cold_resist = baseData._data._cold_resist;
        newData._data._earth_resist = baseData._data._earth_resist;
        newData._data._dark_resist = baseData._data._dark_resist;
        newData._data._poison_resist = baseData._data._poison_resist;
        newData._data._evasion = baseData._data._evasion;
        newData._data._item_grade = baseData._data._item_grade;
        newData._data._equip_slot = baseData._data._equip_slot;
        newData._data._element_type = baseData._data._element_type;
        newData._data._hand_type = baseData._data._hand_type;
        newData._data._current_enhance = baseData._data._current_enhance;
        newData._data._max_enhance = baseData._data._max_enhance;
        newData._data._required_skill_level = baseData._data._required_skill_level;
        newData._data._nickname = baseData._data._nickname;
        newData._data._comment = baseData._data._comment;
        newData._count = baseData._count;
        newData._currentEnhance = baseData._currentEnhance;
        return newData;
    }

    public void ResponseEquipInven() => eventManager.PostNotification(EVENT_EQUIP_INVEN_UI.RESPONDED_EQUIP_INVENTORY_DATA, this, _equipmentInven);

    public void ResponseConsumeInven() => eventManager.PostNotification(EVENT_CONSUME_INVEN_UI.RESPONDED_CONSUME_INVENTORY_DATA, this, _consumeInven);

    public void RemoveItem(ItemDataComponent component, bool isDestroy = true)
    {
        _equipmentInven.Remove(component);
        if (isDestroy) Destroy(component);
        eventManager.PostNotification(EVENT_EQUIP_INVEN_UI.RESPONDED_EQUIP_INVENTORY_DATA, this, _equipmentInven);
    }

    public void RemoveItem(ConsumeType consumeType, int count)
    {
        if (!_consumeInven.TryGetValue(consumeType, out var invenItemData))
            return;

        invenItemData._itemCount -= count;
        if (invenItemData._itemCount <= 0)
        {
            _consumeInven.Remove(consumeType);
            if (invenItemData._itemDataComponent != null)
                Destroy(invenItemData._itemDataComponent);
        }
        eventManager.PostNotification(EVENT_CONSUME_INVEN_UI.RESPONDED_CONSUME_INVENTORY_DATA, this, _consumeInven);
    }
}

