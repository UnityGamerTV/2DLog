using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterService : MonoBehaviour
{
    [FindComponents("Model"), SerializeField] private MonsterDataComponent model;
    [FindComponents("View"), SerializeField] private MonsterView view;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
    }

    public void OnDamaged(AttackType attackType, int damage) => model.OnDamaged(attackType, damage);


    //public void SetFilpXSprite(bool isFilpX) => spriteRenderer.flipX = isFilpX;
}
