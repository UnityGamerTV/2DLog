using UnityEngine;
public class GameManager : MonoBehaviour
{
    static GameManager s_instance;
    public static GameManager Instance { get { return s_instance; } }


    #region Contents
    MapManagerEX _map = new MapManagerEX();
    ObjectManager _obj = new ObjectManager();
    UIManager _ui = new UIManager();
    SummonManager _summon = new SummonManager();
    DataManagerEX _data = new DataManagerEX();
    InvenManager _inven = new InvenManager();

    public static MapManagerEX Map { get { return Instance._map; } }
    public static ObjectManager Obj { get { return Instance._obj; } }
    public static UIManager Ui { get { return Instance._ui; } }
    public static SummonManager Summon { get { return Instance._summon; } }
    public static DataManagerEX Data { get { return Instance._data; } }
    public static InvenManager Inven { get { return Instance._inven; } }

    #endregion
    #region Core
    SceneManagerEx _scene = new SceneManagerEx();
    ResourceManagerEX _resouce = new ResourceManagerEX();
    TurnManager _Turn = new TurnManager();
    EventManager _evt = new EventManager();
    YieldReturnManager _Yield = new YieldReturnManager();


    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static TurnManager TurnM { get { return Instance._Turn; } }
    public static ResourceManagerEX Resouce { get { return Instance._resouce; } }
    public static EventManager evt { get { return Instance._evt; } }
    public static YieldReturnManager Yield { get { return Instance._Yield; } }
    #endregion

    void Awake()
    {
        Init();
    }

    void Start()
    {

    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<GameManager>();
        }

        s_instance._data.Init();
        s_instance._Turn.Init();

    }

}
