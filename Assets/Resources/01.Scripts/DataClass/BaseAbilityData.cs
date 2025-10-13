using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbilityData : INickname
{
    public int _id { get; set; }
    public string _name { get; set; }
    public int? _base_damage { get; set; }
    public int? _cast_range { get; set; }
    public EffectRadius? _effect_radius { get; set; }
    public MagicCastType? _magic_cast_type { get; set; }
    public int? _mp_consume { get; set; }
    public int? _required_skill_level { get; set; }
    public ElementType? _element_type { get; set; }
    public int? _turn { get; set; }
    public StatusEffectType? _status_effect_type { get; set; }
    public int? _max_hp { get; set; }
    public int? _max_mp { get; set; }
    public int? _min_attack { get; set; }
    public int? _max_attack { get; set; }
    public int? _defense { get; set; }
    public int? _min_magic_attack { get; set; }
    public int? _max_magic_attack { get; set; }
    public int? _fire_resist { get; set; }
    public int? _cold_resist { get; set; }
    public int? _earth_resist { get; set; }
    public int? _dark_resist { get; set; }
    public int? _poison_resist { get; set; }
    public int? _evasion { get; set; }
    public ItemGrade? _item_grade { get; set; }
    public DataType? _data_type { get; set; }
    public string _nickname { get; set; }
    public string _icon { get; set; }
    public string _comment { get; set; }
}

public enum EffectRadius
{
    SINGLE,             // 단일 대상
    THREE_HORIZONTAL,   // 가로 3칸
    FIVE_CROSS,         // 십자형 5칸
    NINE_SQUARE,        // 9칸 정사각형
    EIGHT_DIRECTIONS,    // 8방향
    NONE,
}

public enum MagicCastType
{
    TARGET,
    SELF,
    MISSILE,
    NONE,
}

public enum StatusEffectType
{
    BURN,
    FREEZE,
    POISON,
    CURSE,
    PARALYSIS,
    NONE,
}