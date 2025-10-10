using UnityEngine;

public class MagicDataComponent : DecoratorDataComponent
{
    protected DecoratorDataComponent decoData;

    public DecoratorDataComponent Set(DecoratorDataComponent decoData)
    {
        this.decoData = decoData;
        return this;
    }

    public int _id { get { return no; } set { no = value; } }
    [SerializeField] private int no;
    public string _name { get { return magicName; } set { magicName = value; } }
    [SerializeField] private string magicName;
    public int? _base_damage { get { return baseDamage; } set { baseDamage = CheckNullValue(value); } }
    [SerializeField] private int baseDamage;
    public int? _cast_range { get { return castRange; } set { castRange = CheckNullValue(value); } }
    [SerializeField] private int castRange;
    public EffectRadius? _effect_radius { get { return effectRadius; } set { effectRadius = CheckEffectRadius(value); } }
    [SerializeField] private EffectRadius effectRadius;
    public MagicCastType? _magic_cast_type { get { return magicCastType; } set { magicCastType = CheckMagicCastType(value); } }
    [SerializeField] private MagicCastType magicCastType;
    public int? _mp_consume { get { return mpConsume; } set { mpConsume = CheckNullValue(value); } }
    [SerializeField] private int mpConsume;
    public int? _required_skill_level { get { return requiredSkillLevel; } set { requiredSkillLevel = CheckNullValue(value); } }
    [SerializeField] private int requiredSkillLevel;
    public ElementType? _element_type { get { return elementType; } set { elementType = CheckElementType(value); } }
    [SerializeField] private ElementType elementType;
    public int? _turn { get { return turn; } set { turn = CheckNullValue(value); } }
    [SerializeField] private int turn;
    public StatusEffectType? _status_effect_type { get { return statusEffectType; } set { statusEffectType = CheckStatusEffectType(value); } }
    [SerializeField] private StatusEffectType statusEffectType;
    public string _nickname { get { return nickname; } set { nickname = value; } }
    [SerializeField] private string nickname;
    public string _icon { get { return icon; } set { icon = value; } }
    [SerializeField] private string icon;
    public string _comment { get { return comment; } set { comment = value; } }
    [SerializeField] private string comment;

    EffectRadius CheckEffectRadius(EffectRadius? data)
    {
        return data ?? EffectRadius.NONE;
    }

    MagicCastType CheckMagicCastType(MagicCastType? data)
    {
        return data ?? MagicCastType.NONE;
    }

    ElementType CheckElementType(ElementType? data)
    {
        return data ?? ElementType.NONE;
    }

    StatusEffectType CheckStatusEffectType(StatusEffectType? data)
    {
        return data ?? StatusEffectType.NONE;
    }

    public override void Operation()
    {
        decoData.Operation();
        decoData._current_hp += currentHp;
        decoData._current_mp += currentMp;
        decoData._max_hp += maxHp;
        decoData._max_mp += maxMp;
        decoData._min_attack += minAttack;
        decoData._max_attack += maxAttack;
        decoData._defense += defense;
        decoData._min_magic_attack += minMagicAttack;
        decoData._max_magic_attack += minMagicAttack;
        decoData._fire_resist += fireResist;
        decoData._cold_resist += coldResist;
        decoData._earth_resist += earthResist;
        decoData._dark_resist += darkResist;
        decoData._poison_resist += poisonResist;
        decoData._evasion += evasion;
    }

    public override void Revert()
    {
        decoData.Revert();
        decoData._current_hp -= currentHp;
        decoData._current_mp -= currentMp;
        decoData._max_hp -= maxHp;
        decoData._max_mp -= maxMp;
        decoData._min_attack -= minAttack;
        decoData._max_attack -= maxAttack;
        decoData._defense -= defense;
        decoData._min_magic_attack -= minMagicAttack;
        decoData._max_magic_attack -= minMagicAttack;
        decoData._fire_resist -= fireResist;
        decoData._cold_resist -= coldResist;
        decoData._earth_resist -= earthResist;
        decoData._dark_resist -= darkResist;
        decoData._poison_resist -= poisonResist;
        decoData._evasion -= evasion;
    }
}

