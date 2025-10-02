using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene_Ability_InvenModel : MonoBehaviour
{
    public AbilityType _abilityType { get { return abilityType; } set { abilityType = value; } }
    [SerializeField] private AbilityType abilityType;
    public bool _onDetail {  get { return onDetail; } set { onDetail = value; } }
    [SerializeField] private bool onDetail;

    public void Init()
    {

    }

    public void Release()
    {

    }
}
