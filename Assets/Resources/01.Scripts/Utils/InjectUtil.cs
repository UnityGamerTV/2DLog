using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEditor;

[AttributeUsage(AttributeTargets.Field)]
public class FindComponentsAttribute : Attribute
{
    public string[] gameObjectNames { get; }

    public FindComponentsAttribute(params string[] gameObjectNames)
    {
        this.gameObjectNames = gameObjectNames;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class SingletonAttribute : Attribute
{
    public Type type;

    public SingletonAttribute(Type type)
    {
        this.type = type;
    }
}
public static class InjectUtil
{
    public static void InjectComponents(object o)
    {
        Type type = o.GetType();
        MonoBehaviour script = o as MonoBehaviour;

        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var field in fields)
        {
            var attribute = (FindComponentsAttribute)field.GetCustomAttribute(typeof(FindComponentsAttribute));

            if (attribute == null)
            {
                LogUtil.Log($"필드 '{field.Name}'에 대해 FindComponentAttribute가 없습니다.");
                continue;
            }

            // 필드 타입에 따라 분기
            if (field.FieldType.IsArray)
            {
                InjectArrayField(field, attribute, script);
            }
            else if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
            {
                InjectListField(field, attribute, script);
            }
            else
            {
                InjectSingleComponent(field, attribute, script);
            }
        }
    }

    // 배열 필드 처리
    private static void InjectArrayField(FieldInfo field, FindComponentsAttribute attribute, MonoBehaviour script)
    {
        Type elementType = field.FieldType.GetElementType();
        List<Component> componentsList = GetComponentsFromGameObjects(attribute, elementType, script);

        Array componentArray = Array.CreateInstance(elementType, componentsList.Count);

        for (int i = 0; i < componentsList.Count; i++)
        {
            componentArray.SetValue(componentsList[i], i);
        }

        field.SetValue(script, componentArray);
    }

    // 리스트 필드 처리
    private static void InjectListField(FieldInfo field, FindComponentsAttribute attribute, MonoBehaviour script)
    {
        Type elementType = field.FieldType.GetGenericArguments()[0];
        IList componentsList = (IList)Activator.CreateInstance(field.FieldType);

        foreach (var component in GetComponentsFromGameObjects(attribute, elementType, script))
        {
            componentsList.Add(component);
        }

        field.SetValue(script, componentsList);
    }

    // 단일 컴포넌트 필드 처리
    private static void InjectSingleComponent(FieldInfo field, FindComponentsAttribute attribute, MonoBehaviour script)
    {
        Transform tr;
        // 현재 스크립트가 루트인지 확인
        if (script.transform.CompareTag("PrefabRoot"))
            tr = FindChild(attribute.gameObjectNames[0], script.transform);
        else
            tr = FindInSameRoot(attribute.gameObjectNames[0], script.transform);

        if (tr == null)
        {
            LogUtil.Log($"게임오브젝트 '{attribute.gameObjectNames[0]}'의 Transform을 찾지 못했습니다.");
            return;
        }

        Component component = tr.GetComponent(field.FieldType);

        if (component == null)
        {
            LogUtil.Log($"게임오브젝트 '{attribute.gameObjectNames[0]}'에서 '{field.FieldType}' 컴포넌트를 찾지 못했습니다.");
            return;
        }

        field.SetValue(script, component);
    }

    // 여러 게임 오브젝트에서 컴포넌트를 가져오는 공통 함수
    private static List<Component> GetComponentsFromGameObjects(FindComponentsAttribute attribute, Type componentType, MonoBehaviour script)
    {
        List<Component> componentsList = new List<Component>();

        foreach (string gameObjectName in attribute.gameObjectNames)
        {
            Transform tr;

            // 현재 스크립트가 루트인지 확인
            if (script.transform.CompareTag("PrefabRoot"))
                tr = FindChild(attribute.gameObjectNames[0], script.transform);
            else
                tr = FindInSameRoot(attribute.gameObjectNames[0], script.transform);

            if (tr == null)
            {
                LogUtil.Log($"게임오브젝트 '{gameObjectName}'의 Transform을 찾지 못했습니다.");
                continue;
            }

            Component component = tr.GetComponent(componentType);
            if (component == null)
            {
                LogUtil.Log($"게임오브젝트 '{gameObjectName}'에서 '{componentType}' 컴포넌트를 찾지 못했습니다.");
                continue;
            }

            componentsList.Add(component);
        }

        return componentsList;
    }

    /// <summary>
    /// 같은 루트 그룹 내에서 이름으로 Transform 찾기 (부모/자식 모두 탐색)
    /// </summary>
    public static Transform FindInSameRoot(string name, Transform startTr)
    {
        // 1. 내가 속한 루트 구하기
        Transform findTr = null;
        while (findTr == null)
        {
            if (startTr.CompareTag("PrefabRoot"))
                findTr = startTr;

            startTr = startTr.parent;
        }

        // 2. 루트 기준으로 전체 탐색
        return FindChild(name, findTr);
    }

    /// <summary>
    /// 게임오브젝트의 Transform을 찾는 재귀 함수
    /// </summary>
    /// <param name="name">타겟 게임오브젝트 이름</param>
    /// <param name="tr">찾는 시작 위치 Transform</param>
    /// <returns></returns>
    public static Transform FindChild(string name, Transform tr)
    {
        if (tr.name == name)
            return tr;

        for (int i = 0; i < tr.childCount; i++)
        {
            Transform findTr = FindChild(name, tr.GetChild(i));
            if (findTr != null)
                return findTr;
        }
        return null;
    }

    static Dictionary<string, UnityEngine.Object> singletonDic = new Dictionary<string, UnityEngine.Object>();
    static UnityEngine.Object singleton;


    public static void InjectSingleton(object o)
    {
        Type type = o.GetType();
        FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                                            BindingFlags.NonPublic |
                                            BindingFlags.Instance);

        foreach (var one in fields)
        {
            var attribute = (SingletonAttribute)one.GetCustomAttribute(typeof(SingletonAttribute));

            if (attribute == null)
                continue;

            var singletonType = attribute.type;

            if (!singletonDic.TryGetValue(singletonType.Name, out singleton))
                singleton = UnityEngine.Object.FindObjectOfType(singletonType);

            if (singleton == null)
            {
                var property = singletonType.GetProperty("Instance",
                            BindingFlags.Static |
                            BindingFlags.Public |
                            BindingFlags.FlattenHierarchy |
                            BindingFlags.GetProperty);

                singleton = (UnityEngine.Object)property.GetValue(null, null);
            }

            one.SetValue(o, singleton);

            if (!singletonDic.TryGetValue(singleton.name, out _))
                singletonDic.Add(singleton.name, singleton);
        }

    }
}
