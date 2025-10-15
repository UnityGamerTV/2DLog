using UnityEngine;

public class MagicDataComponent : BaseDataComponent<MagicData>
{
    [SerializeField] private MagicData inspectorData;
    public int _count { get => count; set => count = value; }
    [SerializeField] private int count;
    public override void Init(MagicData data)
    {
        base.Init(data);
        inspectorData = data;
    }
}

