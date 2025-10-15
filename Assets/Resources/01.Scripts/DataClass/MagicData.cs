using System;
using UnityEngine;

[Serializable]
public class MagicData : BaseData, INickname //BaseAbilityData
{
    public int _id { get => id; set => id = value; }
    [SerializeField] private int id;
    public string _name { get => itemName; set => itemName = value; }
    [SerializeField] private string itemName;
    public int? _base_damage { get => baseDamage; set => baseDamage = CheckNullValue(value); }
    [SerializeField] private int baseDamage;
    public int? _cast_range { get => castRange; set => castRange = CheckNullValue(value); }
    [SerializeField] private int castRange;
    public EffectRadius? _effect_radius { get => effectRadius; set => effectRadius = CheckNullValue(value); }
    [SerializeField] private EffectRadius effectRadius;
    public MagicCastType? _magic_cast_type { get => magicCast; set => magicCast = CheckNullValue(value); }
    [SerializeField] private MagicCastType magicCast;
    public int? _mp_consume { get => mpConsume; set => mpConsume = CheckNullValue(value); }
    [SerializeField] private int mpConsume;
    public int? _required_skill_level { get => requiredSkillLevel; set => requiredSkillLevel = CheckNullValue(value); }
    [SerializeField] private int requiredSkillLevel;
    public ElementType? _element_type { get => elementType; set => elementType = CheckNullValue(value); }
    [SerializeField] private ElementType elementType;
    public int? _turn { get => turn; set => turn = CheckNullValue(value); }
    [SerializeField] private int turn;
    public StatusEffectType? _status_effect_type { get => statusEffectType; set => statusEffectType = CheckNullValue(value); }
    [SerializeField] private StatusEffectType statusEffectType;
    public int? _max_hp { get => maxHp; set => maxHp = CheckNullValue(value); }
    [SerializeField] private int maxHp;
    public int? _max_mp { get => maxMp; set => maxMp = CheckNullValue(value); }
    [SerializeField] private int maxMp;
    public int? _min_attack { get => minAttack; set => minAttack = CheckNullValue(value); }
    [SerializeField] private int minAttack;
    public int? _max_attack { get => maxAttack; set => maxAttack = CheckNullValue(value); }
    [SerializeField] private int maxAttack;
    public int? _defense { get => defense; set => defense = CheckNullValue(value); }
    [SerializeField] private int defense;
    public int? _min_magic_attack { get => minMagicAttack; set => minMagicAttack = CheckNullValue(value); }
    [SerializeField] private int minMagicAttack;
    public int? _max_magic_attack { get => maxMagicAttack; set => maxMagicAttack = CheckNullValue(value); }
    [SerializeField] private int maxMagicAttack;
    public int? _fire_resist { get => fireResist; set => fireResist = CheckNullValue(value); }
    [SerializeField] private int fireResist;
    public int? _cold_resist { get => coldResist; set => coldResist = CheckNullValue(value); }
    [SerializeField] private int coldResist;
    public int? _earth_resist { get => earthResist; set => earthResist = CheckNullValue(value); }
    [SerializeField] private int earthResist;
    public int? _dark_resist { get => darkResist; set => darkResist = CheckNullValue(value); }
    [SerializeField] private int darkResist;
    public int? _poison_resist { get => poisonResist; set => poisonResist = CheckNullValue(value); }
    [SerializeField] private  int poisonResist;
    public int? _evasion { get => evasion; set => evasion = CheckNullValue(value); }
    [SerializeField] private int evasion;
    public ItemGrade? _item_grade { get => itemGrade; set => itemGrade = CheckNullValue(value); }
    [SerializeField] private ItemGrade itemGrade; 
    public DataType? _data_type { get => dataType; set => dataType = CheckNullValue(value); }
    [SerializeField] private DataType dataType;
    public string _nickname { get => nickname; set => nickname = value; }
    [SerializeField] private string nickname;
    public string _icon { get => icon; set => icon = value; }
    [SerializeField] private string icon;
    public string _comment { get => comment; set => comment = value; }
    [SerializeField] private string comment;
}
