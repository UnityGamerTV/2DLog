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
            _no = randartData._no,
            _max_hp = randartData._max_hp,
            _max_mp = randartData._max_mp,
            _min_attack = randartData._min_attack,
            _max_attack = randartData._max_attack,
            _defence = randartData._defence,
            _min_magic_attack = randartData._min_magic_attack,
            _max_magic_attack = randartData._max_magic_attack,
            _fire_res = randartData._fire_res,
            _cold_res = randartData._cold_res,
            _earth_res = randartData._earth_res,
            _dark_res = randartData._dark_res,
            _poison_res = randartData._poison_res,
            _avoid = randartData._avoid,
            _option_name = randartData._option_name,

            _name = GetLocalizedText(randartData._option_name),
        };
    }

    public ItemData AddItemData(string spriteName)
    {
        ItemData itemData = null;

        List<Dictionary<string, ItemData>> itemDictionaries = new()
        {
            amuletDic, armourDic, axeDic, bootsDic, bowDic,
            gloveDic, helmetDic, maceDic, ringDic, robeDic,
            shieldDic, spearDic, staffDic, swordDic, etcDic
        };

        foreach (var dic in itemDictionaries)
        {
            if (dic.TryGetValue(spriteName, out itemData))
            {
                return new ItemData
                {
                    _no = itemData._no,
                    _max_hp = itemData._max_hp,
                    _max_mp = itemData._max_mp,
                    _min_attack = itemData._min_attack,
                    _max_attack = itemData._max_attack,
                    _defence = itemData._defence,
                    _min_magic_attack = itemData._min_magic_attack,
                    _max_magic_attack = itemData._max_magic_attack,
                    _fire_res = itemData._fire_res,
                    _cold_res = itemData._cold_res,
                    _earth_res = itemData._earth_res,
                    _dark_res = itemData._dark_res,
                    _poison_res = itemData._poison_res,
                    _avoid = itemData._avoid,
                    _item_property_type = itemData._item_property_type,
                    _hand = itemData._hand,
                    _enhance_limit = itemData._enhance_limit,
                    _skill_limit = itemData._skill_limit,
                    _nickName = itemData._nickName,

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
            _no = potionData._no,
            _max_hp = potionData._max_hp,
            _current_hp = potionData._current_hp,
            _max_mp = potionData._max_mp,
            _current_mp = potionData._current_mp,
            _min_attack = potionData._min_attack,
            _max_attack = potionData._max_attack,
            _defence = potionData._defence,
            _min_magic_attack = potionData._min_magic_attack,
            _max_magic_attack = potionData._max_magic_attack,
            _fire_res = potionData._fire_res,
            _cold_res = potionData._cold_res,
            _earth_res = potionData._earth_res,
            _dark_res = potionData._dark_res,
            _poison_res = potionData._poison_res,
            _avoid = potionData._avoid,
            _turn = potionData._turn,
            _nickName = potionData._nickName,

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
            _no = scrollData._no,
            _avoid = scrollData._avoid,
            _enhance = scrollData._enhance,
            _turn = scrollData._turn,
            _nickName = scrollData._nickName,

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
            _no = magicData._no,
            _base_damage = magicData._base_damage,
            _reach = magicData._reach,
            _range = magicData._range,
            _magic_attack_type = magicData._magic_attack_type,
            _mp_consume = magicData._mp_consume,
            _skill_limit = magicData._skill_limit,
            _magic_property_type = magicData._magic_property_type,
            _turn = magicData._turn,
            _magic_effect_type = magicData._magic_effect_type,
            _max_hp = magicData._max_hp,
            _max_mp = magicData._max_mp,
            _min_attack = magicData._min_attack,
            _max_attack = magicData._max_attack,
            _defence = magicData._defence,
            _min_magic_attack = magicData._min_magic_attack,
            _max_magic_attack = magicData._max_magic_attack,
            _fire_res = magicData._fire_res,
            _cold_res = magicData._cold_res,
            _earth_res = magicData._earth_res,
            _dark_res = magicData._dark_res,
            _poison_res = magicData._poison_res,
            _avoid = magicData._avoid,
            _nickName = magicData._nickName,
            _icon = magicData._icon,

            _name = GetLocalizedText(spriteName),
            _comment = GetLocalizedText($"{spriteName}_comment"),
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
            _defence = monsterData._defence,
            _fire_res = monsterData._fire_res,
            _cold_res = monsterData._cold_res,
            _earth_res = monsterData._earth_res,
            _dark_res = monsterData._dark_res,
            _poison_res = monsterData._poison_res,
            _attackType = monsterData._attackType,
            _magic1 = monsterData._magic1,
            _magic2 = monsterData._magic2,

            _name = GetLocalizedText(spriteName),
            _comment = GetLocalizedText($"{spriteName}_comment"),
        };
    }
}
