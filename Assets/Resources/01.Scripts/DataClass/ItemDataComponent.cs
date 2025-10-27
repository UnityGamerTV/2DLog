using UnityEngine;

public class ItemDataComponent : BaseDataComponent<ItemData>
{
    public int _count { get => count; set => count = value; }
    [SerializeField] private int count;
    public int _currentEnhance { get => currentEnhance; set => currentEnhance = value; }
    [SerializeField] private int currentEnhance;
    public override void Init(ItemData data)
    {
        base.Init(data);
    }
}

