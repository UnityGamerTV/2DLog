using System;

[Serializable]
public class RandartData
{
    public int _no { get; set; }
    public string _name { get; set; }
    public int? _max_hp { get { return maxHp; } set { maxHp = CheckNullValue(value); } }
    private int maxHp;
    public int? _max_mp { get { return maxMp; } set { maxMp = CheckNullValue(value); } }
    private int maxMp;
    public int? _min_attack { get { return minAttack; } set { minAttack = CheckNullValue(value); } }
    private int minAttack;
    public int? _max_attack { get { return maxAttack; } set { maxAttack = CheckNullValue(value); } }
    private int maxAttack;
    public int? _defence { get { return defence; } set { defence = CheckNullValue(value); } }
    private int defence;
    public int? _min_magic_attack { get { return minMagicAttack; } set { minMagicAttack = CheckNullValue(value); } }
    private int minMagicAttack;
    public int? _max_magic_attack { get { return maxMagicAttack; } set { maxMagicAttack = CheckNullValue(value); } }
    private int maxMagicAttack;
    public int? _fire_res { get { return fireRes; } set { fireRes = CheckNullValue(value); } }
    private int fireRes;
    public int? _cold_res { get { return coldRes; } set { coldRes = CheckNullValue(value); } }
    private int coldRes;
    public int? _earth_res { get { return earthRes; } set { earthRes = CheckNullValue(value); } }
    private int earthRes;
    public int? _dark_res { get { return darkRes; } set { darkRes = CheckNullValue(value); } }
    private int darkRes;
    public int? _poison_res { get { return poisonRes; } set { poisonRes = CheckNullValue(value); } }
    private int poisonRes;
    public int? _avoid { get { return avoid; } set { avoid = CheckNullValue(value); } }
    private int avoid;
    public string _option_name { get; set; }

    T CheckNullValue<T>(T? data) where T : struct
    {
        return data ?? default;
    }
}
