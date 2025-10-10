using Cinemachine;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;


// 항상 특정 경로에 있는 시네머신브레인과 시네머신을 가지고 올까?
// 아니면 씬에 항상 있다고 가정할까?
// 맵에 플레이어는 항상 있다고 가정

public class CameraManager : Singleton<CameraManager>, IManager
{
    [Singleton(typeof(FieldManager))] private FieldManager fieldManager;
    [Singleton(typeof(ResourceManager))] private ResourceManager resourceManager;

    public Camera _mainCamera { get { return mainCamera; } }
    [SerializeField] private Camera mainCamera;

    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera v1;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CancellationTokenSource cts;

    private readonly string MAIN_CAMERA_PATH = "Prefabs/Camera/Main Camera";
    private readonly string VCAM1_PATH = "Prefabs/Camera/CM vcam1";

    public void Init()
    {
        InjectUtil.InjectSingleton(this);
    }

    public void StartCamV1()
    {
        // 플레이어 
        if (playerController == null)
            playerController = fieldManager._playerController;

        // 메인카메라 가상카메라 로드
        if (cinemachineBrain == null)
        {
            // TODO 테스트용 임시 코드
            var tempCam = GameObject.Find("Main Camera");
            if (tempCam == null)
            {
                var mainCam = resourceManager.Instantiate<CinemachineBrain>(MAIN_CAMERA_PATH);
                cinemachineBrain = mainCam;
                mainCamera = mainCam.GetComponent<Camera>();
            }
            else
            {
                cinemachineBrain = tempCam.GetComponent<CinemachineBrain>();
                mainCamera = tempCam.GetComponent<Camera>();
            }
            // 테스트용 임시 코드 끝

            // 원본코드
            //var mainCam = resourceManager.Instantiate<CinemachineBrain>(MAIN_CAMERA_PATH);
            //cinemachineBrain = mainCam;
            //mainCamera = mainCam.GetComponent<Camera>();
        }
        if (v1 == null)
        {
            var vcam1 = resourceManager.Instantiate<CinemachineVirtualCamera>(VCAM1_PATH);
            v1 = vcam1;
        }

        // 카메라 팔로잉
        v1.Follow = playerController.transform;
        v1.LookAt = playerController.transform;
    }

    public void StopCamV1()
    {
        if (v1 != null)
        {
            v1.Follow = null;
            v1.LookAt = null;
            v1.gameObject.SetActive(false);
        }
    }

    public void Release()
    {
        
    }
}
