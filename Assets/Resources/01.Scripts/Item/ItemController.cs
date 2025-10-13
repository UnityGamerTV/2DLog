using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;


public class ItemController : FieldObjBase, IController
{

    public BaseDataComponent _baseDataComponent { get { return baseDataComponent; } set { baseDataComponent = value; } }
    [SerializeField] private BaseDataComponent baseDataComponent;

    public SpriteRenderer _spriteRenderer { get { return spriteRenderer; } set { spriteRenderer = value; } }
    [FindComponents("item"), SerializeField] private SpriteRenderer spriteRenderer;

    private float alpha = 1f;
    private float fadeTime = 1.0f;

    public void Init()
    {
        InjectUtil.InjectComponents(this);
    }

    public void OnPlayerTouch() => OnCollected().Forget();

    private async UniTaskVoid OnCollected()
    {
        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime / fadeTime;
            spriteRenderer.color = new Color(1, 1, 1, alpha);
            await UniTask.Yield();
        }
    }

    public void Release()
    {
        
    }
}
