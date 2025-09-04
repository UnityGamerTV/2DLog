using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataManager : Singleton<DataManager>
{

    public void CopyRandartData(ItemDataComponent target, RandartData newRandartData)
    {
        target._no = newRandartData._no;
        target._name = newRandartData._name + target._name;
        target._max_hp += newRandartData._max_hp;
        target._max_mp += newRandartData._max_mp;
        target._min_attack += newRandartData._min_attack;
        target._max_attack += newRandartData._max_attack;
        target._defence += newRandartData._defence;
        target._min_magic_attack += newRandartData._min_magic_attack;
        target._max_magic_attack += newRandartData._max_magic_attack;
        target._fire_res += newRandartData._fire_res;
        target._cold_res += newRandartData._cold_res;
        target._earth_res += newRandartData._earth_res;
        target._dark_res += newRandartData._dark_res;
        target._poison_res += newRandartData._poison_res;
        target._avoid += newRandartData._avoid;
    }

    public void CopyItemData(ItemDataComponent target, ItemData newItemData)
    {
        target._no = newItemData._no;
        target._name = newItemData._name;
        target._max_hp = newItemData._max_hp;
        target._max_mp = newItemData._max_mp;
        target._min_attack = newItemData._min_attack;
        target._max_attack = newItemData._max_attack;
        target._defence = newItemData._defence;
        target._min_magic_attack = newItemData._min_magic_attack;
        target._max_magic_attack = newItemData._max_magic_attack;
        target._fire_res = newItemData._fire_res;
        target._cold_res = newItemData._cold_res;
        target._earth_res = newItemData._earth_res;
        target._dark_res = newItemData._dark_res;
        target._poison_res = newItemData._poison_res;
        target._avoid = newItemData._avoid;
        target._item_property_type = newItemData._item_property_type;
        target._hand = newItemData._hand;
        target._enhance_limit = newItemData._enhance_limit;
        target._skill_limit = newItemData._skill_limit;
        target._nickName = newItemData._nickName;
        target._comment = newItemData._comment;
    }

    public void CopyPotionData(PotionDataComponent target, PotionData newPotionData)
    {
        target._no = newPotionData._no;
        target._name = newPotionData._name;
        target._max_hp = newPotionData._max_hp;
        target._current_hp = newPotionData._current_hp;
        target._max_mp = newPotionData._max_mp;
        target._current_mp = newPotionData._current_mp;
        target._min_attack = newPotionData._min_attack;
        target._max_attack = newPotionData._max_attack;
        target._defence = newPotionData._defence;
        target._min_magic_attack = newPotionData._min_magic_attack;
        target._max_magic_attack = newPotionData._max_magic_attack;
        target._fire_res = newPotionData._fire_res;
        target._cold_res = newPotionData._cold_res;
        target._earth_res = newPotionData._earth_res;
        target._dark_res = newPotionData._dark_res;
        target._poison_res = newPotionData._poison_res;
        target._avoid = newPotionData._avoid;
        target._turn = newPotionData._turn;
        target._nickName = newPotionData._nickName;
        target._comment = newPotionData._comment;
    }

    public void CopyScrollData(ScrollDataComponent target, ScrollData newScrollData)
    {
        target._no = newScrollData._no;
        target._name = newScrollData._name;
        target._avoid = newScrollData._avoid;
        target._enhance = newScrollData._enhance;
        target._nickName = newScrollData._nickName;
        target._comment = newScrollData._comment;
    }

    public void CopyMagicData(MagicDataComponent target, MagicData newMagicData)
    {
        target._no = newMagicData._no;
        target._name = newMagicData._name;
        target._base_damage = newMagicData._base_damage;
        target._reach = newMagicData._reach;
        target._range = newMagicData._range;
        target._magic_attack_type = newMagicData._magic_attack_type;
        target._mp_consume = newMagicData._mp_consume;
        target._skill_limit = newMagicData._skill_limit;
        target._magic_property_type = newMagicData._magic_property_type;
        target._turn = newMagicData._turn;
        target._magic_effect_type = newMagicData._magic_effect_type;
        target._max_hp = newMagicData._max_hp;
        target._max_mp = newMagicData._max_mp;
        target._defence = newMagicData._defence;
        target._min_magic_attack = newMagicData._min_magic_attack;
        target._max_magic_attack = newMagicData._max_magic_attack;
        target._fire_res = newMagicData._fire_res;
        target._cold_res = newMagicData._cold_res;
        target._earth_res = newMagicData._earth_res;
        target._dark_res = newMagicData._dark_res;
        target._poison_res = newMagicData._poison_res;
        target._avoid = newMagicData._avoid;
        target._nickName = newMagicData._nickName;
        target._icon = newMagicData._icon;
        target._comment = newMagicData._comment;
    }

    public void CopyMonsterData(MonsterDataComponent target, MonsterData newMonsterData)
    {
        target._name = newMonsterData._name;
        target._current_hp = newMonsterData._max_hp;
        target._max_hp = newMonsterData._max_hp;
        target._min_attack = newMonsterData._min_attack;
        target._max_attack = newMonsterData._max_attack;
        target._defence = newMonsterData._defence;
        target._fire_res = newMonsterData._fire_res;
        target._cold_res = newMonsterData._cold_res;
        target._earth_res = newMonsterData._earth_res;
        target._dark_res = newMonsterData._dark_res;
        target._poison_res = newMonsterData._poison_res;
        target._attackType = newMonsterData._attackType;
        target._magic1 = newMonsterData._magic1;
        target._magic2 = newMonsterData._magic2;
        target._comment = newMonsterData._comment;
    }
}
