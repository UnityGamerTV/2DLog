using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IManager
{
    [Singleton(typeof(CameraManager))] private CameraManager cameraManager;
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;

    [SerializeField] private int order = 300;
    [SerializeField] private GameObject root;
    [SerializeField] private Dictionary<UI_SCENE_ENUM, string> sceneDic; // scenePath dic
    [SerializeField] private Dictionary<UI_POPUP_ENUM, string> popupDic; // popupPaht dic

    private Stack<UI_Popup> popupStack;
    //private Stack<UI_Scene> sceneStack;
    private Dictionary<UI_SCENE_ENUM, UI_Scene> sceneObjDic;
    private Dictionary<UI_POPUP_ENUM, UI_Popup> popupObjDic;

    private readonly string SCENE_UI_PATH = "Prefabs/UI/Scene/";
    private readonly string POPUP_UI_PATH = "Prefabs/UI/Popup/";

    private Dictionary<UI_POPUP_ENUM, UI_Popup> currentPopup;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        root = new() { name = "@UI_Root" };
        sceneDic = new();
        popupDic = new();
        popupStack = new();
        //sceneStack = new();
        sceneObjDic = new();
        popupObjDic = new();
        currentPopup = new();
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
        // 관리중인지 체크 후 반환
        var checkSceneUI = CheckSceneObjDic(ui_enum);
        if (checkSceneUI)
        {
            checkSceneUI.Open();
            return (T)checkSceneUI;
        }

        // 없으면 생성 후 반환
        GameObject go = resourceManager.Instantiate($"{GetUIScenePath(ui_enum)}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        order++;
        sceneObjDic.Add(ui_enum, sceneUI);
        go.transform.SetParent(root.transform);
        SetCanvase(go);
        sceneUI.Init();
        return sceneUI;
    }

    private UI_Scene CheckSceneObjDic(UI_SCENE_ENUM ui_enum)
    {
        UI_Scene objDic = null;

        if (!sceneObjDic.TryGetValue(ui_enum, out objDic))
            return null;

        objDic.gameObject.SetActive(true);
        SetCanvase(objDic.gameObject);
        return objDic;
    }

    /// <summary>
    /// 사용시 T = enum + Controller 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ui_enum"></param>
    /// <returns></returns>
    public T ShowPopupUI<T>(UI_POPUP_ENUM ui_enum) where T : UI_Popup
    {
        // 동일한 팝업이 열려있는지 확인 (아마도 동일한 팝업은 열지 않을 것이라고 생각함)
        var samPopup = IsSamePopup(ui_enum);
        if (samPopup)
            return (T)samPopup;

        // 관리중인지 체크 후 반환
        var popup = CheckPopupObjDic(ui_enum);
        if (popup)
        {
            popup.Open();
            currentPopup.Add(ui_enum, popup);
            return (T)popup;
        }

        // 없으면 생성 후 반환
        GameObject go = resourceManager.Instantiate($"{GetUIPopupPath(ui_enum)}");
        T newPopup = Util.GetOrAddComponent<T>(go);
        order++;
        popupObjDic.Add(ui_enum, newPopup);
        go.transform.SetParent(root.transform);
        newPopup.Init();
        currentPopup.Add(ui_enum, newPopup);
        return newPopup;
    }

    private UI_Popup CheckPopupObjDic(UI_POPUP_ENUM ui_enum)
    {
        UI_Popup objDic = null;

        if (!popupObjDic.TryGetValue(ui_enum, out objDic))
            return null;

        objDic.gameObject.SetActive(true);
        SetCanvase(objDic.gameObject);
        return objDic;
    }

    public void ClosePopupUI(UI_POPUP_ENUM ui_enum)
    {
        var popup = CheckPopupObjDic(ui_enum);
        if (popup != null)
        {
            popup.Close();
            currentPopup.Remove(ui_enum);
            popup.gameObject.SetActive(false);
        }
    }

    public void CloseSceneUI(UI_SCENE_ENUM uiSceneEnum)
    {
        var checkSceneUI = CheckSceneObjDic(uiSceneEnum);
        if (checkSceneUI != null)
        {
            checkSceneUI.Close();
            checkSceneUI.gameObject.SetActive(false);
        }
    }

    public void CloseAllPopupUI()
    {
        //while (popupStack.Count > 0)
            //ClosePopupUI();
    }

    public UI_Popup IsSamePopup(UI_POPUP_ENUM ui_enum)
    {
        UI_Popup objDic = null;

        if (!currentPopup.TryGetValue(ui_enum, out objDic))
            return null;

        return objDic;
    }   

    public void Release()
    {
        
    }
}
