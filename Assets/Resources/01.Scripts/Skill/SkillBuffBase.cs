using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public abstract class SkillBuffBase : ParticleBase
{
    // 기본적인 설정 위주로
    // 모든 파티클을 찾아야 됨 이건 각각 구현해야 될 것 같은대?? >>거기서 오더인레이어를 넣어줘야됨

    public abstract void Init();
    public abstract void SetOrderInLayer();
    public abstract void PlayTargetParticle();
    public abstract void StopTargetParticle();
}
