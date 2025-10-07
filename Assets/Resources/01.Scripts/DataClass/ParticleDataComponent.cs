using UnityEngine;

public class ParticleDataComponent : Decorator
{
    public string _name { get { return particleName; } set { particleName = value; } }
    [SerializeField] private string particleName;
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
}
