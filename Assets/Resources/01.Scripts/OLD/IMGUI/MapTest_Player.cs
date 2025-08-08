using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class MapTest_Player : MonoBehaviour
{
    bool showTextField = false;
    string userInput1 = "helmet1";
    string userInput2 = "leader armour1";
    string userInput3 = "shield1";
    public GameObject map;
    private void OnGUI()
    {
        // Make a background box
        GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
        boxStyle.fontSize = 30;

        GUI.Box(new Rect(870, 10, 200, 200), "Player Test", boxStyle);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 30;

        if (GUI.Button(new Rect(880, 80, 180, 100), "ChangeItem", buttonStyle))
        {
            showTextField = true;
        }

        GUIStyle textFieldStyle = new GUIStyle(GUI.skin.textField);
        textFieldStyle.fontSize = 30;

        if (showTextField)
        {
            userInput1 = GUI.TextField(new Rect(Screen.width / 4, Screen.height / 2, Screen.width / 2, 100), userInput1, textFieldStyle);
            userInput2 = GUI.TextField(new Rect(Screen.width / 4, Screen.height / 2 + 160, Screen.width / 2, 100), userInput2, textFieldStyle);
            userInput3 = GUI.TextField(new Rect(Screen.width / 4, Screen.height / 2 + 320, Screen.width / 2, 100), userInput3, textFieldStyle);
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + 800 / 2, 180, 100), "확인", buttonStyle))
            {
                ChangeEquipmentItem(userInput1, userInput2, userInput3);
                showTextField = false; // 한글 테스트
            }

            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 800 / 2, 180, 100), "취소", buttonStyle))
            {
                showTextField = false;
            }
        }
    }


    private void ChangeEquipmentItem(string userInput1,  string userInput2, string userInput3)
    {
        var player = GameObject.Find("Player(Clone)");
        var playerController = player.GetComponent<PlayerController>();

        playerController.EquipHelmet(userInput1);
        playerController.EquipArmour(userInput2);
        playerController.EquipShield(userInput3);

        // 만약 씽크가 맞지 않더라도 큰 문제는 없음
        // 테스트 코드이고, 인게임에서는 각각 아이템을 한개씩 순차적으로 착용하기 때문
        //playerController.SetAnimation(userInput1, userInput2, userInput3);
    }

}
