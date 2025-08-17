using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Diagnostics;

public class UIManager : Singleton<UIManager>, IManager
{
    [Singleton(typeof(CameraManager))] private CameraManager cameraManager;
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;

    [SerializeField] private int order = 10;
    [SerializeField] private GameObject root;
    [SerializeField] private Dictionary<UI_SCENE_ENUM, string> sceneDic; // scenePath dic
    [SerializeField] private Dictionary<UI_POPUP_ENUM, string> popupDic; // popupPaht dic

    private Stack<UI_Popup> popupStack;
    private Stack<UI_Scene> sceneStack;

    private readonly string SCENE_UI_PATH = "Prefabs/UI/Scene/";
    private readonly string POPUP_UI_PATH = "Prefabs/UI/Popup/";

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        root = new() { name = "@UI_Root" };
        sceneDic = new();
        popupDic = new();
        popupStack = new();
        sceneStack = new();

        SetDic();
    }


    public void SetCanvase(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = cameraManager._mainCamera;
        canvas.planeDistance = order + 1;
        //overrideSorting = 부모가 어떤값이건 sort오더를 가짐
        canvas.overrideSorting = true;
        if (sort)
        {
            canvas.sortingOrder = order;
            order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    private void SetDic()
    {
        foreach (UI_SCENE_ENUM one in Enum.GetValues(typeof(UI_SCENE_ENUM)))
            sceneDic.Add(one, $"{SCENE_UI_PATH}{one.ToString()}");

        foreach (UI_POPUP_ENUM one in Enum.GetValues (typeof(UI_POPUP_ENUM)))
            popupDic.Add(one, $"{POPUP_UI_PATH}{one.ToString()}");
    }

    private string GetUIScenePath(UI_SCENE_ENUM ui_enum) => sceneDic[ui_enum];
    private string GetUIPopupPath(UI_POPUP_ENUM ui_enum) => popupDic[ui_enum];

    /// <summary>
    /// 사용시 T = enum + Controller 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ui_enum"></param>
    /// <returns></returns>
    public T ShowSceneUI<T>(UI_SCENE_ENUM ui_enum) where T : UI_Scene
    {
        GameObject go = resourceManager.Instantiate($"{GetUIScenePath(ui_enum)}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        order++;
        sceneStack.Push(sceneUI);
        go.transform.SetParent(root.transform);
        SetCanvase(go);
        return sceneUI;
    }

    /// <summary>
    /// 사용시 T = enum + Controller 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ui_enum"></param>
    /// <returns></returns>
    public T ShowPopupUI<T>(UI_POPUP_ENUM ui_enum) where T : UI_Popup
    {
        GameObject go = resourceManager.Instantiate($"{GetUIPopupPath(ui_enum)}");
        T popup = Util.GetOrAddComponent<T>(go);
        order++;
        popupStack.Push(popup);

        go.transform.SetParent(root.transform);

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (popupStack.Count == 0)
            return;
        if (popupStack.Peek() != popup)
        {
            LogUtil.Log("Close Popup Failde!");
        }
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        UI_Popup popup = popupStack.Pop();
        Destroy(popup.gameObject);
        popup = null;
        order--;
    }

    public void CloseSceneUI()
    {
        //if (sceneUI == null)
        //    return;

        //GameManager.Resouce.Destroy(sceneUI.gameObject);
        //sceneUI = null;
        //order--;
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }


    public void Release()
    {
        
    }
}
