using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [FindComponents("Player"), SerializeField] private PlayerController controller;
    [FindComponents("Model"), SerializeField] private PlayerDataComponent model;

    /// <summary>
    /// MapTest 에서 테스트 용도로 사용함
    /// </summary>
    public Animator _bodyAnimator { get => bodyAnimator; }
    [FindComponents("Player"), SerializeField] private Animator bodyAnimator;
    //

    // 보여지는 장착 스프라이트 랜더러
    [FindComponents("Helmet"), SerializeField] private SpriteRenderer helmetSpriteRenderer;
    [FindComponents("Armour"), SerializeField] private SpriteRenderer armourSpriteRenderer;
    [FindComponents("Shield"), SerializeField] private SpriteRenderer shieldSpriteRenderer;
    [FindComponents("Body"), SerializeField] private SpriteRenderer bodySpriteRenderer;

    // 보여지는 장착 스프라이트 Enum 정보
    [SerializeField] private HELMET_TYPE helmetType;
    [SerializeField] private ARMOUR_TYPE armourType;
    [SerializeField] private SHIELD_TYPE shieldType;
    [SerializeField] private PLAYER_STATE playerState;

    // 보여지는 장착 스프라이트 딕셔너리
    [SerializeField] private Dictionary<Enum, Sprite[]> spriteDic = new Dictionary<Enum, Sprite[]>();


    public void Init()
    {
        InjectUtil.InjectComponents(this);

        // 슬라이스한 2D 스프라이트 정보를 딕셔너리에서 관리
        GetResource<HELMET_TYPE>();
        GetResource<ARMOUR_TYPE>();
        GetResource<SHIELD_TYPE>();
        // Model 데이터랑 연결 >> 모델 데이터가 변하면 이벤트로 받아옴 >> 추후 Pull 방식 고민?
        model.helmetEquipAction += GetHelmetData;
        model.armourEquipAction += GetArmourData;
        model.shieldEquipAction += GetShieldData;
    }

    private void GetResource<T>() where T : Enum
    {
        foreach (T one in Enum.GetValues(typeof(T)))
        {
            string result = one.ToString();
            result = result.Replace("_", " ");
            // TODO
            // 추후 리소스 매니저에서 래핑해서 사용하던지 (현재는 이쪽이 마음에 더 들음)
            // 리소스 매니저에서 모든 스프라이트를 일괄 관리
            Sprite[] sprites = Resources.LoadAll<Sprite>($"Animations/Player/Sprites/{result}");
            spriteDic.Add(one, sprites);
        }
    }

    private void GetHelmetData() 
    {
        helmetType = model._helmetType;
        SetAnimSprite(0);
    }
    private void GetArmourData()
    {
        armourType = model._armourType;
        SetAnimSprite(0);
    }
    private void GetShieldData()
    {
        shieldType = model._shieldType;
        SetAnimSprite(0);
    }
     

    private void SetAnimSprite(int spritesNum)
    {
        SetPartSprite(helmetType, HELMET_TYPE.none, helmetSpriteRenderer, spritesNum);
        SetPartSprite(armourType, ARMOUR_TYPE.none, armourSpriteRenderer, spritesNum);
        SetPartSprite(shieldType, SHIELD_TYPE.none, shieldSpriteRenderer, spritesNum);
    }

    private void SetPartSprite<T>(T type, T noneType, SpriteRenderer renderer, int spriteIndex) where T : Enum
    {
        if (type.Equals(noneType))
        {
            renderer.sprite = null;
            return;
        }

        var sprites = spriteDic[type];
        renderer.sprite = sprites[spriteIndex];
    }

    private int CalSpriteOrder()
    {
        playerState = model._playerState;
        int ps = (int)playerState;
        return ps *= 4;
    }

    // 0,1,2,3 Sprite Array Order
    public void PlayAnim0() => SetAnimSprite(CalSpriteOrder() + 0);
    public void PlayAnim1() => SetAnimSprite(CalSpriteOrder() + 1);
    public void PlayAnim2() => SetAnimSprite(CalSpriteOrder() + 2);
    public void PlayAnim3() => SetAnimSprite(CalSpriteOrder() + 3);

    public void PlayAnimation(PLAYER_STATE playerState) => bodyAnimator.SetInteger("State", (int)playerState);

    public void SetFilpXSprite(bool isFilpX)
    {
        helmetSpriteRenderer.flipX = isFilpX;
        armourSpriteRenderer.flipX = isFilpX;
        shieldSpriteRenderer.flipX = isFilpX;
        bodySpriteRenderer.flipX = isFilpX;
    }
}

