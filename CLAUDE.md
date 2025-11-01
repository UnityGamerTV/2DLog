# CLAUDE.md

이 문서는 Claude Code (claude.ai/code)가 이 저장소에서 작업할 때 참고하는 가이드입니다.

## 프로젝트 개요

**2DLOG** (일명 "HelloDungeoun")은 장비, 인벤토리, 스킬 시스템을 갖춘 Unity 2D 로그라이크 던전 크롤러 게임입니다. 이 프로젝트는 커스텀 의존성 주입을 사용한 정교한 이벤트 기반 아키텍처를 사용하며, UI와 게임 로직에 MVC 스타일 패턴을 따릅니다.

**Unity 버전**: 2021.x (패키지 버전으로 추정)
**제품명**: HelloDungeoun
**타겟 플랫폼**: Android (주), iOS, Standalone

## 빌드 및 실행

### 프로젝트 열기
```bash
# Unity 에디터에서 열기
# File -> Open Project -> D:\2DLOG 디렉토리 선택
```

### 게임 빌드
```bash
# Unity 에디터 내에서:
# File -> Build Settings
# 타겟 플랫폼 선택 (Android/iOS/Standalone)
# "Build" 또는 "Build and Run" 클릭
```

### 스크립팅 정의 심볼
- **Android**: `UNITASK_DOTWEEN_SUPPORT`, `MOREMOUNTAINS_NICEVIBRATIONS_INSTALLED`, `UNITY_POST_PROCESSING_STACK_V2`, `AMPLIFY_SHADER_EDITOR`, `DEBUG_LOG`
- **Standalone**: `DEBUG_LOG`, `UNITY_POST_PROCESSING_STACK_V2`

수정 위치: `Edit -> Project Settings -> Player -> Other Settings -> Scripting Define Symbols`

## 아키텍처 개요

### 핵심 시스템 초기화 흐름

게임은 `Manager.Init()`에서 관리하는 엄격한 초기화 순서를 따릅니다:

1. **DataManager** - 모든 JSON 게임 데이터 로드 (아이템, 몬스터, 능력치, 언어)
2. **ObjectPoolManager** - 오브젝트 풀링 준비
3. **ResourceManager** - 에셋 로딩
4. **TestManager** - 에디터 테스트 유틸리티
5. **FieldManager** - 필드 오브젝트 생성
6. **FactoryManager** - 팩토리 패턴 설정
7. **MapManager** - 맵과 충돌 관리
8. **CameraManager** - 카메라 제어
9. **EventManager** - 이벤트 시스템 등록
10. **UIManager** - UI 생명주기 관리

**중요**: 이 순서는 매우 중요합니다. 매니저 초기화를 수정할 경우 의존성을 유지해야 합니다.

### 디렉토리 구조

```
Assets/Resources/01.Scripts/
├── Manager/          # 싱글톤 매니저 (DataManager, EventManager, UIManager 등)
├── DataClass/        # 데이터 모델 (ItemData, PlayerData, MonsterData 등)
├── Factory/          # 팩토리 패턴 구현 (ItemFactory, MonsterFactory 등)
├── Player/           # 플레이어 로직, 상태, 데이터 컴포넌트
├── Monster/          # 몬스터 행동과 AI
├── UI/               # UI 시스템 (Controller/Service/Model/View 레이어)
├── Common/           # 공유 추상화 및 베이스 클래스
├── Utils/            # 유틸리티 (DI용 InjectUtil, LogUtil)
├── Singleton/        # 싱글톤 베이스 클래스
├── Skill/            # 스킬 및 능력 시스템
├── Item/             # 아이템 컨트롤러
├── Effect/           # 시각 효과
├── Scene/            # 씬 관리
└── Table/            # 데이터 테이블 (RandartOptionTable)
```

### 의존성 주입 시스템

코드베이스는 `InjectUtil`을 통한 **속성 기반 의존성 주입**을 사용합니다:

```csharp
// 싱글톤 주입 - 매니저 인스턴스 주입
[Singleton(typeof(EventManager))] private EventManager eventManager;
[Singleton(typeof(DataManager))] private DataManager dataManager;

// 컴포넌트 주입 - 이름으로 자식 GameObject 자동 탐색
[FindComponents("View", "Model", "Service")]
private Component[] components;

// Init() 또는 Awake()에서 반드시 호출
InjectUtil.InjectSingleton(this);
InjectUtil.InjectComponents(this);
```

**새 컴포넌트 추가 시:**
- 매니저 참조 주입을 위해 항상 `InjectUtil.InjectSingleton(this)` 호출
- 자식 컴포넌트 찾기 위해 `InjectUtil.InjectComponents(this)` 사용
- `FindComponents` 속성 사용 시 정확한 GameObject 이름 사용

### 이벤트 시스템

프로젝트는 **타입 안전 제네릭 이벤트 시스템** (`EventManager`)을 사용합니다:

```csharp
// 이벤트 열거형이 이벤트 타입 정의
enum EVENT_PLAYER { PLAYER_MOVE_COMPLETE, PLAYER_ATTACK_COMPLETE, ... }
enum EVENT_EQUIP_INVEN_UI { OPEN_EQUIP_INVENTORY_DETAIL_UI, ... }

// 이벤트 수신 (IListener 인터페이스)
public class MyController : MonoBehaviour, IListener {
    void Start() {
        eventManager.AddListener<EVENT_PLAYER>(EVENT_PLAYER.PLAYER_MOVE_COMPLETE, this);
    }

    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null) {
        switch (Event_Type) {
            case EVENT_PLAYER.PLAYER_MOVE_COMPLETE:
                // 이벤트 처리
                break;
        }
    }
}

// 이벤트 전송
eventManager.PostNotification<EVENT_PLAYER>(EVENT_PLAYER.PLAYER_MOVE_COMPLETE, this, optionalParam);
```

**주요 이벤트 카테고리:**
- `EVENT_PLAYER` - 플레이어 이동, 전투, 아이템 획득
- `EVENT_EQUIP_INVEN_UI` - 장비 인벤토리 UI 상호작용
- `EVENT_ITEM_POPUP_UI` - 아이템 상세 팝업 상호작용
- `EVENT_ABILITY_INVEN_UI` - 능력/스킬 인벤토리
- `EVENT_BOTTOM_BASE_UI` - 하단 UI 바 버튼

### UI 아키텍처 (MVC 패턴)

모든 UI 컴포넌트는 **Controller/Service/Model/View** 분리를 따릅니다:

```
UI_Popup_Item/
├── UI_Popup_ItemController  # 입력 처리, 이벤트 등록
├── UI_Popup_ItemService      # 비즈니스 로직, Model/View 조정
├── UI_Popup_ItemModel        # 데이터 상태, 액션
└── UI_Popup_ItemView         # 렌더링, 시각적 업데이트
```

**데이터 흐름:**
1. 사용자 액션 → **Controller**가 입력 수신
2. Controller → **Service**가 로직 처리
3. Service 업데이트 → **Model** (데이터 상태)
4. Model 발동 → **Action/Event**
5. **View**가 Model 액션 수신 → 비주얼 업데이트

**새 UI 생성 시:**
- 적절한 베이스 상속 (`UI_Scene`은 지속적, `UI_Popup`은 모달)
- `UIManager.ShowSceneUI<T>()` 또는 `ShowPopupUI<T>()`로 인스턴스화
- 네이밍 규칙 준수: `UI_[Scene|Popup]_[Name]_[Layer]`
- 항상 관심사 분리: Controller (이벤트), Service (로직), Model (데이터), View (렌더링)

### 데이터 관리

#### JSON 데이터 로딩
모든 게임 데이터는 시작 시 `DataManager`에 의해 JSON 파일에서 로드됩니다:

```csharp
// 아이템 타입: Amulet, Armor, Axe, Boots, Bow, Glove, Helmet, Mace, Ring,
//             Robe, Shield, Spear, Staff, Sword, Etc
Dictionary<string, ItemData> helmetDic, armorDic, weaponDic, etc.

// 능력치
Dictionary<string, PotionData> potionDic
Dictionary<string, ScrollData> scrollDic

// 몬스터와 현지화
Dictionary<string, MonsterData> monsterDic
Dictionary<string, LanguageData> languageDic

// 절차적 생성
Dictionary<int, RandartData> randartDic
```

**중요**: 아이템은 이름이 아닌 `_nickname` 필드(고유 식별자)로 키잉됩니다.

#### PlayerDataComponent (부분 클래스)

플레이어 데이터는 구조화를 위해 여러 파일로 분리되어 있습니다:

- **PlayerDataComponent.cs** - 핵심 속성, 장비 열거형 (`HELMET_TYPE`, `ARMOUR_TYPE`, `SHIELD_TYPE`)
- **PlayerDataComponent.Equipment.cs** (~245줄) - 장비 슬롯 관리, `EquipItem()`, `UnEquipItem()`
- **PlayerDataComponent.Inven.cs** (~165줄) - 인벤토리 관리, `GetItem()`, 용량 체크
- **PlayerDataComponent.SkillLevel.cs** - 스킬/능력 추적

**플레이어 데이터 수정 시:**
- 장비 변경은 `PlayerDataComponent.Equipment.cs`에
- 인벤토리 로직은 `PlayerDataComponent.Inven.cs`에
- 장비 슬롯 변경 시 항상 적절한 `Action` 콜백 호출 (예: `helmetSlotAction`)

### 인벤토리 시스템

#### 두 가지 인벤토리 타입

1. **장비 인벤토리** (`_equipmentInven`):
   - 타입: `List<ItemDataComponent>`
   - 용량: 15 슬롯 (하드 리밋)
   - 확인: `IsEquipInvenFull()`
   - 추가: `AddEquipItem(ItemDataComponent)`

2. **소모품 인벤토리** (`_consumeInven`):
   - 타입: `Dictionary<ConsumeType, InvenItemData>`
   - 용량: 15 소모품 타입 (타입당 무제한 스택)
   - 확인: `IsConsumeInvenFull()`
   - 추가: `AddConsumeItem(ItemDataComponent)`

#### 아이템 획득 흐름

```
1. 플레이어가 필드의 아이템 위를 걸음 (ItemController)
2. PlayerService.IsValidPosition()가 충돌 확인
3. PlayerService.HandleItemGet()이 상태를 GETITEM으로 설정
4. PlayerDataComponent.GetItem():
   - FieldManager에서 ItemDataComponent 획득
   - 인벤토리 타입 확인 (EQUIPMENT vs CONSUME)
   - 데이터 복사 (필드 오브젝트 참조 안 함)
   - 적절한 인벤토리에 추가
5. 이벤트 전송:
   - EVENT_EQUIP_INVEN_UI.RESPONDED_EQUIP_INVENTORY_DATA
   - EVENT_PLAYER.PLAYER_ITEM_UNEQUIP (교체 시)
6. 이벤트 리스너를 통해 UI 업데이트
```

**중요**: 아이템은 획득 시 **복사**되며, 참조되지 않습니다. 필드 아이템과 인벤토리 아이템은 별도 인스턴스입니다.

### 팩토리 패턴

팩토리는 풀링을 사용한 오브젝트 생성을 처리합니다:

```csharp
// ItemFactory.CreateObj() 프로세스:
1. 오브젝트 풀 확인 (가능하면 재사용)
2. 풀링되지 않은 경우 인스턴스화
3. 위치, 이름, 부모 설정
4. 애니메이터 컨트롤러 로드
5. 리소스 배열에서 스프라이트 설정
6. 적절한 DataComponent 생성:
   - ItemDataComponent (장비)
   - PotionDataComponent (포션)
   - ScrollDataComponent (스크롤)
   - MagicDataComponent (마법)
7. 적격한 경우 랜덤 아트 데이터 적용 (1-2개 랜덤 스탯 보너스)
```

**새 아이템 타입 생성 시:**
- `ItemFactory.CreateObj()`에 케이스 추가
- 적절한 `DataComponent` 클래스 정의
- JSON 데이터 로드하도록 `DataManager` 업데이트
- Resources 폴더에 스프라이트 리소스 추가

### 상태 관리

플레이어 상태는 **상태 패턴**으로 관리됩니다:

```csharp
enum PLAYER_STATE {
    IDLE = 0,
    MOVE = 1,
    ATTACK = 2,
    DIE = 3,
    GETITEM = 4,
    ENDTURN = 5,
}
```

- `IState` 인터페이스가 상태 행동 정의
- `PlayerState` 컴포넌트가 상태 인스턴스 보유
- `PlayerService.curState`가 활성 상태 추적
- 상태가 애니메이션 재생 제어

**새 상태 추가 시:**
- `IState`를 구현하는 새 상태 클래스 생성
- `PLAYER_STATE`에 열거형 값 추가
- `PlayerState` 초기화에서 상태 등록
- `PlayerService`에서 상태 전환 처리

### 스탯용 데코레이터 패턴

코드베이스는 스탯 수정을 위해 데코레이터를 사용합니다:

```csharp
// 베이스 클래스
interface IDecorator {
    void Operation();  // 스탯 수정 적용
    void Revert();     // 스탯 수정 제거
}

abstract class DecoratorDataComponent : BaseDataComponent, IDecorator
```

- `PlayerDataComponent`는 `DecoratorDataComponent`를 상속
- 임시 스탯 버프 추가/제거 가능
- 장비 보너스, 버프, 디버프에 사용

## 주요 의존성

### Unity 패키지
- **Newtonsoft.Json** (3.2.1) - JSON 직렬화/역직렬화
- **TextMeshPro** (3.0.6) - UI 텍스트 렌더링
- **Cinemachine** (2.8.9) - 카메라 관리
- **Post Processing** (3.5.1) - 시각 효과
- **UniTask** - Async/await 지원 (스크립팅 정의로 추정)
- **DOTween** - 애니메이션 트위닝 (스크립팅 정의로 추정)
- **More Mountains Nice Vibrations** - 햅틱 피드백

### 서드파티 에셋
- **Amplify Shader Editor** - 커스텀 셰이더 생성
- **UniRx** - 리액티브 확장
- **More Mountains Feedbacks** - 시각/오디오 피드백 시스템

## 일반적인 개발 패턴

### 새 매니저 추가

```csharp
public class MyNewManager : Singleton<MyNewManager> {
    public void Init() {
        // 초기화 로직
    }
}

// Manager.Init()에 적절한 순서로 추가:
MyNewManager.Instance.Init();
```

### 새 UI 팝업 생성

```csharp
// 1. 폴더 생성: UI/UI_Popup/UI_Popup_MyFeature/
// 2. 네 개의 스크립트 생성:
//    - UI_Popup_MyFeatureController : MonoBehaviour, IListener
//    - UI_Popup_MyFeatureService : MonoBehaviour
//    - UI_Popup_MyFeatureModel : MonoBehaviour
//    - UI_Popup_MyFeatureView : MonoBehaviour

// 3. Controller에서:
[Singleton(typeof(EventManager))] private EventManager eventManager;
[FindComponents("Service", "Model", "View")] private Component[] components;

void Start() {
    InjectUtil.InjectSingleton(this);
    InjectUtil.InjectComponents(this);
}

// 4. 팝업 표시:
UIManager.Instance.ShowPopupUI<UI_Popup_MyFeatureController>();
```

### 새 이벤트 타입 추가

```csharp
// 1. 열거형 정의
public enum EVENT_MY_FEATURE {
    ON_FEATURE_ACTIVATED,
    ON_FEATURE_DEACTIVATED,
}

// 2. 리스너 등록
eventManager.AddListener<EVENT_MY_FEATURE>(EVENT_MY_FEATURE.ON_FEATURE_ACTIVATED, this);

// 3. 이벤트 전송
eventManager.PostNotification<EVENT_MY_FEATURE>(
    EVENT_MY_FEATURE.ON_FEATURE_ACTIVATED,
    this,
    optionalData
);
```

## 디버깅

### 커스텀 로깅
프로젝트는 로깅을 위해 `LogUtil`을 사용합니다 (`DEBUG_LOG` 스크립팅 정의로 제어):

```csharp
LogUtil.Log("메시지");
LogUtil.LogWarning("경고");
LogUtil.LogError("오류");
```

### 인스펙터 가시성
대부분의 필드는 제어된 액세스를 위해 프로퍼티와 함께 `[SerializeField]`를 사용합니다:

```csharp
public int _health { get { return health; } set { health = value; } }
[SerializeField] private int health;
```

이를 통해 캡슐화를 유지하면서 인스펙터 편집이 가능합니다.

## 프로젝트 고유 규칙

### 네이밍 규칙
- **매니저**: `[Name]Manager` (예: `DataManager`)
- **팩토리**: `[Name]Factory` (예: `ItemFactory`)
- **데이터 컴포넌트**: `[Name]DataComponent` (예: `PlayerDataComponent`)
- **UI 컨트롤러**: `UI_[Scene|Popup]_[Name]Controller`
- **이벤트 열거형**: `EVENT_[CATEGORY]` (예: `EVENT_PLAYER`)
- **Private 필드**: camelCase이며 프로퍼티 접근자는 `_` 접두사

### 장비 타입 열거형
장비 타입은 스프라이트 이름과 매칭되는 광범위한 열거형 사용:
- `HELMET_TYPE` (helmet1-helmet18, none)
- `ARMOUR_TYPE` (chainmail, platemail, robe 등)
- `SHIELD_TYPE` (shield1-shield10, none)

**새 장비 스프라이트 추가 시**, `PlayerDataComponent.cs`의 해당 열거형 업데이트.

### 절차적 아이템 생성 (Randart)
아이템은 1-2개의 랜덤 스탯 보너스를 가질 수 있음:
- `ItemFactory.CreateObj()` 중 확인
- `ItemGrade` (희귀도) 기반
- 옵션은 `RandartOptionTable`에 저장
- 생성 시 `ItemDataComponent`에 적용

## 현지화

게임은 한국어와 영어를 지원:
- `LanguageData` JSON 파일에서 로드
- `DataManager.languageDic`으로 관리
- 하드코딩된 문자열이 아닌 언어 키 사용

## 보존해야 할 중요한 파일

- `Assets/Resources/01.Scripts/Manager/Manager.cs` - 초기화 순서
- `Assets/Resources/01.Scripts/Player/PlayerDataComponent*.cs` - 플레이어 데이터 (부분 클래스)
- `Assets/Resources/01.Scripts/Utils/InjectUtil.cs` - 의존성 주입
- `Assets/Resources/01.Scripts/Manager/EventManager.cs` - 이벤트 시스템 코어
- `Assets/Resources/01.Scripts/Factory/ItemFactory.cs` - 아이템 생성 로직

## 일반적인 함정

1. **InjectUtil 호출 누락** - 주입을 사용하는 컴포넌트에서 항상 `InjectSingleton(this)`와 `InjectComponents(this)` 모두 호출
2. **Manager 초기화 순서 깨뜨리기** - 의존성은 의존하는 매니저보다 먼저 초기화되어야 함
3. **아이템 복사 대신 참조** - 인벤토리에 아이템 추가 시 `CopyItemData()` 사용
4. **장비 액션 무시** - 장비 슬롯 변경 시 해당 `Action` 콜백 호출 필수
5. **잘못된 GameObject 네이밍** - `FindComponents`는 정확한 자식 GameObject 이름 필요
6. **이벤트 제거 누락** - `OnDestroy()`에서 항상 `RemoveListener()` 호출
7. **인벤토리 용량** - 아이템 추가 전 `IsEquipInvenFull()`과 `IsConsumeInvenFull()` 확인

## Git 워크플로우

**현재 브랜치**: Dev
**메인 브랜치**: master (풀 리퀘스트에 사용)

프로젝트는 git 커밋을 통해 구현 진행 상황을 추적합니다. 최근 작업 내용:
- 장비 인벤토리 시스템 리팩토링
- 데이터 구조 변경
- 아이템 팝업 UI 개발
