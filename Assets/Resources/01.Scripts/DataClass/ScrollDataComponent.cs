using UnityEngine;
public class ScrollDataComponent : BaseDataComponent
{
    public int _no { get { return no; } set { no = value; } }
    [SerializeField] private int no;
    public string _name { get { return scrollName; } set { scrollName = value; } }
    [SerializeField] private string scrollName;
    public int? _avoid { get { return avoid; } set { avoid = CheckNullValue(value); } }
    [SerializeField] private int avoid;
    public int? _enhance { get { return enhance; } set { enhance = CheckNullValue(value); } }
    [SerializeField] private int enhance;
    public int? _turn { get { return turn; } set { turn = CheckNullValue(value); } }
    [SerializeField] private int turn;
    public string _nickName { get { return nickName; } set { nickName = value; } }
    [SerializeField] private string nickName;
    public string _comment { get { return comment; } set { comment = value; } }
    [SerializeField] private string comment;

    T CheckNullValue<T>(T? data) where T : struct
    {
        return data ?? default;
    }
}
