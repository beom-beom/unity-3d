using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public GameObject collision;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        

    }
    public void OnMovement(float horizontal, float vertical)
    {
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
    }
    public void OnSword()
    {
        animator.SetTrigger("onsword");
    }
    public void OnShield()
    {
        
        animator.SetBool("shield",true);
    }
    public void OnSpell()
    {
         
        animator.SetTrigger("spell");
    }
    public void OnAttackCollision()
    {
        collision.SetActive(true);
      
    }
    public void OnDodge()
    {
        animator.SetBool("Dodge",true);      
    }
}
