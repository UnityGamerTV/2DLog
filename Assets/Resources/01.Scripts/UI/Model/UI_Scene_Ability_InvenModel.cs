using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Ability_InvenModel : MonoBehaviour
{
    [Singleton(typeof(EventManager))] private EventManager eventManager;
    [Singleton(typeof(DataManager))] private DataManager dataManager;

    public List<AbilityType> _abilityList { get { return abilityList; } set { abilityList = value; } } // 이걸 플레이어 Data가 원본을 들고 있고 여기서 구독처리하면 됨
    [SerializeField] private List<AbilityType> abilityList;
    public Action changeAbilityAction { get { return _changeAbilityAction; } set { _changeAbilityAction = value; } } 
    [SerializeField] private Action _changeAbilityAction;
    public bool _onDetail { get { return onDetail; } set { onDetail = value; } }
    [SerializeField] private bool onDetail;

    [SerializeField] private readonly int abilityMaxCount = 8;
    [SerializeField] private Dictionary<AbilityType, MeleeData> meleeDataDic;
    [SerializeField] private Dictionary<AbilityType, MagicData> magicDataDic;
   
    public void Init()
    {
        abilityList = new();
        meleeDataDic = new();
        magicDataDic = new();

        InjectUtil.InjectSingleton(this);
    }

    public void AddAbility(AbilityType newAbility)
    {
        // 8개가 Max
        if (abilityList.Count >= abilityMaxCount)
        {
            // 더 이상 배울 수 없습니다. 팝업 온 (공용 팝업, 이벤트 처리)
            return;
        }
        abilityList.Add(newAbility);
        changeAbilityAction.Invoke(); // View 에서 구독
    }

    public void RemoveAbility(AbilityType removeAbility)
    {
        if (abilityList.Count == 0)
        {
            // 삭제 대상이 없습니다. 팝업 온 (공용 팝업, 이벤트 처리)
            return;
        }
        abilityList.Remove(removeAbility);
        changeAbilityAction.Invoke(); // View 에서 구독
    }

    public MagicData GetMagicData(AbilityType ability)
    {
        MagicData data = null;
        if(!magicDataDic.TryGetValue(ability, out data))
        {
            var newData = dataManager.AddMagicData(ability.ToString());
            magicDataDic.Add(ability, newData);
            return newData;
        }
        return data;
    }

    public MeleeData GetMeleeData(AbilityType ability)
    {
        MeleeData data = null;
        if(!meleeDataDic.TryGetValue(ability, out data))
        {
            var newData = dataManager.AddMeleeData(ability.ToString());
            meleeDataDic.Add(ability, newData);
            return newData;
        }
        return data;
    }

    public void Release()
    {

    }
}
