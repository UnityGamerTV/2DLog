using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 모든 씬 시작 시 부착시킬 것
/// </summary>
public class InitComponent : MonoBehaviour
{
    [SerializeField] private Manager manager;
    public void Awake()
    {
        if (manager == null)
        {
            GameObject go = new();
            manager = go.AddComponent<Manager>();
        }
    }
}
