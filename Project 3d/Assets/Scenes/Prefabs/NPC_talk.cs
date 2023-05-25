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
    // �������� �� �� �ְ� ���ش�.
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

    // ��ȭ�� �󸶳� ���� �Ǿ����� Ȯ��
    private int count = 0;

    static private String[] dialogue; // ��ȭ�� ���� �迭

    private void Awake()
    {
        dialogue = new String[100];
        // ó�� intro ���
        dialogue[0] = "�ȳ��ϼ��� " +playerName+"��"+"  �ݰ�����";
        dialogue[1] = playerName+"�� ���� �������� �巡���� �����ؿԽ��ϴ�.";
        dialogue[2] = "���� �巡���� ���� �����ο� �ڸ��� ��� �ֽ��ϴ�.";
        dialogue[3] = "�ε� �巡���� ��� ������ �������ּ���.....";
        dialogue[4] = "������ ����"+playerName+"���� ���� �巡��� �����ϱ⿣ �����Ͻ� �� �����ϴ�.";
        dialogue[5] = "�巡��� �ο�� ���� �����س��� �Ʒ����� ����"+playerName+"�� ������ ���ڽ��ϴ�.";
        dialogue[6] = "�Ʒ����� ��Ż�� ���� �����Ͻ� �� �ֽ��ϴ�.";
        dialogue[7] = "�׷�"+playerName+"���� ������ ���ϴ� ...";
        dialogue[8] = "�ε� �巡���� ������ ������ �����ּ��� !!!";
        // ��ȭ ���� null�� �־ ��ȭ�� �������� �˸���.
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
        // count �� 12�� ������ ��ȭ �������̶� ���� �д�.
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
