using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    public int _orderInLayerCount { get { return orderInLayerCount; } set { orderInLayerCount = value; } }
    [SerializeField] protected int orderInLayerCount;
}
