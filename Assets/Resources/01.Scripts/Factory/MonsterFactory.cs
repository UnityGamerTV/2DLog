using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : FactoryBase
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(FieldManager))] private FieldManager fieldManager;
    [Singleton(typeof(DataManager))] private DataManager dataManager;
#if UNITY_EDITOR
    [Singleton(typeof(TestManager))] private TestManager testManager;
#endif
    private readonly string MONSTER_SPRITE_PATH = "Sprite/Monster/Monster";
    private readonly string MONSTER_PATH = "Prefabs/Monster/monster";

    private Sprite[] monsterSprites;

    // String 사용을 줄이기 위한 캐싱용
    private Dictionary<string, string> monsterPathDic;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        monsterPathDic = new();

        monsterSprites = resourceManager.LoadAll<Sprite>(MONSTER_SPRITE_PATH);
        SetMonsterPathDic();
    }


    public FieldObjBase CreateObj(Vector3 worldPos, GameObject map, MapData mapData, int monsterIndex)
    {
        // 생성
        var monster = resourceManager.Instantiate(MONSTER_PATH);
        SpriteRenderer spriteRenderer = monster.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Array.Find(monsterSprites, sprite => sprite.name.Equals(mapData.monsterList[monsterIndex]));
        Animator animator = monster.GetComponent<Animator>();
        string spriteName = spriteRenderer.sprite.name;
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(GetMonsterPathDic(mapData.monsterList[monsterIndex]));
        var monsterController = monster.GetComponent<MonsterController>();
#if UNITY_EDITOR
        testManager._animators.Add(animator);
#endif

        // 데이터 입력
        MonsterData monsterData = dataManager.AddMonsterData(spriteName);
        var myMonsterData = monster.AddComponent<MonsterDataComponent>();
        dataManager.CopyMonsterData(myMonsterData, monsterData);
        //
        monster.transform.position = worldPos;
        monster.name = $"{mapData.monsterList[monsterIndex]}{monsterIndex + 1}";
        monster.transform.SetParent(map.transform);
        monsterIndex++;

        return monsterController;
    }

    private void SetMonsterPathDic()
    {
        string CONTROLLER_PATH = "Animations/Monster/Controller";
        var controllers = resourceManager.LoadAll<RuntimeAnimatorController>(CONTROLLER_PATH);

        foreach (var monster in controllers)
        {
            monsterPathDic.Add(monster.name, $"{CONTROLLER_PATH}/{monster.name}");
        }
    }

    private string GetMonsterPathDic(string spriteName) => monsterPathDic[spriteName];
}
