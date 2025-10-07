using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorDataComponent : BaseDataComponent, IDecorator
{
    public int? _current_hp { get { return currentHp; } set { currentHp = CheckNullValue(value); } }
    [SerializeField] protected int currentHp;
    public int? _current_mp { get { return currentMp; } set { currentMp = CheckNullValue(value); } }
    [SerializeField] protected int currentMp;
    public int? _max_hp { get { return maxHp; } set { maxHp = CheckNullValue(value); } }
    [SerializeField] protected int maxHp;
    public int? _max_mp { get { return maxMp; } set { maxMp = CheckNullValue(value); } }
    [SerializeField] protected int maxMp;
    public int? _min_attack { get { return minAttack; } set { minAttack = CheckNullValue(value); } }
    [SerializeField] protected int minAttack;
    public int? _max_attack { get { return maxAttack; } set { maxAttack = CheckNullValue(value); } }
    [SerializeField] protected int maxAttack;
    public int? _defense { get { return defense; } set { defense = CheckNullValue(value); } }
    [SerializeField] protected int defense;
    public int? _min_magic_attack { get { return minMagicAttack; } set { minMagicAttack = CheckNullValue(value); } }
    [SerializeField] protected int minMagicAttack;
    public int? _max_magic_attack { get { return maxMagicAttack; } set { maxMagicAttack = CheckNullValue(value); } }
    [SerializeField] protected int maxMagicAttack;
    public int? _fire_resist { get { return fireResist; } set { fireResist = CheckNullValue(value); } }
    [SerializeField] protected int fireResist;
    public int? _cold_resist { get { return coldResist; } set { coldResist = CheckNullValue(value); } }
    [SerializeField] protected int coldResist;
    public int? _earth_resist { get { return earthResist; } set { earthResist = CheckNullValue(value); } }
    [SerializeField] protected int earthResist;
    public int? _dark_resist { get { return darkResist; } set { darkResist = CheckNullValue(value); } }
    [SerializeField] protected int darkResist;
    public int? _poison_resist { get { return poisonResist; } set { poisonResist = CheckNullValue(value); } }
    [SerializeField] protected int poisonResist;
    public int? _evasion { get { return evasion; } set { evasion = CheckNullValue(value); } }
    [SerializeField] protected int evasion
;

    protected T CheckNullValue<T>(T? data) where T : struct
    {
        return data ?? default;
    }

    public abstract void Operation();
    public abstract void Revert();
}
