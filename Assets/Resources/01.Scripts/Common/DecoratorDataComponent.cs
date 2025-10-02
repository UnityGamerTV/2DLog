using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecoratorDataComponent : MonoBehaviour, IDecorator
{
    public int? _max_hp { get { return maxHp; } set { maxHp = CheckNullValue(value); } }
    [SerializeField] protected int maxHp;
    public int? _max_mp { get { return maxMp; } set { maxMp = CheckNullValue(value); } }
    [SerializeField] protected int maxMp;
    public int? _min_attack { get { return minAttack; } set { minAttack = CheckNullValue(value); } }
    [SerializeField] protected int minAttack;
    public int? _max_attack { get { return maxAttack; } set { maxAttack = CheckNullValue(value); } }
    [SerializeField] protected int maxAttack;
    public int? _defence { get { return defence; } set { defence = CheckNullValue(value); } }
    [SerializeField] protected int defence;
    public int? _min_magic_attack { get { return minMagicAttack; } set { minMagicAttack = CheckNullValue(value); } }
    [SerializeField] protected int minMagicAttack;
    public int? _max_magic_attack { get { return maxMagicAttack; } set { maxMagicAttack = CheckNullValue(value); } }
    [SerializeField] protected int maxMagicAttack;
    public int? _fire_res { get { return fireRes; } set { fireRes = CheckNullValue(value); } }
    [SerializeField] protected int fireRes;
    public int? _cold_res { get { return coldRes; } set { coldRes = CheckNullValue(value); } }
    [SerializeField] protected int coldRes;
    public int? _earth_res { get { return earthRes; } set { earthRes = CheckNullValue(value); } }
    [SerializeField] protected int earthRes;
    public int? _dark_res { get { return darkRes; } set { darkRes = CheckNullValue(value); } }
    [SerializeField] protected int darkRes;
    public int? _poison_res { get { return poisonRes; } set { poisonRes = CheckNullValue(value); } }
    [SerializeField] protected int poisonRes;
    public int? _avoid { get { return avoid; } set { avoid = CheckNullValue(value); } }
    [SerializeField] protected int avoid;

    protected T CheckNullValue<T>(T? data) where T : struct
    {
        return data ?? default;
    }

    public abstract void Operation();
    public abstract void Revert();
}
