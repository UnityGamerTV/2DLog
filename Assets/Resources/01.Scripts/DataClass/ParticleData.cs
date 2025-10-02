using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleData : MonoBehaviour
{
    // DecoratorDataComponent
    public int? _max_hp;
    public int? _max_mp;
    public int? _min_attack;
    public int? _max_attack;
    public int? _defence;
    public int? _min_magic_attack;
    public int? _max_magic_attack;
    public int? _fire_res;
    public int? _cold_res;
    public int? _earth_res;
    public int? _dark_res;
    public int? _poison_res;
    public int? _avoid;
    // ParticleDataComponent
    public string _name;
    public int? _base_damage;
    public int? _reach;
    public int? _range;
    public MagicAttackType? _magic_attack_type;
    public int? _mp_consume;
    public int? _skill_limit;
    public MagicPropertyType? _magic_property_type;
    public int? _turn;
    public MagicEffectType? _magic_effect_type;
    public string _nickName;
    public string _icon;
    public string _comment;
}
