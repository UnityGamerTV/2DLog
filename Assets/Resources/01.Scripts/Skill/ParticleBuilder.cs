using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBuilder
{
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;
    [Singleton(typeof(ObjectPoolManager))] private ObjectPoolManager objectPoolManager;

    private Vector3 position;
    private int orderInLayer;
    private ParticleBase particleBase;

    public ParticleBuilder()
    {
        InjectUtil.InjectSingleton(this);
    }

    public ParticleBuilder SetPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }

    public ParticleBuilder SetOrderInLayer(int orderInLayer)
    {
        this.orderInLayer = orderInLayer; 
        return this;
    }

    public ParticleBuilder SetParticleBase(ParticleBase particleBase)
    {
        this.particleBase = particleBase;
        return this;
    }

    public ParticleBase Build(GameObject particlePrefab)
    {

        return null;
    }
}
