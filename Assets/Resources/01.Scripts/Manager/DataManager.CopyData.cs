using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager : Singleton<DataManager>
{

    public void CopyRandartData(ItemDataComponent target, RandartData newRandartData)
    {
        target._id = newRandartData._id;
        target._name = newRandartData._name + target._name;
        target._max_hp += newRandartData._max_hp;
        target._max_mp += newRandartData._max_mp;
        target._min_attack += newRandartData._min_attack;
        target._max_attack += newRandartData._max_attack;
        target._defense += newRandartData._defense;
        target._min_magic_attack += newRandartData._min_magic_attack;
        target._max_magic_attack += newRandartData._max_magic_attack;
        target._fire_resist += newRandartData._fire_resist;
        target._cold_resist += newRandartData._cold_resist;
        target._earth_resist += newRandartData._earth_resist;
        target._dark_resist += newRandartData._dark_resist;
        target._poison_resist += newRandartData._poison_resist;
        target._evasion += newRandartData._evasion;
    }

    public void CopyItemData(ItemDataComponent target, ItemData newItemData)
    {
        target._id = newItemData._id;
        target._name = newItemData._name;
        target._max_hp = newItemData._max_hp;
        target._max_mp = newItemData._max_mp;
        target._min_attack = newItemData._min_attack;
        target._max_attack = newItemData._max_attack;
        target._defense = newItemData._defense;
        target._min_magic_attack = newItemData._min_magic_attack;
        target._max_magic_attack = newItemData._max_magic_attack;
        target._fire_resist = newItemData._fire_resist;
        target._cold_resist = newItemData._cold_resist;
        target._earth_resist = newItemData._earth_resist;
        target._dark_resist = newItemData._dark_resist;
        target._poison_resist = newItemData._poison_resist;
        target._evasion = newItemData._evasion;
        target._dataType= newItemData._data_type;
        target._inventoryType = newItemData._inventory_type;
        target._item_type = newItemData._Item_type;
        target._slot_type = newItemData._slot_type;
        target._element_type = newItemData._element_type;
        target._hand_type = newItemData._hand_type;
        target._current_enhance = newItemData._current_enhance;
        target._max_enhance = newItemData._max_enhance;
        target._required_skill_level = newItemData._required_skill_level;
        target._nickname = newItemData._nickname;
        target._comment = newItemData._comment;
    }

    public void CopyPotionData(PotionDataComponent target, PotionData newPotionData)
    {
        target._id = newPotionData._id;
        target._name = newPotionData._name;
        target._max_hp = newPotionData._max_hp;
        target._current_hp = newPotionData._current_hp;
        target._max_mp = newPotionData._max_mp;
        target._current_mp = newPotionData._current_mp;
        target._min_attack = newPotionData._min_attack;
        target._max_attack = newPotionData._max_attack;
        target._defense = newPotionData._defense;
        target._min_magic_attack = newPotionData._min_magic_attack;
        target._max_magic_attack = newPotionData._max_magic_attack;
        target._fire_resist = newPotionData._fire_resist;
        target._cold_resist = newPotionData._cold_resist;
        target._earth_resist = newPotionData._earth_resist;
        target._dark_resist = newPotionData._dark_resist;
        target._poison_resist = newPotionData._poison_resist;
        target._evasion = newPotionData._evasion;
        target._data_type = newPotionData._data_type;
        target._inventory_type = newPotionData._inventory_type;
        target._item_type = newPotionData._item_type;
        target._turn = newPotionData._turn;
        target._nickname = newPotionData._nickname;
        target._comment = newPotionData._comment;
    }

    public void CopyScrollData(ScrollDataComponent target, ScrollData newScrollData)
    {
        target._id = newScrollData._id;
        target._name = newScrollData._name;
        target._evasion = newScrollData._evasion;
        target._enhance_value = newScrollData._enhance_value;
        target._data_type = newScrollData._data_type;
        target._inventory_type = newScrollData._inventory_type;
        target._item_type = newScrollData._item_type;
        target._nickName = newScrollData._nickname;
        target._comment = newScrollData._comment;
    }

    public void CopyMagicData(MagicDataComponent target, MagicData newMagicData)
    {
        target._id = newMagicData._id;
        target._name = newMagicData._name;
        target._base_damage = newMagicData._base_damage;
        target._cast_range = newMagicData._cast_range;
        target._effect_radius = newMagicData._effect_radius;
        target._magic_cast_type = newMagicData._magic_cast_type;
        target._mp_consume = newMagicData._mp_consume;
        target._required_skill_level = newMagicData._required_skill_level;
        target._element_type = newMagicData._element_type;
        target._turn = newMagicData._turn;
        target._status_effect_type = newMagicData._status_effect_type;
        target._max_hp = newMagicData._max_hp;
        target._max_mp = newMagicData._max_mp;
        target._min_attack = newMagicData._min_attack;
        target._max_attack = newMagicData._max_attack;
        target._defense = newMagicData._defense;
        target._min_magic_attack = newMagicData._min_magic_attack;
        target._max_magic_attack = newMagicData._max_magic_attack;
        target._fire_resist = newMagicData._fire_resist;
        target._cold_resist = newMagicData._cold_resist;
        target._earth_resist = newMagicData._earth_resist;
        target._dark_resist = newMagicData._dark_resist;
        target._poison_resist = newMagicData._poison_resist;
        target._evasion = newMagicData._evasion;
        target._nickname = newMagicData._nickname;
        target._icon = newMagicData._icon;
        target._comment = newMagicData._comment;
    }

    public void CopyMonsterData(MonsterDataComponent target, MonsterData newMonsterData)
    {
        target._name = newMonsterData._name;
        target._current_hp = newMonsterData._current_hp;
        target._max_hp = newMonsterData._max_hp;
        target._min_attack = newMonsterData._min_attack;
        target._max_attack = newMonsterData._max_attack;
        target._defense = newMonsterData._defense;
        target._fire_resist = newMonsterData._fire_resist;
        target._cold_resist = newMonsterData._cold_resist;
        target._earth_resist = newMonsterData._earth_resist;
        target._dark_resist = newMonsterData._dark_resist;
        target._poison_resist = newMonsterData._poison_resist;
        target._element_type = newMonsterData._element_type;
        target._magic1 = newMonsterData._magic1;
        target._magic2 = newMonsterData._magic2;
        target._comment = newMonsterData._comment;
    }

    public void CopyParticleData(ParticleDataComponent target, ParticleData newParticleData)
    {
        // DecoratorDataComponent
        target._max_hp = newParticleData._max_hp;
        target._max_mp = newParticleData._max_mp;
        target._min_attack = newParticleData._min_attack;
        target._max_attack = newParticleData._max_attack;
        target._defense = newParticleData._defense;
        target._min_magic_attack = newParticleData._min_magic_attack;
        target._max_magic_attack = newParticleData._max_magic_attack;
        target._fire_resist = newParticleData._fire_resist;
        target._cold_resist = newParticleData._cold_resist;
        target._earth_resist = newParticleData._earth_resist;
        target._dark_resist = newParticleData._dark_resist;
        target._poison_resist = newParticleData._poison_resist;
        target._evasion = newParticleData._evasion;
        // ParticleDataComponent
        target._name = newParticleData._name;
        target._base_damage = newParticleData._base_damage;
        target._cast_range = newParticleData._cast_range;
        target._effect_radius = newParticleData._effect_radius;
        target._magic_cast_type = newParticleData._magic_cast_type;
        target._mp_consume = newParticleData._mp_consume;
        target._required_skill_level = newParticleData._required_skill_level;
        target._element_type = newParticleData._element_type;
        target._turn = newParticleData._turn;
        target._status_effect_type = newParticleData._status_effect_type;
        target._nickname = newParticleData._nickname;
        target._icon = newParticleData._icon;
        target._comment = newParticleData._comment;
    }
}

