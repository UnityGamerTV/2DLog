using System;

[Serializable]
public class MonsterData
{
    public string _name { get; set; }
    public int _max_hp { get; set; }
    public int _min_attack { get; set; }
    public int _max_attack { get; set; }
    public int? _defence { get; set; }
    public int? _fire_res { get; set; }
    public int? _cold_res { get; set; }
    public int? _earth_res { get; set; }
    public int? _dark_res { get; set; }
    public int? _poison_res { get; set; }
    public MonsterAttackType? _attackType { get; set; }
    public MagicType? _magic1 { get; set; }
    public MagicType? _magic2 { get; set; }
    public string _comment { get; set; }

}

public enum MonsterAttackType
{
    POISON,
    DARK,
    FIRE,
    COLD,
    EARTH,
    NONE,
}

public enum MagicType
{
    FIRE1,
    FIRE2,
    FIRE3,
    FIRE4,
    FIRE5,
    FIRE6,
    FIRE7,
    COLD1,
    COLD2,
    COLD3,
    // COLD4 해당 이미지를 사용 안해서 제거
    COLD5,
    COLD6,
    COLD7,
    COLD8,
    POISON1,
    POISON2,
    POISON3,
    POISON4,
    POISON5,
    POISON6,
    EARTH1,
    EARTH2,
    EARTH3,
    EARTH4,
    EARTH5,
    NEC1,
    NEC2,
    NEC3,
    NEC4,
    NEC5,
    NEC6,
    SUM1,
    SUM2,
    SUM3,
    SUM4,
    SUM5,
    SUM6,
    NONE,
}
