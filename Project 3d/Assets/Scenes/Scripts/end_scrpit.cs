using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;
public class end_scrpit : MonoBehaviour
{

    static public int npc_talk_int = 0;
    Boolean justOne = false;
    public Text txt_Dialogue;
    public string playerName;
    static public bool isDialogue = true;
    public bool dragon_dead;
    // 대화가 얼마나 진행 되었는지 확인
    private int count = 0;
    static private String[] dialogue; // 대화가 들어가는 배열
    private Transform target;
    private void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void Awake()
    {
        dialogue = new String[100];
        // 처음 intro 대사
        dialogue[0] = playerName + "님 마침내 드래곤을 무찔러 주셨군요 ....";
        dialogue[1] = "덕분에 마을에는 평화가 찾아올 것 같습니다 ,,,,";
        dialogue[2] = playerName + " 님의 모험이 평온하길 저희가 기원하겠습니다 ..!!";
        dialogue[3] = "\tThe End\t";
        dialogue[4] = "게임 플레이 해주셔서 감사합니다 !";
        // 대화 끝에 null을 넣어서 대화가 끝났음을 알린다.
        dialogue[5] = "제작자 : 김범진,민평화 \n스토리 구상 : 민평화";
        dialogue[6] = "";
       
    }
    private void Start()
    {
        target = GameObject.Find("Main Camera").GetComponent<Cameracontroller>().target;
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
        if (dragon_dead==false && count < 7)
        {

            GameObject.Find("End").gameObject.SetActive(true);
            GameObject.Find("End").transform.FindChild("Ends").gameObject.SetActive(true);
            GameObject.Find("Ends").transform.FindChild("Texts").gameObject.SetActive(true);
            GameObject.Find("Ends").transform.FindChild("Target").gameObject.SetActive(true);
            if(GameObject.Find("Target").activeSelf == true)
            {
                target = GameObject.Find("Target").transform;
            }
            else
            {
                target = GameObject.Find("CameraTarget").transform;

            }
        }
        if (dragon_dead == false && count >= 7)
        {
            RestartScene();
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
        if (isDialogue && count != 7)
        {
            if (dialogue[count].Equals("end"))
            {
                npc_talk_int = 1;
            }
            //if (OVRInput.GetDown(OVRInput.Button.One))
            if (Input.GetKeyDown(KeyCode.G) && dragon_dead == false)
            {
                if (count < dialogue.Length)
                    NextDialogue();
            }
        }
    }
}
