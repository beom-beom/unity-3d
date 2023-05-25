using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fillexp : MonoBehaviour
{
    public int level=1;
    private Slider expSlider;
    public ParticleSystem particleObject;
    public Text text;
    private movements movement3D;
    HealthSystemForDummies healthSystem;
    Animator anim;
    void Awake()
    {

        expSlider = GameObject.Find("exp").GetComponent<Slider>();
         anim = GameObject.Find("Player").GetComponentInChildren<Animator>();
        movement3D = GameObject.Find("Player").GetComponent<movements>();

        text.text = "LV : " + level.ToString();
        healthSystem = GameObject.Find("Player").GetComponent<HealthSystemForDummies>();
    }


    void FixedUpdate()
    {
       if(expSlider.value==15)
        {   
            level++;
            expSlider.value = 0;
            healthSystem.CurrentHealth = 100;
            text.text = "LV : " + level.ToString();
            anim.Play("LevelUp");
            particleObject.Play();
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("LevelUp"))
            {
                movement3D.moveSpeed = 0.0f;
            }
        }
    }
}
