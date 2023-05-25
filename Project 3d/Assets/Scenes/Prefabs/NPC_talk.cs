using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using static System.Net.Mime.MediaTypeNames;

[System.Serializable]
public class Dialogue
{
    // 여러줄을 쓸 수 있게 해준다.
    [TextArea]
    public string dialogue;
}
public class NPC_talk : MonoBehaviour
{
    static public int npc_talk_int = 0;
    Boolean justOne = false;
    public Text txt_Dialogue;
    public string playerName;

    static public bool isDialogue = true;

    // 대화가 얼마나 진행 되었는지 확인
    private int count = 0;

    static private String[] dialogue; // 대화가 들어가는 배열

    private void Awake()
    {
        dialogue = new String[100];
        // 처음 intro 대사
        dialogue[0] = "안녕하세요 " +playerName+"님"+"  반가워요";
        dialogue[1] = playerName+"님 현재 마을에는 드래곤이 습격해왔습니다.";
        dialogue[2] = "현재 드래곤은 마을 수뇌부에 자리를 잡고 있습니다.";
        dialogue[3] = "부디 드래곤을 잡아 마을을 구원해주세요.....";
        dialogue[4] = "하지만 현재"+playerName+"님은 아직 드래곤과 대적하기엔 부족하신 것 같습니다.";
        dialogue[5] = "드래곤과 싸우기 위해 구축해놓은 훈련장을 통해"+playerName+"님 성장을 돕겠습니다.";
        dialogue[6] = "훈련장을 포탈을 통해 입장하실 수 있습니다.";
        dialogue[7] = "그럼"+playerName+"님의 무운을 빕니다 ...";
        dialogue[8] = "부디 드래곤을 물리쳐 마을을 구해주세요 !!!";
        // 대화 끝에 null을 넣어서 대화가 끝났음을 알린다.
        dialogue[9] = "";
        dialogue[10] = "end";
    }
    private void Start()
    {
        ShowDialogue(0);
    }

    public void ShowDialogue(int count_)
    {
        count = count_;
        NextDialogue();
    }
    public void talk_npc()
    {
        count = 12;
        txt_Dialogue.text = dialogue[count];
    }

    private void NextDialogue()
    {
        txt_Dialogue.text = dialogue[count];
        count++;
    }
    void Update()
    {
        if (count >= 10)
        {
            gameObject.SetActive(false);
        }
        if (npc_talk_int == 1 && justOne == false)
        {
            talk_npc();
            justOne = true;
        }
        else if (npc_talk_int == 2)
        {

            justOne = false;
        }
        // count 가 12면 선택지 대화 진행중이라 막아 둔다.
        if (isDialogue && count != 10)
        {
            if (dialogue[count].Equals("end"))
            {
                npc_talk_int = 1;
            }
            //if (OVRInput.GetDown(OVRInput.Button.One))
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (count < dialogue.Length)
                    NextDialogue();
            }
        }
    }
}
