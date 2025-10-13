using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleFactory : ParticleFactoryBase
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(FieldManager))] private FieldManager fieldManager;
    [Singleton(typeof(DataManager))] private DataManager dataManager;
    [Singleton(typeof(ObjectPoolManager))] private ObjectPoolManager objectPoolManager;

    private readonly string PARTICLE_PATH = "Prefabs/Effect";

    // String 사용을 줄이기 위한 캐싱용
    private Dictionary<string, string> particlePathDic;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        particlePathDic = new();
        SetParticlePathDic();
    }

    private void SetParticlePathDic()
    {
        var particles = resourceManager.LoadAll<ParticleSystem>(PARTICLE_PATH);

        foreach (var particle in particles)
        {
            particlePathDic.Add(particle.name, $"{PARTICLE_PATH}/{particle.name}");
        }
        // 공통 파티클 수동보정
        particlePathDic.Add("MagicEffect_nec1", $"{PARTICLE_PATH}/MagicEffect_sum1");
        particlePathDic.Add("MagicEffect_nec3", $"{PARTICLE_PATH}/MagicEffect_sum1");
        particlePathDic.Add("MagicEffect_sum2", $"{PARTICLE_PATH}/MagicEffect_sum1");
        particlePathDic.Add("MagicEffect_sum3", $"{PARTICLE_PATH}/MagicEffect_sum1");
        particlePathDic.Add("MagicEffect_sum4", $"{PARTICLE_PATH}/MagicEffect_sum1");
        particlePathDic.Add("MagicEffect_sum6", $"{PARTICLE_PATH}/MagicEffect_sum5");
    }

    private string GetParticlePathDic(string particleName) => particlePathDic[particleName];

    public ParticleBase CreateParticle(Vector3 worldPos, GameObject parent, ParticleType particleType)
    {
        var particle = GetParticle(particleType);
        SetPosition(particle, worldPos);
        SetParent(particle, parent);
        var particleController = SetData(particle, particleType);
        return particleController;
    }

    private GameObject GetParticle(ParticleType particleType)
    {
        var particleName = particleType.ToString();
        GameObject obj;
        // 오브젝트 풀링 확인
        obj = objectPoolManager.GetObjPool(particleName);
        if (obj == null)
        {
            var particleController = resourceManager.Instantiate<ParticleController>(particlePathDic[particleName]);
            particleController.Init();
            return particleController.gameObject;
        }
        return obj;
    }

    private void SetPosition(GameObject particle, Vector3 worldPos) => particle.transform.position = worldPos;
    private void SetParent(GameObject particle, GameObject parent) => particle.transform.SetParent(parent.transform);
    private ParticleBase GetParticleBase(GameObject particle) => particle.GetComponent<ParticleBase>();

    private string ConvertToParticleType(ParticleType particleType)
    {
        var text = particleType.ToString();
        if (text.LastIndexOf('_') == -1)
            return null;
        if (!char.IsDigit(text[text.Length - 1]))
            return null;

        var parts = text.Split('_');
        return parts[1];
    }

    public ParticleController SetData(GameObject particle, ParticleType particleType)
    {
        var particleName = ConvertToParticleType(particleType);
        var particleNumber = (int)particleType;

        ParticleDataComponent myParticleData = null;

        if (particleNumber < 100) // ParticleType - MagicEffect = 45까지
        {
            ParticleData particleData = dataManager.AddMagicParticleData(particleName);
            myParticleData = particle.AddComponent<ParticleDataComponent>();
            dataManager.CopyParticleData(myParticleData, particleData);
        }
        else if (particleNumber >= 100 && particleNumber < 200) // ParticleType - MeleeEffect
        {
            ParticleData particleData = dataManager.AddMeleeParticleData(particleName);
            myParticleData = particle.AddComponent<ParticleDataComponent>();
            dataManager.CopyParticleData(myParticleData, particleData);
        }
        else // 데이터가 필요없는 이펙트
        { }

        var particleController = particle.GetComponent<ParticleController>();
        if (particleController != null)
            particleController._particleDataComponent = myParticleData;

        return particleController;
    }

    private void SetOrderInLayer(int order)
    {

    }
}
