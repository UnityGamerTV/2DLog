using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 필드 오브젝트 생성, 데이터 주입, 관리
/// </summary>
public class FieldManager : Singleton<FieldManager>, IManager
{
    [Singleton(typeof(ResourceManager))] public ResourceManager resourceManager;
    [Singleton(typeof(DataManager))] public DataManager dataManager;

#if UNITY_EDITOR
    [Singleton(typeof(TestManager))] private TestManager testManager;
#endif

    public PlayerController _playerController { get { return playerController; } set { playerController = value; } }
    [SerializeField] private PlayerController playerController;
    public List<MonsterController> _monsterControllerList { get { return monsterControllerList; } set { monsterControllerList = value; } }
    [SerializeField] private List<MonsterController> monsterControllerList;
    public List<ItemController> _ItemControllerList { get { return itemControllerList; } set { itemControllerList = value; } }
    [SerializeField] private List<ItemController> itemControllerList;

    public FactoryBase _itemFactory { set { itemFactory = value; } }
    private FactoryBase itemFactory;
    public FactoryBase _monsterFactory { set { monsterFactory = value; } }
    private FactoryBase monsterFactory;
    public FactoryBase _playerFactory { set { playerFactory = value; } }
    private FactoryBase playerFactory;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        monsterControllerList = new();
        itemControllerList = new();
    }

    public void CreateItem(Vector3 worldPos, GameObject map, MapData mapData, int itemIndex)
    {
        var item = itemFactory.CreateObj(worldPos, map, mapData, itemIndex);
            itemControllerList.Add((ItemController)item);
    }

    public void CreateMonster(Vector3 worldPos, GameObject map, MapData mapData, int monsterIndex)
    {
        var monster = monsterFactory.CreateObj(worldPos, map, mapData, monsterIndex);
        monsterControllerList.Add((MonsterController)monster);
    }

    public void CreatePlayer(Vector3 worldPos, GameObject map, MapData mapData = null, int index = 0)
    {
        var player = playerFactory.CreateObj(worldPos, map, mapData, index);
        playerController = (PlayerController)player;

#if UNITY_EDITOR
        testManager._playerController = playerController;
#endif
    }


    public void Release()
    {
        
    }
}
