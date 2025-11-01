using System;
using UnityEngine;
using UnityEngine.XR;

public partial class PlayerService : MonoBehaviour
{
    [Singleton(typeof(FieldManager))] private FieldManager fieldManager;
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
        model.Init();
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

        // 해당 Enum 을 기준으로 인벤(딕셔너리)에 저장
        if (Enum.TryParse<HELMET_TYPE>(helmetSpriteName, out var helmetType))
            model._helmetType = helmetType;
        else
            LogUtil.Log("장착할 수 있는 헬멧이 없습니다.");
    }

    public void EquipArmour(string armourSpriteName)
    {
        if (armourSpriteName.Equals("") || armourSpriteName.Equals(String.Empty) || armourSpriteName.Equals(" "))
        {
            model._shieldType = SHIELD_TYPE.none;
            return;
        }

        armourSpriteName = armourSpriteName.Replace(" ", "_"); // 이름 보정

        if (Enum.TryParse<ARMOUR_TYPE>(armourSpriteName, out var armortype))
            model._armourType = armortype;
        else
            LogUtil.Log("장착할 수 있는 갑옷이 없습니다.");
    }

    public void EquipShield(string shieldSpriteName)
    {
        if (shieldSpriteName.Equals("") || shieldSpriteName.Equals(String.Empty) || shieldSpriteName.Equals(" "))
        {
            model._shieldType = SHIELD_TYPE.none;
            return;
        }

        if (Enum.TryParse<SHIELD_TYPE>(shieldSpriteName, out var shieldType))
            model._shieldType = shieldType;
        else
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
        if (HasMonsterAt(dir)) 
            return;
        // 장착 아이템 체크
        if (HasEquipItemAt(dir)) 
            return;
        if (HasConsumeItemAt(dir))
            return;
        // 이동
        HasCollisionAt(dir);
    }

    private bool HasMonsterAt(Vector3 dir)
    {
        // 몬스터 체크
        var monsterController = mapManager.HasMonsterAt(transform.position + dir);
        if (monsterController == null)
            return false;

        SetDirectionAndFlip(dir);
        SetPlayerState(state._attackState);
        DoPlayerState();

        // 몬스터가 있을 경우 해야될 상태를 넣을 것 // 기본 공격 // 스킬 // 마법 
        return true;
    }

    private bool HasEquipItemAt(Vector3 dir)
    {
        // 1. 아이템 확인만 (획득하지 않음)
        var itemController = mapManager.PeekItemAt(transform.position + dir);
        if (itemController == null)
            return false;

        // 2. 인벤토리 타입 체크 (명시적 비교 + null 안전)
        var inventoryType = GetInventoryType(itemController);
        if (inventoryType == null || inventoryType != InventoryType.EQUIPMENT)
            return false;

        // 3. 장착 인벤토리 용량 체크
        if (model.IsEquipInvenFull())
            return false;

        // 4. 모든 체크 통과 → 실제 획득
        var actualItem = mapManager.HasItemAt(transform.position + dir);
        HandleItemGet(dir, actualItem);
        return true;
    }

    private bool HasConsumeItemAt(Vector3 dir)
    {
        // 1. 아이템 확인만 (획득하지 않음)
        var itemController = mapManager.PeekItemAt(transform.position + dir);
        if (itemController == null)
            return false;

        // 2. 인벤토리 타입 체크 (명시적 비교 + null 안전)
        var inventoryType = GetInventoryType(itemController);
        if (inventoryType == null || inventoryType != InventoryType.CONSUME)
            return false;

        // 3. 소모품 인벤토리 용량 체크
        if (model.IsConsumeInvenFull())
            return false;

        // 4. 모든 체크 통과 → 실제 획득
        var actualItem = mapManager.HasItemAt(transform.position + dir);
        HandleItemGet(dir, actualItem);
        return true;
    }

    private InventoryType? GetInventoryType(ItemController itemController)
    {
        if (itemController == null || itemController._baseDataComponent == null)
            return null;

        if (itemController._baseDataComponent is ItemDataComponent itemDataComponent)
            return itemDataComponent._data._inventory_type;

        return null;
    }

    private void HandleItemGet(Vector3 dir, ItemController itemController)
    {
        SetDirectionAndFlip(dir);
        SetPlayerState(state._getItemState);
        DoPlayerState();
        fieldManager._getItemController = itemController;
        model.GetItem();
    }

    private void HasCollisionAt(Vector3 dir)
    {
        // 이동 체크
        if (!mapManager.HasCollisionAt(gameObject.transform.position + dir))
            dir = Vector3.zero;

        SetDirectionAndFlip(dir);
        SetPlayerState(state._moveState);
        DoPlayerState();
    }

    private void SetDirectionAndFlip(Vector3 dir)
    {
        if (dir.x > 0) SetFilpXSprite(false);
        if (dir.x < 0) SetFilpXSprite(true);
        SetMoveDir(dir);
    }

    public void HandIdle()
    {
        SetPlayerState(state._idleState);
        DoPlayerState();
    }

    public Vector3 GetMoveDir() => model._dir;
    public void SetMoveDir(Vector3 dir) => model._dir = dir;
    public void SetFilpXSprite(bool isFilp) => view.SetFilpXSprite(isFilp);
    public void SetPlayerState(IState state) => curState = state;
    public void DoPlayerState() => curState.OnStateEnter();
    public void GetItemComplete() { view.GetItemComplete(); }
    public void ResponseEquipInven() => model.ResponseEquipInven();
    public void ResponseConsumeInven() => model.ResponseConsumeInven();
    public void EquipItem(ItemDataComponent component) => model.EquipItem(component);
    public void RemoveItem(ItemDataComponent component, bool isDestroy = true) => model.RemoveItem(component, isDestroy);
    public void ResponseEquipData() => model.ResponseEquipData();
}
