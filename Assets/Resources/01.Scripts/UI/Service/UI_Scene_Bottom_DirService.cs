using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Bottom_DirService : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [FindComponents("LButton"), SerializeField] private Button leftButton;
    [FindComponents("RButton"), SerializeField] private Button rightButton;
    [FindComponents("UButton"), SerializeField] private Button upButton;
    [FindComponents("DButton"), SerializeField] private Button downButton;

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);
    }

    public void OnClickLeftButton() { eventManager.PostNotification(EVENT_PLAYER.PLAYER_LEFT_MOVE, this);OffAllButton(); }

    public void OnClickRightButton() { eventManager.PostNotification(EVENT_PLAYER.PLAYER_RIGHT_MOVE, this); OffAllButton(); }

    public void OnClickUpButton() { eventManager.PostNotification(EVENT_PLAYER.PLAYER_UP_MOVE, this); OffAllButton(); }

    public void OnClickDownButton() { eventManager.PostNotification(EVENT_PLAYER.PLAYER_DOWN_MOVE, this); OffAllButton(); }

    public void OnButton(Button button) => button.interactable = true;

    public void OffButton(Button button) => button.interactable = false;

    public void OnAllButton()
    {
        OnButton(leftButton);
        OnButton(rightButton);
        OnButton(upButton);
        OnButton(downButton);
    }

    public void OffAllButton()
    {
        OffButton(leftButton);
        OffButton(rightButton);
        OffButton(upButton);
        OffButton(downButton);
    }
}
