using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class end_scrpit : MonoBehaviour
{

    static public int npc_talk_int = 0;
    Boolean justOne = false;
    public Text txt_Dialogue;
    public string playerName;
    static public bool isDialogue = true;
    private bool dragon_dead;
    // ��ȭ�� �󸶳� ���� �Ǿ����� Ȯ��
    private int count = 0;
    public GameObject end;
    static private String[] dialogue; // ��ȭ�� ���� �迭
    private Transform target;
    private void Awake()
    {
        dialogue = new String[100];
        // ó�� intro ���
        dialogue[0] = playerName + "�� ��ħ�� �巡���� ���� �ּ̱��� ....";
        dialogue[1] = "���п� �������� ��ȭ�� ã�ƿ� �� �����ϴ� ,,,,";
        dialogue[2] = playerName + " ���� ������ ����ϱ� ���� ����ϰڽ��ϴ� ..!!";
        dialogue[3] = "\tThe End\t";
        dialogue[4] = "���� �÷��� ���ּż� �����մϴ� !";
        // ��ȭ ���� null�� �־ ��ȭ�� �������� �˸���.
        dialogue[5] = "";
        dialogue[6] = "end";
    }
    private void Start()
    {
        target = GameObject.Find("Main Camera").GetComponent<Cameracontroller>().target;
        dragon_dead = GameObject.FindGameObjectWithTag("Dragon").GetComponent<Dragoncontroller>().dragonlive;
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
        if (dragon_dead==false && count < 6)
        {

            GameObject.Find("End").gameObject.SetActive(true);
            GameObject.Find("End").transform.Find("Ends").gameObject.SetActive(true);
            GameObject.Find("Ends").transform.Find("Texts").gameObject.SetActive(true);
            GameObject.Find("Ends").transform.Find("Target").gameObject.SetActive(true);
            if(GameObject.Find("End").activeSelf == true)
            {
                target = GameObject.Find("Target").transform;
            }
        }
        if (count >= 6)
        {
            GameObject.Find("End").gameObject.SetActive(false);
            GameObject.Find("End").transform.Find("Ends").gameObject.SetActive(false);
            GameObject.Find("Ends").transform.Find("Texts").gameObject.SetActive(false);
            GameObject.Find("Ends").transform.Find("Target").gameObject.SetActive(false);
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
        // count �� 12�� ������ ��ȭ �������̶� ���� �д�.
        if (isDialogue && count != 6)
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
