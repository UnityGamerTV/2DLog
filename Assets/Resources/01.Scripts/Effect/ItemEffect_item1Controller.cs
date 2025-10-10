using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using static UnityEngine.ParticleSystem;


public class ItemEffect_item1Controller : ParticleController
{
    [Singleton(typeof(ObjectPoolManager))] private ObjectPoolManager objectPoolManager;
    [FindComponents("ItemEffect_item1"), SerializeField] protected ParticleSystem particle;

    public override void Init()
    {
        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);
    }

    public override void Play() => PlayParticle().Forget();

    public async UniTaskVoid PlayParticle()
    {
        var token = this.GetCancellationTokenOnDestroy();
        particle.Play();
        await UniTask.WaitUntil(() => !particle.IsAlive(true), PlayerLoopTiming.Update, token);
        objectPoolManager.AddObj(gameObject); // 오브젝트풀에서 활성화, 하이어라키 위치 관리함
    }

    public override void Stop() => particle.Stop();

    public override void Release()
    {
        
    }
}
