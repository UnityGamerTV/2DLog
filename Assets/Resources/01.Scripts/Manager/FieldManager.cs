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

    public Dictionary<Vector2Int, MonsterController> _monsterDic { get { return monsterDic; } set { monsterDic = value; } }
    [SerializeField] private Dictionary<Vector2Int, MonsterController> monsterDic;

    public Dictionary<Vector2Int, ItemController> _itemDic { get { return itemDic; } set { itemDic = value; } }
    [SerializeField] private Dictionary<Vector2Int, ItemController> itemDic;

    // 필드에서 획득 할 아이템
    public ItemController _getItemController { get { return getItemController; } set { getItemController = value; } }
    [SerializeField] private ItemController getItemController;
    //
    public FactoryBase _itemFactory { set { itemFactory = value; } }
    private FactoryBase itemFactory;
    public FactoryBase _monsterFactory { set { monsterFactory = value; } }
    private FactoryBase monsterFactory;
    public FactoryBase _playerFactory { set { playerFactory = value; } }
    private FactoryBase playerFactory;
    public ParticleFactoryBase _particleFactory { set { particleFactory = value; } }
    private ParticleFactoryBase particleFactory;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        monsterDic = new();
        itemDic = new();
    }

    public ItemController CreateItem(Vector3 worldPos, GameObject map, MapData mapData, int itemIndex)
    {
        var item = itemFactory.CreateObj(worldPos, map, mapData, itemIndex);
            return(ItemController)item;
    }

    public MonsterController CreateMonster(Vector3 worldPos, GameObject map, MapData mapData, int monsterIndex)
    {
        var monster = monsterFactory.CreateObj(worldPos, map, mapData, monsterIndex);
        return (MonsterController)monster;
    }

    public void CreatePlayer(Vector3 worldPos, GameObject map, MapData mapData = null, int index = 0)
    {
        var player = playerFactory.CreateObj(worldPos, map, mapData, index);
        playerController = (PlayerController)player;

#if UNITY_EDITOR
        testManager._playerController = playerController;
#endif
    }

    /// <summary>
    /// 파티클 생성 메서드 / 생성만 하고 관리는 오브젝트풀에서...
    /// </summary>
    /// <param name="worldPos">파티클 생성 위치</param>
    /// <param name="parent">부모 게임오브젝트</param>
    /// <param name="particleType">파티클 종류</param>
    /// <returns></returns>
    public ParticleController CreateParticle(Vector3 worldPos, GameObject parent, ParticleType particleType)
    {
        var particleController = particleFactory.CreateParticle(worldPos, parent, particleType);
        return (ParticleController)particleController;
    }

    public ItemController TakeItemAt(Vector2Int indexPos)
    {
        ItemController itemController = null;
        if (!itemDic.TryGetValue(indexPos, out itemController))
            return null;

        itemDic.Remove(indexPos);
        return itemController;
    }

    public void Release()
    {
        
    }
}
