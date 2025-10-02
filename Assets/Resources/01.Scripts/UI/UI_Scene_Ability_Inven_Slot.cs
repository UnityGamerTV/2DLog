using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scene_Ability_Inven_Slot : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;

    [FindComponents("Model"), SerializeField] private UI_Scene_Ability_InvenModel model;
    [FindComponents("AbilityButton"), SerializeField] private Button button;
    [FindComponents("BlackImage"), SerializeField] private Image blackImage;
    [FindComponents("EnhanceText"), SerializeField] private Text enhanceText;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
        InjectUtil.InjectSingleton(this);

        button.onClick.AddListener(OnButtonClick);
    }

    public void OnDetail()
    {
        blackImage.gameObject.SetActive(true);
        enhanceText.gameObject.SetActive(true);
    }

    public void OffDetail()
    {
        blackImage.gameObject.SetActive(false);
        enhanceText.gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        if (model._onDetail)
            eventManager.PostNotification(EVENT_ABILITY_INVEN_UI.OPEN_ABILITY_DETAIL_UI, this, model._abilityType); // 디테일 창 오픈
        else
            eventManager.PostNotification(EVENT_ABILITY_INVEN_UI.USE_ABILITY, this, model._abilityType); // 어빌리티 사용
    }

    public void Release()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }

}
