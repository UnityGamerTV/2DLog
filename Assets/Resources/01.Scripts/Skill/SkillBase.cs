using UnityEngine;

public enum SKILL_RANGE
{
    SINGLE,             // 단일 대상
    THREE_HORIZONTAL,   // 가로 3칸
    FIVE_CROSS,         // 십자형 5칸
    NINE_SQUARE,        // 9칸 정사각형
    EIGHT_DIRECTIONS    // 8방향
}

// 대미지 생각은 일단 x
public abstract class SkillBase : MonoBehaviour
{
    public IController _casterController { get { return casterController; } set { casterController = value; } }
    [SerializeField] protected IController casterController;
    public IController _targetController { get { return targetController; } set { targetController = value; } }
    [SerializeField] protected IController targetController;
    public ParticleSystem _casterParticle { get { return casterParticle; } set { casterParticle = value; } }
    [SerializeField] protected ParticleSystem casterParticle;
    public ParticleSystem _targetParticle { get { return targetParticle; } set {  targetParticle = value; } }
    [SerializeField] protected ParticleSystem targetParticle;
    public int _reach { get { return reach; } set { reach = value; } }
    [SerializeField] protected int reach;
    public SKILL_RANGE _range { get { return _range; } set { _range = value; } }
    [SerializeField] protected SKILL_RANGE range;

    public virtual void Init()
    {

    }

    public abstract void PlayCasterParticle();
    public abstract void PlayTargetParticle();
    public abstract void StopCasterParticle();
    public abstract void StopTargetParticle();
}



// 스킬 범위
// 단일
// 가로
// 십자형
// 9칸
// 8칸
