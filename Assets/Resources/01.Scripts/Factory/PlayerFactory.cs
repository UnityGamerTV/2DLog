using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : FactoryBase
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(DataManager))] private DataManager dataManager;

    private readonly string PLAYER_PATH = "Prefabs/Player/Player";

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
    }

    public FieldObjBase CreateObj(Vector3 worldPos, GameObject map, MapData mapData, int monsterIndex)
    {
        // 생성
        var playerController = resourceManager.Instantiate<PlayerController>(PLAYER_PATH);
        playerController.Init();
        playerController.gameObject.transform.position = worldPos;
        playerController.gameObject.transform.SetParent(map.transform);
        // 데이터 주입

        return playerController;
    }
}
