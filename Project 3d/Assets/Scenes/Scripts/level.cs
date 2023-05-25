using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class level : MonoBehaviour
{
    public Text text;
    private int levels;
    void Start()
    {
        levels = GameObject.Find("exp").GetComponent<fillexp>().level;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        text.text = "LV"+levels;
    }
}
