using System;
using UnityEngine;

public partial class PlayerService : MonoBehaviour
{
    [Singleton(typeof(MapManager))] private MapManager mapManager;
    [FindComponents("View"), SerializeField] private PlayerView view;
    [FindComponents("Model"), SerializeField] private PlayerDataComponent model;
    [FindComponents("State"), SerializeField] private PlayerState state;

    private IState preState;
    private IState tempState;
    private IState curState;

    public Animator GetAnimator()
    {
        return view._bodyAnimator;
    }

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
        InjectUtil.InjectComponents(this);

        state.Init();
        view.Init();
    }

    public void PlayAnim0() => view.PlayAnim0();
    public void PlayAnim1() => view.PlayAnim1();
    public void PlayAnim2() => view.PlayAnim2();
    public void PlayAnim3() => view.PlayAnim3();


    // 받은 string 값 또는 enum 값을 Model 에 넘겨주면 자동으로 스프라이트 변경 됨
    public void EquipHelmet(string helmetSpriteName)
    {
        if (helmetSpriteName.Equals("") || helmetSpriteName.Equals(String.Empty) || helmetSpriteName.Equals(" "))
        {
            model._shieldType = SHIELD_TYPE.none;
            return;
        }

        foreach (HELMET_TYPE one in Enum.GetValues(typeof(HELMET_TYPE)))
        {
            if (helmetSpriteName.Equals(one.ToString()))
            {
                model._helmetType = one;
                return;
            }
        }
        LogUtil.Log("장착할 수 있는 헬멧이 없습니다.");
    }

    public void EquipArmour(string armourSpriteName)
    {
        if (armourSpriteName.Equals("") || armourSpriteName.Equals(String.Empty) || armourSpriteName.Equals(" "))
        {
            model._shieldType = SHIELD_TYPE.none;
            return;
        }

        armourSpriteName = armourSpriteName.Replace(" ", "_");

        foreach (ARMOUR_TYPE one in Enum.GetValues(typeof(ARMOUR_TYPE)))
        {
            if (armourSpriteName.Equals(one.ToString()))
            {
                model._armourType = one;
                return;
            }
        }
        LogUtil.Log("장착할 수 있는 갑옷이 없습니다.");
    }

    public void EquipShield(string shieldSpriteName)
    {
        if (shieldSpriteName.Equals("") || shieldSpriteName.Equals(String.Empty) || shieldSpriteName.Equals(" "))
        {
            model._shieldType = SHIELD_TYPE.none;
            return;
        }

        foreach (SHIELD_TYPE one in Enum.GetValues(typeof(SHIELD_TYPE)))
        {
            if (shieldSpriteName.Equals(one.ToString()))
            {
                model._shieldType = one;
                return;
            }
        }
        LogUtil.Log("장착할 수 있는 쉴드가 없습니다.");
    }

    public void PlayAnimation(PLAYER_STATE playerState)
    {
        model._playerState = playerState;
        view.PlayAnimation(playerState);
    }

    // 이건 방향키 이벤트 전용으로 사용해야 될 것 같은 느낌이?
    // 추후 마법이나 스킬은 다른 이벤트를 받아서 쓰는게 어떨까?
    public void IsValidPosition(Vector3 dir)
    {
        // 몬스터 체크
        if (HasMonsterAt(dir) != null) 
            return;
        // 아이템 체크
        //if (HasItemAt(dir) != null) 
            //return;
        // 이동
        HasCollisionAt(dir);
    }

    private MonsterController HasMonsterAt(Vector3 dir)
    {
        // 몬스터 체크
        var monsterController = mapManager.HasMonsterAt(transform.position + dir);
        if (monsterController == null)
            return null;

        // 몬스터가 있을 경우 해야될 상태를 넣을 것 // 기본 공격 // 스킬 // 마법 
        return monsterController;
    }

    private ItemController HasItemAt(Vector3 dir)
    {
        // 아이템 체크
        var itemController = mapManager.HasItemAt(transform.position + dir);
        if (itemController == null)
            return null;

        // 아이템이 있을 경우 해야될 상태를 넣을 것 
        return itemController;
    }

    private void HasCollisionAt(Vector3 dir)
    {
        Vector3 newDir = dir;
        // 이동 체크
        if (!mapManager.HasCollisionAt(gameObject.transform.position + newDir))
            newDir = Vector3.zero;

        if (newDir.x > 0) // 0 보다 크면 오른쪽
            SetFilpXSprite(false);

        if (newDir.x < 0) // 0 보다 작으면 왼쪽
            SetFilpXSprite(true);

        SetMoveDir(newDir);
        SetPlayerState(state._moveState);
        DoPlayerState();
    }

    public void HandIdle()
    {
        SetPlayerState(state._idleState);
        DoPlayerState();
    }

    public Vector3 GetMoveDir() => model._dir;
    public void SetMoveDir(Vector3 dir) => model._dir = dir;
    public void SetFilpXSprite(bool isFilp) => view.SetFilpXSprite(isFilp);

    public void SetPlayerState(IState state)
    {
        tempState = curState;
        curState = state;
        preState = tempState;
    }

    public void DoPlayerState()
    {
        if (preState != null)
            preState.OnStateExit();

        curState.OnStateEnter();
    }
}
