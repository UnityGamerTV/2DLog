using System;

[Serializable]
public class MagicData : INickname
{
    public int _no { get; set; }
    public string _name { get; set; }
    public int? _base_damage { get; set; }
    public int? _reach { get; set; }
    public int? _range { get; set; }
    public MagicAttackType? _magic_attack_type { get; set; }
    public int? _mp_consume { get; set; }
    public int? _skill_limit { get; set; }
    public MagicPropertyType? _magic_property_type { get; set; }
    public int? _turn { get; set; }
    public MagicEffectType? _magic_effect_type { get; set; }
    public int? _max_hp { get; set; }
    public int? _max_mp { get; set; }
    public int? _min_attack { get; set; }
    public int? _max_attack { get; set; }
    public int? _defence { get; set; }
    public int? _min_magic_attack { get; set; }
    public int? _max_magic_attack { get; set; }
    public int? _fire_res { get; set; }
    public int? _cold_res { get; set; }
    public int? _earth_res { get; set; }
    public int? _dark_res { get; set; }
    public int? _poison_res { get; set; }
    public int? _avoid { get; set; }
    public string _nickName { get; set; }
    public string _icon { get; set; }
    public string _comment { get; set; }
}

public enum MagicAttackType
{
    TARGET,
    SELF,
    MISSILE,
    NONE,
}

public enum MagicPropertyType
{
    FIRE,
    COLD,
    EARTH,
    POISON,
    NECRO,
    SUM,
    NONE,
}

public enum MagicEffectType
{
    BURN,
    FREEZE,
    CURSE,
    PARALYSIS,
    POISON,
    NONE,
}
