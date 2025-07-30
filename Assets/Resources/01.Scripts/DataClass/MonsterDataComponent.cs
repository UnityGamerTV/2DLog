using UnityEngine;
using System;

public class MonsterDataComponent : MonoBehaviour
{
    public string _name { get { return monsterName; } set { monsterName = value; } }
    [SerializeField] private string monsterName;
    public int _current_hp { get { return currentHp; } set { currentHp = value; } }
    [SerializeField] private int currentHp;
    public int _max_hp { get { return maxHp; } set { maxHp = value; } }
    [SerializeField] private int maxHp;
    public int _min_attack { get { return minAttack; } set { minAttack = value; } }
    [SerializeField] private int minAttack;
    public int _max_attack { get { return maxAttack; } set { maxAttack = value; } }
    [SerializeField] private int maxAttack;
    public int? _defence { get { return defence; } set { defence = CheckNullValue(value); } }
    [SerializeField] private int defence;
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
    public MonsterAttackType? _attackType { get { return attackType; } set { attackType = CheckMonsterAttackType(value); } }
    [SerializeField] private MonsterAttackType attackType;
    public MagicType? _magic1 { get { return magic1; } set { magic1 = CheckMagicType(value); } }
    [SerializeField] private MagicType magic1;
    public MagicType? _magic2 { get { return magic2; } set { magic2 = CheckMagicType(value); } }
    [SerializeField] private MagicType magic2;
    public string _comment { get { return comment; } set { comment = value; } }
    [SerializeField] private string comment;

    T CheckNullValue<T>(T? data) where T : struct
    {
        return data ?? default;
    }

    MonsterAttackType CheckMonsterAttackType(MonsterAttackType? data)
    {
        return data ?? MonsterAttackType.NONE;
    }

    MagicType CheckMagicType(MagicType? data)
    {
        return data ?? MagicType.NONE;
    }
}
