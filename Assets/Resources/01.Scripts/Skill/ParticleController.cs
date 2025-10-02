using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : ParticleBase
{
    public ParticleDataComponent _particleDataComponent { get {  return particleDataComponent; } set { particleDataComponent = value; } }
    [SerializeField] private ParticleDataComponent particleDataComponent;
}
