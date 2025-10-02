using UnityEngine;

public interface ParticleFactoryBase
{
    void Init();

    ParticleBase CreateParticle(Vector3 worldPos, GameObject map, ParticleType particleType);
}
