using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : MonoBehaviour { }

public abstract class ParticleController : ParticleBase
{
    public int _orderInLayerCount { get { return orderInLayerCount; } set { orderInLayerCount = value; } }
    [SerializeField] protected int orderInLayerCount;

    public ParticleDataComponent _particleDataComponent { get { return particleDataComponent; } set { particleDataComponent = value; } }
    [SerializeField] private ParticleDataComponent particleDataComponent;

    public abstract void Init();

    public abstract void Play();

    public abstract void Stop();

    public abstract void Release();
}
