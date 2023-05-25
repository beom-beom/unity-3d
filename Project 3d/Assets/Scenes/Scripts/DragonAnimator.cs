using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimator : MonoBehaviour
{
    private Animator animator;
    public GameObject collision;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void OnAttack()
    {

        animator.SetTrigger("onAttack");
    }
    public void OnFlame()
    {
        animator.SetTrigger("onflame");
        
    }
    public void OnAttackCollision()
    {
        collision.SetActive(true);

    }
   
}
