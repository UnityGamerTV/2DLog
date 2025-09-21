using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Bottom_DirView : MonoBehaviour, IListener
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);

        leftButton.onClick.AddListener(OnClickLeftButton);
        //leftButton.onClick.AddListener
    }

    public void OnEvent(EVENT_TYPE event_Type, Component sender, object param = null)
    {
        throw new System.NotImplementedException();
    }

    private void OnClickLeftButton() => eventManager.PostNotification(EVENT_TYPE.PLAYER_LEFT_MOVE, this);

    private void OnClickRightButton() => eventManager.PostNotification(EVENT_TYPE.PLAYER_RIGHT_MOVE, this);


}
