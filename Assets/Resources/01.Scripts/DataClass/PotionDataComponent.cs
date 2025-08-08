using UnityEngine;
public class PotionDataComponent : BaseDataComponent
{
    public int _no { get { return no; } set { no = value; } }
    [SerializeField] private int no;
    public string _name { get { return potionName; } set { potionName = value; } }
    [SerializeField] private string potionName;
    public int? _max_hp { get { return maxHp; } set { maxHp = CheckNullValue(value); } }
    [SerializeField] private int maxHp;
    public int? _current_hp { get { return currentHp; } set { currentHp = CheckNullValue(value); } }
    [SerializeField] private int currentHp;
    public int? _max_mp { get { return maxMp; } set { maxMp = CheckNullValue(value); } }
    [SerializeField] private int maxMp;
    public int? _current_mp { get { return currentMp; } set { currentMp = CheckNullValue(value); } }
    [SerializeField] private int currentMp;
    public int? _min_attack { get { return minAttack; } set { minAttack = CheckNullValue(value); } }
    [SerializeField] private int minAttack;
    public int? _max_attack { get { return maxAttack; } set { maxAttack = CheckNullValue(value); } }
    [SerializeField] private int maxAttack;
    public int? _defence { get { return defence; } set { defence = CheckNullValue(value); } }
    [SerializeField] private int defence;
    public int? _min_magic_attack { get { return minAttack; } set { minAttack = CheckNullValue(value); } }
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
    public int? _turn { get { return turn; } set { turn = CheckNullValue(value); } }
    [SerializeField] private int turn;
    public string _nickName { get { return nickName; } set { nickName = value; } }
    [SerializeField] string nickName;
    public string _comment { get { return comment; } set { comment = value; } }
    [SerializeField] string comment;

    T CheckNullValue<T>(T? data) where T : struct
    {
        return data ?? default;
    }
}
