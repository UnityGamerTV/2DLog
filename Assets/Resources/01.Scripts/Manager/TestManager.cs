using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : Singleton<TestManager>, IManager
{
    public List<Animator> _animators { get { return animators; } set { animators = value; } }
    [SerializeField] private List<Animator> animators;

    public PlayerController _playerController { get { return playerController; } set { playerController = value; } }
    [SerializeField] private PlayerController playerController; // 플레이어
    private int StateNum = 1;


    public void Init()
    {
        animators = new List<Animator>();
    }

    public void NextMobAnim()
    {
        foreach (var one in animators)
        {
            one.SetInteger("State", StateNum);
        }

        //playerController.SetPlayerState3((PLAYER_STATE)StateNum);

        if (StateNum < 3)
        {
            StateNum++;
        }
        else
        {
            StateNum = 0;
        }
    }

    public void ResetAnim()
    {
        animators.Clear();
    }

    public void Release()
    {
        
    }
}
