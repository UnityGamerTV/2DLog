using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Bottom_DirController : UI_Scene, IListener
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [FindComponents("Service"),SerializeField] private UI_Scene_Bottom_DirService service;
    [FindComponents("LButton"),SerializeField] private Button leftButton;
    [FindComponents("RButton"),SerializeField] private Button rightButton;
    [FindComponents("UButton"),SerializeField] private Button upButton;
    [FindComponents("DButton"),SerializeField] private Button downButton;

    public override void Init()
    {
        base.Init();

        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        service.Init();

        leftButton.onClick.AddListener(service.OnClickLeftButton);
        rightButton.onClick.AddListener(service.OnClickRightButton);
        upButton.onClick.AddListener(service.OnClickUpButton);
        downButton.onClick.AddListener(service.OnClickDownButton);

        eventManager.AddListener(EVENT_PLAYER.PLAYER_MOVE_COMPLETE, this);
    }

    public override void Release()
    {
        base.Release();

        leftButton.onClick.RemoveListener(service.OnClickLeftButton);
        rightButton.onClick.RemoveListener(service.OnClickRightButton);
        upButton.onClick.RemoveListener(service.OnClickUpButton);
        downButton.onClick.RemoveListener(service.OnClickDownButton);

        eventManager.RemoveListener(EVENT_PLAYER.PLAYER_MOVE_COMPLETE, this);
    }

    void IListener.OnEvent<TEnum>(TEnum event_Type, Component sender, object param)
    {
        if (event_Type.Equals(EVENT_PLAYER.PLAYER_MOVE_COMPLETE))
        {
            service.OnAllButton();
        }
    }
}
