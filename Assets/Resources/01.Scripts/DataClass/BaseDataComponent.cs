using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDataComponent : MonoBehaviour
{
    // 공통적으로 접근 가능한 최소 기능 정의 당장은 필요 없을 듯?
    // public abstract IBaseData GetData();
}

public class BaseDataComponent<T> : BaseDataComponent where T : BaseData
{
    public T _data { get => data; set => data = value; }
    [SerializeField] private T data;

    public virtual void Init(T data) => this.data = data;
}
