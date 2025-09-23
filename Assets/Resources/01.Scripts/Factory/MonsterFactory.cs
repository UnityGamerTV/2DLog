using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : FactoryBase
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(FieldManager))] private FieldManager fieldManager;
    [Singleton(typeof(DataManager))] private DataManager dataManager;
    [Singleton(typeof(ObjectPoolManager))] private ObjectPoolManager objectPoolManager;
#if UNITY_EDITOR
    [Singleton(typeof(TestManager))] private TestManager testManager;
#endif
    private readonly string MONSTER_PATH = "Prefabs/Monster/Monster";
    private readonly string MONSTER_SPRITE_PATH = "Sprite/Monster/Monster";

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

    public FieldObjBase CreateObj(Vector3 worldPos, GameObject map, MapData mapData, int monsterIndex)
    {
        // 생성
        var monster = GetMonster();
        // 기본 설정
        SetPosition(monster, worldPos);
        SetName(monster, mapData, monsterIndex);
        SetParent(monster, map);
        SetAnimator(monster, mapData, monsterIndex);
        var monsterName = SetSprite(monster, mapData, monsterIndex);
        // 데이터 입력
        var monsterController = SetData(monster, monsterName);
        return monsterController;
    }

    private GameObject GetMonster()
    {
        GameObject obj;
        // 오브젝트 풀링 확인
        obj = objectPoolManager.GetObjPool("Monster");
        if (obj == null)
            obj = resourceManager.Instantiate(MONSTER_PATH);

        return obj;
    }

    private void SetPosition(GameObject monster, Vector3 worldPos) => monster.transform.position = worldPos;
    private void SetName(GameObject monster, MapData mapData, int monsterIndex) => monster.name = mapData.monsterList[monsterIndex];
    private void SetParent(GameObject monster, GameObject map) => monster.transform.SetParent(map.transform);

    private void SetAnimator(GameObject monster, MapData mapData, int monsterIndex)
    {
        Animator animator = monster.GetComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(GetMonsterPathDic(mapData.monsterList[monsterIndex]));
#if UNITY_EDITOR
        testManager._animators.Add(animator);
#endif
    }

    private string SetSprite(GameObject monster, MapData mapData, int monsterIndex)
    {
        SpriteRenderer spriteRenderer = monster.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Array.Find(monsterSprites, sprite => sprite.name.Equals(mapData.monsterList[monsterIndex]));
        return spriteRenderer.sprite.name;
    }

    private MonsterController SetData(GameObject monster, string spriteName)
    {
        var monsterController = monster.GetComponent<MonsterController>();
        MonsterData monsterData = dataManager.AddMonsterData(spriteName);
        var myMonsterData = monster.AddComponent<MonsterDataComponent>();
        dataManager.CopyMonsterData(myMonsterData, monsterData);
        return monsterController;
    }
}
