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
        // 积己
        var player = resourceManager.Instantiate(PLAYER_PATH);
        var controller = player.GetComponent<PlayerController>();
        controller.Init();
        player.transform.position = worldPos;
        player.transform.SetParent(map.transform);
        // 单捞磐 林涝

        return controller;
    }
}
