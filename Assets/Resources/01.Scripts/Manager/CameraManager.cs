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

    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera v1;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CancellationTokenSource cts;
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
            GameObject mainCam = resourceManager.Instantiate("Camera/Main Camera");
            cinemachineBrain = mainCam.GetComponent<CinemachineBrain>();
        }
        if (v1 == null)
        {
            GameObject vcam1 = resourceManager.Instantiate("Camera/CM vcam1");
            v1 = vcam1.GetComponent<CinemachineVirtualCamera>();
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
