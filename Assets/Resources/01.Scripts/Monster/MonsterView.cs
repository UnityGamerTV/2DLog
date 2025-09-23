using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : MonoBehaviour
{
    [FindComponents("Monster")] private SpriteRenderer spriteRenderer;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
    }

    public void SetFilpXSprite(bool isFilpX) => spriteRenderer.flipX = isFilpX;
    
        
    
}
