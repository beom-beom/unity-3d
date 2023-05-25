using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class portal : MonoBehaviour
{
    public GameObject portals;
    private Transform Player;
    private int level;
    GameObject canvas;
    private bool move;
    private int a = 0;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        level = GameObject.Find("exp").GetComponent<fillexp>().level;
        canvas = GameObject.Find("NPC_canvas");
  }
    void FixedUpdate()
    {
        if (a == 1 && move == true)
        {
            Player.position = portals.transform.position + new Vector3(0, 0, 7);
            Player.rotation = portals.transform.rotation;
            move = false;
        }
        if (a == 2 && move == true)
        {
            Player.position = portals.transform.position + new Vector3(0, 0, 7);
            Player.rotation = portals.transform.rotation;
            move = false;

        }
        if (a == 3 && move == true)
        {
            Player.position = portals.transform.position + new Vector3(0, 0, 7);
            Player.rotation = portals.transform.rotation;
            move = false;

        }
        if (a == 4 && move == true)
        {
            Player.position = portals.transform.position + new Vector3(0, 0, 7);
            Player.rotation = portals.transform.rotation;
            move = false;

        }
        if (a == 5 && move == true)
        {
            Player.position = portals.transform.position + new Vector3(0, 0, 7);
            Player.rotation = portals.transform.rotation;
            move = false;

        }
        if (a == 6 && move == true)
        {
            Player.position = portals.transform.position + new Vector3(0, 0, 7);
            Player.rotation = portals.transform.rotation;
            move = false;

        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && !canvas.activeSelf)
        {
            if (portals.name == "portal B")
            {
                a = 1;
                move = true;
            }
            if (portals.name == "portal A")
            {
                a = 2;
                move = true;
            }
            if (portals.name == "portal D")
            {
                a = 3;
                move = true;
            }
            if (portals.name == "portal C")
            {
                a = 4;
                move = true;
            }
            if (portals.name == "portal E")
            {
                if (level >= 3)
                {
                    a = 5;
                    move = true;
                }
                else
                {
                    UnityEngine.Debug.Log("레벨이 부족합니다");
                }
            }

            if (portals.name == "portal F")
            {

                if (level >= 3)
                {
                    a = 6;
                    move = true;
                }
                else
                {
                    UnityEngine.Debug.Log("레벨이 부족합니다");
                }
            }

        }
        else
        {
            UnityEngine.Debug.Log("스토리를 진행해 주세요");
        }
    }

}
