using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>, IManager
{
    [Singleton(typeof(ObjectPoolManager))] private ObjectPoolManager objectPoolManager;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
    }

    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path); 
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        var prefab = Load<GameObject>(path);
        if (prefab == null)
        {
            LogUtil.Log($"리소스 로드 실패 : {path}");
            return null;
        }
        else
        {
            GameObject go = Object.Instantiate(prefab, parent);
            go.name = prefab.name;
            return go;
        }
    }

    public T Instantiate<T>(string path, Transform parent = null) where T : Component
    {
        var prefab = Load<T>(path);
        if (prefab == null)
        {
            LogUtil.Log($"리소스 로드 실패 : {path}");
            return null;
        }
        var go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;

        var component = go.GetComponent<T>();
        if (component == null)
            LogUtil.Log($"Component of type {typeof(T)} not found in prefab: {path}");

        return component;
    }

    public GameObject InstantiateChashingPath(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>(path);
        if (prefab == null)
        {
            LogUtil.Log($"Failed to load prefab : {path}");
            return null;
        }

        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        Object.Destroy(go);
    }

    //몇초후 파괴
    public void Destroy(GameObject go, float time)
    {
        if (go == null)
            return;
        Object.Destroy(go, time);
    }

    public void Release()
    {
        
    }
}
