using UnityEngine;

public class ParticleDataComponent : Decorator
{
    public string _name { get { return particleName; } set { particleName = value; } }
    [SerializeField] private string particleName;
    public int? _base_damage { get { return baseDamage; } set { baseDamage = CheckNullValue(value); } }
    [SerializeField] private int baseDamage;
    public int? _reach { get { return reach; } set { reach = CheckNullValue(value); } }
    [SerializeField] private int reach;
    public int? _range { get { return range; } set { range = CheckNullValue(value); } }
    [SerializeField] private int range;
    public MagicAttackType? _magic_attack_type { get { return magicAttackType; } set { magicAttackType = CheckMagicAttackType(value); } }
    [SerializeField] private MagicAttackType magicAttackType;
    public int? _mp_consume { get { return mpConsume; } set { mpConsume = CheckNullValue(value); } }
    [SerializeField] private int mpConsume;
    public int? _skill_limit { get { return skillLimit; } set { skillLimit = CheckNullValue(value); } }
    [SerializeField] private int skillLimit;
    public MagicPropertyType? _magic_property_type { get { return magicPropertyType; } set { magicPropertyType = CheckMagicPropertyType(value); } }
    [SerializeField] private MagicPropertyType magicPropertyType;
    public int? _turn { get { return turn; } set { turn = CheckNullValue(value); } }
    [SerializeField] private int turn;
    public MagicEffectType? _magic_effect_type { get { return magicEffectType; } set { magicEffectType = CheckMagicEffectType(value); } }
    [SerializeField] private MagicEffectType magicEffectType;
    public string _nickName { get { return nickName; } set { nickName = value; } }
    [SerializeField] private string nickName;
    public string _icon { get { return icon; } set { icon = value; } }
    [SerializeField] private string icon;
    public string _comment { get { return comment; } set { comment = value; } }
    [SerializeField] private string comment;

    MagicAttackType CheckMagicAttackType(MagicAttackType? data)
    {
        return data ?? MagicAttackType.NONE;
    }

    MagicPropertyType CheckMagicPropertyType(MagicPropertyType? data)
    {
        return data ?? MagicPropertyType.NONE;
    }

    MagicEffectType CheckMagicEffectType(MagicEffectType? data)
    {
        return data ?? MagicEffectType.NONE;
    }
}
