using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 모든 매니저 초기화 역할
/// </summary>
/// 
public class Manager : MonoBehaviour
{
    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        DataManager.Instance.Init();
        ObjectPoolManager.Instance.Init();
        ResourceManager.Instance.Init();
        TestManager.Instance.Init();
        FieldManager.Instance.Init();
        FactoryManager.Instance.Init();
        MapManager.Instance.Init();
        CameraManager.Instance.Init();
        EventManager.Instance.Init();
        UIManager.Instance.Init();
    }

    public static void Release()
    {

    }
}
