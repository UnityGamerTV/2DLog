using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// 장착 데이터
public partial class PlayerDataComponent : DecoratorDataComponent
{
    public ItemDataComponent _helmet 
    { 
        get => helmet;  
        set 
        { 
            if (helmet != null)
                _unEquipItem = helmet;
            
            helmet = value;
            helmetSlotAction?.Invoke(helmet); 
        } 
    }
    [SerializeField] private ItemDataComponent helmet;

    public ItemDataComponent _armor 
    { 
        get => armor;
        set 
        {
            if (armor != null)
                _unEquipItem = armor;

            armor = value;
            armorSlotAction?.Invoke(armor);
        }
    }
    [SerializeField] private ItemDataComponent armor;

    public ItemDataComponent _amulet 
    { 
        get => amulet;
        set 
        { 
            if (amulet != null)
                _unEquipItem = amulet;

            amulet = value;
            amuletSlotAction?.Invoke(amulet);
        }
    }
    [SerializeField] private ItemDataComponent amulet;

    public ItemDataComponent _weapon 
    { 
        get => weapon;
        set 
        {
            if (weapon != null)
                _unEquipItem = weapon;

            if (value._data._hand_type == 2)
                _shield = value;

            weapon = value;
            weaponSlotAction?.Invoke(weapon);
        }
    }
    [SerializeField] private ItemDataComponent weapon;

    public ItemDataComponent _shield 
    { 
        get => shield;
        set 
        { 
            if (shield != null)
                _unEquipItem = shield;

            shield = value;
            shieldSlotAction?.Invoke(shield);
        }
    }
    [SerializeField] private ItemDataComponent shield;

    public ItemDataComponent _ring1 
    { 
        get => ring1;
        set
        {
            if (ring1 != null)
                _unEquipItem = ring1;

            ring1 = value;
            ring1._data._equip_slot = EquipSlot.RING1;
            ring1SlotAction?.Invoke(ring1);
        }
    }
    [SerializeField] private ItemDataComponent ring1;

    public ItemDataComponent _ring2 
    { 
        get => ring2;
        set
        {
            if (ring2 != null)
                _unEquipItem = ring2;

            ring2 = value;
            ring2._data._equip_slot = EquipSlot.RING2;
            ring2SlotAction?.Invoke(ring2);
        }
    }
    [SerializeField] private ItemDataComponent ring2;

    public ItemDataComponent _unEquipItem 
    { 
        get => unEquipItem;
        set
        {
            unEquipItem = value;
            eventManager.PostNotification(EVENT_PLAYER.PLAYER_ITEM_UNEQUIP, this, unEquipItem);
            unEquipItemAction?.Invoke(unEquipItem);
        }
    }
    [SerializeField] private ItemDataComponent unEquipItem;

    public Action<ItemDataComponent> helmetSlotAction;
    public Action<ItemDataComponent> armorSlotAction;
    public Action<ItemDataComponent> amuletSlotAction;
    public Action<ItemDataComponent> weaponSlotAction;
    public Action<ItemDataComponent> shieldSlotAction;
    public Action<ItemDataComponent> ring1SlotAction;
    public Action<ItemDataComponent> ring2SlotAction;
    public Action<ItemDataComponent> unEquipItemAction;

    public void EquipItem(ItemDataComponent component)
    {
        if (component == null)
            return;

        var equipSlot = component._data._equip_slot;
        var name = component._data._nickname;

        if (Enum.TryParse<EquipmentType>(name, out var equipmentType))
        {
            switch (equipSlot)
            {
                case EquipSlot.HELMET: _helmet = component; break;
                case EquipSlot.ARMOR: _armor = component; break;
                case EquipSlot.AMULET: _amulet = component; break;
                case EquipSlot.WEAPON: _weapon = component; break;
                case EquipSlot.SHIELD: EquipShield(component); break;
                case EquipSlot.RING: EquipRing(component); break;
            }
        }
    }

    private void EquipItem(ItemDataComponent _equipSlot, ItemDataComponent component)
    {
        if (_equipSlot != null)
            _unEquipItem = _equipSlot;

        _equipSlot = component;
    }

    private void EquipWeapon(ItemDataComponent component)
    {
         EquipItem(_weapon, component);

        if (component._data._hand_type == 2)
            EquipItem(_shield, component);
    }

    private void EquipShield(ItemDataComponent component)
    {
        if (component._data._hand_type == 2) // 양손무기라는 뜻
        {
            // 강제로 무기 벗기고 쉴드 착용
            UnEquipItem(EquipSlot.WEAPON);
            EquipItem(component);
        }
    }

    private void EquipRing(ItemDataComponent component)
    {
        if (_ring1 == null)
        {
            _ring1 = component;
            return;
        }

        if (_ring2 == null)
        {
            _ring2 = component;
            return;
        }
        // 
        // 상태창 띄워서 자리 고르도록 // 다른 아이템도 착용하면 상태창 뜨도록
        eventManager.PostNotification(EVENT_PLAYER.PLAYER_RING_SLOT_FULL, this);
    }

    private void EquipRingAtSlot(EquipSlot equipSlot, ItemDataComponent component)
    {
        switch(equipSlot)
        {
            case EquipSlot.RING1: EquipItem(_ring1, component); break;
            case EquipSlot.RING2: EquipItem(_ring2, component); break;
        }
    }

    private void UnEquipItem(EquipSlot equipSlot)
    {
        switch(equipSlot)
        {
            case EquipSlot.HELMET: UnEquip(_helmet); break;
            case EquipSlot.ARMOR: UnEquip(_armor); break;
            case EquipSlot.AMULET: UnEquip(_amulet); break;
            case EquipSlot.WEAPON: UnEquip(_weapon); break;
            case EquipSlot.SHIELD: UnEquip(_shield); break;
            case EquipSlot.RING1: UnEquip(_ring1); break;
            case EquipSlot.RING2: UnEquip(_ring2); break;
        }
    }

    private void UnEquip(ItemDataComponent _equipSlot)
    {
        unEquipItem = _equipSlot;
        _equipSlot = null;
    }

    public void ResponseEquipData() => eventManager.PostNotification(EVENT_STATUS_POPUP_UI.RESPONDED_EQUIP_DATA, this, GetAllEquipItems());


    List<ItemDataComponent> itemDataComponents = new List<ItemDataComponent>();
    private List<ItemDataComponent> GetAllEquipItems()
    {
        itemDataComponents.Clear();
        itemDataComponents.Add(_helmet);
        itemDataComponents.Add(_armor);
        itemDataComponents.Add(_amulet);
        itemDataComponents.Add(_weapon);
        itemDataComponents.Add(_shield);
        itemDataComponents.Add(_ring1);
        itemDataComponents.Add(_ring2);
        return itemDataComponents;
    }
    
}
