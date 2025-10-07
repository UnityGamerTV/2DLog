using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// TODO 추후 시간되면 string 을 enum으로 교체
public class ObjectPoolManager : Singleton<ObjectPoolManager>, IManager
{
    [SerializeField] private List<GameObject> objPool;
    [SerializeField] private GameObject root;

    public void Init()
    {
        objPool = new();
        root = new() { name = "@Obj_Root" };
    }

    public void AddObj(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(root.transform);
        objPool.Add(obj);
    }

    public GameObject GetObjPool(string objName)
    {
        for (int i = 0; i < objPool.Count; i++)
        {
            if (string.Equals(objPool[i].name, objName))
            {
                objPool[i].gameObject.SetActive(true);
                return objPool[i];
            }
        }
        return null;
    }

    public void Release()
    {
        
    }
}
