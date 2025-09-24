using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : FieldObjBase, IController
{
    [FindComponents("Service"), SerializeField] private MonsterService service;
    [SerializeField] private MonsterDataComponent model;
    
    public void Init()
    {
        InjectUtil.InjectComponents(this);
    }

    public void OnDamaged(AttackType attackType, int damage) => service.OnDamaged(attackType, damage);

    public void Release()
    {
        
    }
}
