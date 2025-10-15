using UnityEngine;
using static Define;
public class ScrollDataComponent : BaseDataComponent<ScrollData>
{
    [SerializeField] private ScrollData inspectorData;
    public int _count { get => count; set => count = value; }
    [SerializeField] private int count;
    public override void Init(ScrollData data)
    {
        base.Init(data);
        inspectorData = data;

    }
}
