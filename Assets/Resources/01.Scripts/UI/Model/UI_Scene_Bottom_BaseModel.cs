using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Bottom_BaseModel : MonoBehaviour
{
    public UI_SCENE_ENUM _uiSceneEnum { get { return uiSceneEnum; } set { uiSceneEnum = value; } }
    [SerializeField] private UI_SCENE_ENUM uiSceneEnum;

    public void Init()
    {

    }

    public void Release()
    {

    }
}
