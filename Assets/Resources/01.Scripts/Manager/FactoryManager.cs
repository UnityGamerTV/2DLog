using UnityEngine;

/// <summary>
/// 팩토리를 관리하는 매니저 
/// </summary>
public class FactoryManager : Singleton<FactoryManager>, IManager
{
    [SerializeField] private FactoryBase itemFactory;
    [SerializeField] private FactoryBase monsterFactory;

    public void Init()
    {
        // TODO 수가 늘어나면 루프처리
        itemFactory = new ItemFactory();
        itemFactory.Init();
    }

    public void Release()
    {
        
    }
}
