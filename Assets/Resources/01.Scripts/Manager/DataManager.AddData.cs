using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager : Singleton<DataManager>
{
    public RandartData AddRandartData(int optionNumer)
    {
        RandartData randartData = null;
        if (!randartDic.TryGetValue(optionNumer, out randartData))
            return null;

        return new RandartData
        {
            _id = randartData._id,
            _max_hp = randartData._max_hp,
            _max_mp = randartData._max_mp,
            _min_attack = randartData._min_attack,
            _max_attack = randartData._max_attack,
            _defense = randartData._defense,
            _min_magic_attack = randartData._min_magic_attack,
            _max_magic_attack = randartData._max_magic_attack,
            _fire_resist = randartData._fire_resist,
            _cold_resist = randartData._cold_resist,
            _earth_resist = randartData._earth_resist,
            _dark_resist = randartData._dark_resist,
            _poison_resist = randartData._poison_resist,
            _evasion = randartData._evasion,
            _option_name = randartData._option_name,

            _name = GetLocalizedText(randartData._option_name),
        };
    }

    public ItemData AddItemData(string spriteName)
    {
        ItemData itemData = null;

        List<Dictionary<string, ItemData>> itemDictionaries = new()
        {
            amuletDic, armorDic, axeDic, bootsDic, bowDic,
            gloveDic, helmetDic, maceDic, ringDic, robeDic,
            shieldDic, spearDic, staffDic, swordDic, etcDic
        };

        foreach (var dic in itemDictionaries)
        {
            if (dic.TryGetValue(spriteName, out itemData))
            {
                return new ItemData
                {
                    _id = itemData._id,
                    _max_hp = itemData._max_hp,
                    _max_mp = itemData._max_mp,
                    _min_attack = itemData._min_attack,
                    _max_attack = itemData._max_attack,
                    _defense = itemData._defense,
                    _min_magic_attack = itemData._min_magic_attack,
                    _max_magic_attack = itemData._max_magic_attack,
                    _fire_resist = itemData._fire_resist,
                    _cold_resist = itemData._cold_resist,
                    _earth_resist = itemData._earth_resist,
                    _dark_resist = itemData._dark_resist,
                    _poison_resist = itemData._poison_resist,
                    _evasion = itemData._evasion,
                    _item_grade = itemData._item_grade,
                    _data_type = itemData._data_type,
                    _inventory_type = itemData._inventory_type,
                    _Item_type = itemData._Item_type,
                    _equip_slot = itemData._equip_slot,
                    _element_type = itemData._element_type,
                    _hand_type = itemData._hand_type,
                    _current_enhance = itemData._current_enhance,
                    _max_enhance = itemData._max_enhance,
                    _required_skill_level = itemData._required_skill_level,
                    _nickname = itemData._nickname,

                    _name = GetLocalizedText(spriteName),
                    _comment = GetLocalizedText($"{spriteName}_comment"),
                };
            };
        }
        return null;
    }

    public PotionData AddPotionData(string spriteName)
    {
        PotionData potionData = null;

        if (!potionDic.TryGetValue(spriteName, out potionData))
            return null;

        return new PotionData
        {
            _id = potionData._id,
            _max_hp = potionData._max_hp,
            _current_hp = potionData._current_hp,
            _max_mp = potionData._max_mp,
            _current_mp = potionData._current_mp,
            _min_attack = potionData._min_attack,
            _max_attack = potionData._max_attack,
            _defense = potionData._defense,
            _min_magic_attack = potionData._min_magic_attack,
            _max_magic_attack = potionData._max_magic_attack,
            _fire_resist = potionData._fire_resist,
            _cold_resist = potionData._cold_resist,
            _earth_resist = potionData._earth_resist,
            _dark_resist = potionData._dark_resist,
            _poison_resist = potionData._poison_resist,
            _evasion = potionData._evasion,
            _item_grade = potionData._item_grade,
            _data_type = potionData._data_type,
            _inventory_type = potionData._inventory_type,
            _item_type = potionData._item_type,
            _turn = potionData._turn,
            _nickname = potionData._nickname,

            _name = GetLocalizedText(spriteName),
            _comment = GetLocalizedText($"{spriteName}_comment"),
        };
    }

    public ScrollData AddScrollData(string spriteName)
    {
        ScrollData scrollData = null;

        if (!scrollDic.TryGetValue(spriteName, out scrollData))
            return null;

        return new ScrollData
        {
            _id = scrollData._id,
            _evasion = scrollData._evasion,
            _enhance_value = scrollData._enhance_value,
            _item_grade = scrollData._item_grade,
            _data_type = scrollData._data_type,
            _inventory_type = scrollData._inventory_type,
            _item_type = scrollData._item_type,
            _turn = scrollData._turn,
            _nickname = scrollData._nickname,

            _name = GetLocalizedText(spriteName),
            _comment = GetLocalizedText($"{spriteName}_comment"),
        };
    }

    public MagicData AddMagicData(string spriteName)
    {
        MagicData magicData = null;

        if (!magicDic.TryGetValue(spriteName, out magicData))
            return null;

        return new MagicData
        {
            _id = magicData._id,
            _base_damage = magicData._base_damage,
            _cast_range = magicData._cast_range,
            _effect_radius = magicData._effect_radius,
            _magic_cast_type = magicData._magic_cast_type,
            _mp_consume = magicData._mp_consume,
            _required_skill_level = magicData._required_skill_level,
            _element_type = magicData._element_type,
            _turn = magicData._turn,
            _status_effect_type = magicData._status_effect_type,
            _max_hp = magicData._max_hp,
            _max_mp = magicData._max_mp,
            _min_attack = magicData._min_attack,
            _max_attack = magicData._max_attack,
            _defense = magicData._defense,
            _min_magic_attack = magicData._min_magic_attack,
            _max_magic_attack = magicData._max_magic_attack,
            _fire_resist = magicData._fire_resist,
            _cold_resist = magicData._cold_resist,
            _earth_resist = magicData._earth_resist,
            _dark_resist = magicData._dark_resist,
            _poison_resist = magicData._poison_resist,
            _evasion = magicData._evasion,
            _item_grade = magicData._item_grade,
            _data_type= magicData._data_type,
            _nickname = magicData._nickname,
            _icon = magicData._icon,

            _name = GetLocalizedText(spriteName),
            _comment = GetLocalizedText($"{spriteName}_comment"),
        };
    }

    public MeleeData AddMeleeData(string abilityName)
    {
        MeleeData meleeData = null;

        if (!meleeDic.TryGetValue(abilityName, out meleeData))
            return null;

        return new MeleeData
        {
            _id = meleeData._id,
            _base_damage = meleeData._base_damage,
            _cast_range = meleeData._cast_range,
            _effect_radius = meleeData._effect_radius,
            _magic_cast_type = meleeData._magic_cast_type,
            _mp_consume = meleeData._mp_consume,
            _required_skill_level = meleeData._required_skill_level,
            _element_type = meleeData._element_type,
            _turn = meleeData._turn,
            _status_effect_type = meleeData._status_effect_type,
            _max_hp = meleeData._max_hp,
            _max_mp = meleeData._max_mp,
            _min_attack = meleeData._min_attack,
            _max_attack = meleeData._max_attack,
            _defense = meleeData._defense,
            _min_magic_attack = meleeData._min_magic_attack,
            _max_magic_attack = meleeData._max_magic_attack,
            _fire_resist = meleeData._fire_resist,
            _cold_resist = meleeData._cold_resist,
            _earth_resist = meleeData._earth_resist,
            _dark_resist = meleeData._dark_resist,
            _poison_resist = meleeData._poison_resist,
            _evasion = meleeData._evasion,
            _nickname = meleeData._nickname,
            _icon = meleeData._icon,

            _name = GetLocalizedText(abilityName),
            _comment = GetLocalizedText($"{abilityName}_comment"),
        };
    }

    public MonsterData AddMonsterData(string spriteName)
    {
        MonsterData monsterData = null;

        if (!monsterDic.TryGetValue(spriteName, out monsterData))
            return null;

        return new MonsterData
        {
            _max_hp = monsterData._max_hp,
            _min_attack = monsterData._min_attack,
            _max_attack = monsterData._max_attack,
            _defense = monsterData._defense,
            _fire_resist = monsterData._fire_resist,
            _cold_resist = monsterData._cold_resist,
            _earth_resist = monsterData._earth_resist,
            _dark_resist = monsterData._dark_resist,
            _poison_resist = monsterData._poison_resist,
            _element_type = monsterData._element_type,
            _magic1 = monsterData._magic1,
            _magic2 = monsterData._magic2,

            _name = GetLocalizedText(spriteName),
            _comment = GetLocalizedText($"{spriteName}_comment"),
        };
    }

    public ParticleData AddMagicParticleData(string particleName)
    {
        MagicData magicData = null;

        if (!magicDic.TryGetValue(particleName, out magicData))
            return null;

        return new ParticleData
        {
            // DecoratorDataComponent
            _max_hp = magicData._max_hp,
            _max_mp = magicData._max_mp,
            _min_attack = magicData._min_attack,
            _max_attack = magicData._max_attack,
            _defense = magicData._defense,
            _min_magic_attack = magicData._min_magic_attack,
            _max_magic_attack = magicData._max_magic_attack,
            _fire_resist = magicData._fire_resist,
            _cold_resist = magicData._cold_resist,
            _earth_resist = magicData._earth_resist,
            _dark_resist = magicData._dark_resist,
            _poison_resist = magicData._poison_resist,
            _evasion = magicData._evasion,
            // ParticleDataComponent
            _name = magicData._name,
            _base_damage = magicData._base_damage,
            _cast_range = magicData._cast_range,
            _effect_radius = magicData._effect_radius,
            _magic_cast_type = magicData._magic_cast_type,
            _mp_consume = magicData._mp_consume,
            _required_skill_level = magicData._required_skill_level,
            _element_type = magicData._element_type,
            _turn = magicData._turn,
            _status_effect_type = magicData._status_effect_type,
            _nickname = magicData._nickname,
            _icon = magicData._icon,
            _comment = magicData._comment,
        };
    }

    public ParticleData AddMeleeParticleData(string particleName)
    {
        MeleeData meleeData = null;

        if (!meleeDic.TryGetValue(particleName, out meleeData))
            return null;

        return new ParticleData
        {
            // DecoratorDataComponent
            _max_hp = meleeData._max_hp,
            _max_mp = meleeData._max_mp,
            _min_attack = meleeData._min_attack,
            _max_attack = meleeData._max_attack,
            _defense = meleeData._defense,
            _min_magic_attack = meleeData._min_magic_attack,
            _max_magic_attack = meleeData._max_magic_attack,
            _fire_resist = meleeData._fire_resist,
            _cold_resist = meleeData._cold_resist,
            _earth_resist = meleeData._earth_resist,
            _dark_resist = meleeData._dark_resist,
            _poison_resist = meleeData._poison_resist,
            _evasion = meleeData._evasion,
            // ParticleDataComponent
            _name = meleeData._name,
            _base_damage = meleeData._base_damage,
            _cast_range = meleeData._cast_range,
            _effect_radius = meleeData._effect_radius,
            _magic_cast_type = meleeData._magic_cast_type,
            _mp_consume = meleeData._mp_consume,
            _required_skill_level = meleeData._required_skill_level,
            _element_type = meleeData._element_type,
            _turn = meleeData._turn,
            _status_effect_type = meleeData._status_effect_type,
            _nickname = meleeData._nickname,
            _icon = meleeData._icon,
            _comment = meleeData._comment,
        };
    }

    public ParticleData AddParticleData(string particleName)
    {
        MagicData magicData = null;

        if (!magicDic.TryGetValue(particleName, out magicData))
            return null;

        return new ParticleData
        {
            // DecoratorDataComponent
            _max_hp = magicData._max_hp,
            _max_mp = magicData._max_mp,
            _min_attack = magicData._min_attack,
            _max_attack = magicData._max_attack,
            _defense = magicData._defense,
            _min_magic_attack = magicData._min_magic_attack,
            _max_magic_attack = magicData._max_magic_attack,
            _fire_resist = magicData._fire_resist,
            _cold_resist = magicData._cold_resist,
            _earth_resist = magicData._earth_resist,
            _dark_resist = magicData._dark_resist,
            _poison_resist = magicData._poison_resist,
            _evasion = magicData._evasion,
            // ParticleDataComponent
            _name = magicData._name,
            _base_damage = magicData._base_damage,
            _cast_range = magicData._cast_range,
            _effect_radius = magicData._effect_radius,
            _magic_cast_type = magicData._magic_cast_type,
            _mp_consume = magicData._mp_consume,
            _required_skill_level = magicData._required_skill_level,
            _element_type = magicData._element_type,
            _turn = magicData._turn,
            _status_effect_type = magicData._status_effect_type,
            _nickname = magicData._nickname,
            _icon = magicData._icon,
            _comment = magicData._comment,
        };
    }
}
