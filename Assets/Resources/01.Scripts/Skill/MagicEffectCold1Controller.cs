using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffectCold1Controller : SkillBuffBase
{
    [FindComponents("MagicEffect_cold1"), SerializeField] private ParticleSystem particle;
    [FindComponents("SparkSub", "Sparks", "Circle")] private List<ParticleSystemRenderer> renderers;

    public override void Init()
    {
        InjectUtil.InjectComponents(this);
    }

    public override void SetOrderInLayer()
    {
        for (int i = 0; i < renderers.Count; i++)
            renderers[i].sortingOrder = orderInLayerCount;
    }

    public override void PlayTargetParticle() => particle.Play();

    public override void StopTargetParticle() => particle.Stop();

}
