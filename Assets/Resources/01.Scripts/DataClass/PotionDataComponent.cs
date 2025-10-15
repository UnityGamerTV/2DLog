using UnityEngine;
public class PotionDataComponent : BaseDataComponent<PotionData>
{
    [SerializeField] private PotionData inspectorData;
    public int _count { get => count; set => count = value; }
    [SerializeField] private int count;
    public override void Init(PotionData data)
    {
        base.Init(data);
        inspectorData = data;
    }
}
