using UnityEngine;

public class MagicDataComponent : MonoBehaviour
{
    public int _no { get { return no; } set { no = value; } }
    [SerializeField] private int no;
    public string _name { get { return magicName; } set { magicName = value; } }
    [SerializeField] private string magicName;
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
    public int? _max_hp { get { return maxHp; } set { maxHp = CheckNullValue(value); } }
    [SerializeField] private int maxHp;
    public int? _max_mp { get { return maxMp; } set { maxMp = CheckNullValue(value); } }
    [SerializeField] private int maxMp;
    public int? _min_attack { get { return minAttack; } set { minAttack = CheckNullValue(value); } }
    [SerializeField] private int minAttack;
    public int? _max_attack { get { return maxAttack; } set { maxAttack = CheckNullValue(value); } }
    [SerializeField] private int maxAttack;
    public int? _defence { get { return defence; } set { defence = CheckNullValue(value); } }
    [SerializeField] private int defence;
    public int? _min_magic_attack { get { return minMagicAttack; } set { minMagicAttack= CheckNullValue(value); } }
    [SerializeField] private int minMagicAttack;
    public int? _max_magic_attack { get { return maxMagicAttack; } set { maxMagicAttack = CheckNullValue(value); } }
    [SerializeField] private int maxMagicAttack;
    public int? _fire_res { get { return fireRes; } set { fireRes = CheckNullValue(value); } }
    [SerializeField] private int fireRes;
    public int? _cold_res { get { return coldRes; } set { coldRes = CheckNullValue(value); } }
    [SerializeField] private int coldRes;
    public int? _earth_res { get { return earthRes; } set { earthRes = CheckNullValue(value); } }
    [SerializeField] private int earthRes;
    public int? _dark_res { get { return darkRes; } set { darkRes = CheckNullValue(value); } }
    [SerializeField] private int darkRes;
    public int? _poison_res { get { return poisonRes; } set { poisonRes = CheckNullValue(value); } }
    [SerializeField] private int poisonRes;
    public int? _avoid { get { return avoid; } set { avoid = CheckNullValue(value); } }
    [SerializeField] private int avoid;
    public string _nickName { get { return nickName; } set { nickName = value; } }
    [SerializeField] private string nickName;
    public string _icon { get { return icon; } set { icon = value; } }
    [SerializeField] private string icon;
    public string _comment { get { return comment; } set { comment = value; } }
    [SerializeField] private string comment;

    T CheckNullValue<T>(T? data) where T : struct
    {
        return data ?? default;
    }

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

