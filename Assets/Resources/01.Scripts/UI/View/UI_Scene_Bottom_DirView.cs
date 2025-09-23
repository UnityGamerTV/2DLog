using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Bottom_DirView : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        //leftButton.onClick.AddListener(OnClickLeftButton);
        //rightButton.onClick.AddListener(OnClickRightButton);
        //upButton.onClick.AddListener(OnClickUpButton);
        //downButton.onClick.AddListener(OnClickDownButton);
    }

    //private void OnClickLeftButton() => eventManager.PostNotification(EVENT_PLAYER.PLAYER_LEFT_MOVE, this);

    //private void OnClickRightButton() => eventManager.PostNotification(EVENT_PLAYER.PLAYER_RIGHT_MOVE, this);

    //private void OnClickUpButton() => eventManager.PostNotification(EVENT_PLAYER.PLAYER_UP_MOVE, this);

    //private void OnClickDownButton() => eventManager.PostNotification(EVENT_PLAYER.PLAYER_DOWN_MOVE, this);
}
