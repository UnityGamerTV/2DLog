using UnityEngine;

/// <summary>
/// 팩토리를 관리하는 매니저 
/// </summary>
public class FactoryManager : Singleton<FactoryManager>, IManager
{
    [Singleton(typeof(FieldManager))] private FieldManager fieldManager;

    public FactoryBase _itemFactory { get { return itemFactory; } set { itemFactory = value; } }
    [SerializeField] private FactoryBase itemFactory;
    public FactoryBase _monsterFactory { get { return monsterFactory; } set { monsterFactory = value; } }
    [SerializeField] private FactoryBase monsterFactory;
    public FactoryBase _playerFactory { get { return playerFactory; } set { playerFactory = value; } }
    [SerializeField] private FactoryBase playerFactory;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        // TODO 수가 늘어나면 루프처리
        itemFactory = new ItemFactory();
        itemFactory.Init();

        monsterFactory = new MonsterFactory();
        monsterFactory.Init();

        playerFactory = new PlayerFactory();
        playerFactory.Init();

        // 의존성 주입
        fieldManager._itemFactory = _itemFactory;
        fieldManager._monsterFactory = monsterFactory;
        fieldManager._playerFactory = playerFactory;
    }

    public void Release()
    {
        
    }
}
