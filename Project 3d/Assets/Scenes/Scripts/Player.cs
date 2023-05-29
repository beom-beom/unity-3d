using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public delegate void CmdAction();
public class Player : MonoBehaviour
{

    HealthSystemForDummies healthSystem;
    bool wDown;
    Animator anim;
    public Transform cameraTransform;
    private movements movement3D;
    private PlayerAnimator playerAnimator;
    public ParticleSystem particleObject;
    public enum AttackStateType { ready, swing }
    public AttackStateType attackStateType;
    private bool heal;
    private int level;
    private void Awake()
    {
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;  

        anim = GetComponentInChildren<Animator>();
        movement3D = GetComponent<movements>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
     }
    IEnumerator WaitForIt()
    {
        attackStateType = AttackStateType.swing;
        playerAnimator.OnSpell();
        yield return new WaitForSeconds(0.5f);
        particleObject.Play();
        
        yield return new WaitForSeconds(5.0f);
        attackStateType = AttackStateType.ready;
    }
    
    private void RecoverHP()
    {
            healthSystem.AddToCurrentHealth(5);
    }
    void Start()
    {
        level = GameObject.Find("exp").GetComponent<fillexp>().level;
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        healthSystem = GetComponent<HealthSystemForDummies>();
        attackStateType = AttackStateType.ready;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            InvokeRepeating("RecoverHP", 5f, 5f);
        }
    }
    public void TakeDamage(int damage, Vector3 hitDirection)
    {
        
        if (healthSystem.CurrentHealth != 0)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("shield"))
            {
                healthSystem.AddToCurrentHealth(0);
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("dodge"))
            {
                healthSystem.AddToCurrentHealth(0);
            }
            else
            {
                healthSystem.AddToCurrentHealth(-damage);
                anim.SetTrigger("hitted");
            }
        }
    }
   void FixedUpdate()
    {
        level = GameObject.Find("exp").GetComponent<fillexp>().level;

    }
    void Update()
    {

        
        if (healthSystem.CurrentHealth == 0)
        {
            anim.SetTrigger("die");
            Destroy(gameObject, 1.2f);
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        playerAnimator.OnMovement(x, z);
        movement3D.MoveSpeed = z > 0 ? 3.0f : 2.0f;
        // �̵� �Լ� ȣ�� (ī�޶� �����ִ� ������ �������� ����Ű�� ���� �̵�)
        movement3D.MoveTo(cameraTransform.rotation * new Vector3(x, 0, z));
      // ȸ�� ���� (�׻� �ո� ������ ĳ������ ȸ���� ī�޶�� ���� ȸ�� ������ ����)
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        wDown = Input.GetButton("Run");
        if (wDown && x == 0)
        {
            movement3D.MoveSpeed = z > 0 ? 9.0f : 4.0f;
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            playerAnimator.OnSword();
         }
        if (level > 1)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                movement3D.MoveTo(cameraTransform.rotation * new Vector3(0, 0, 0));
                playerAnimator.OnShield();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                anim.SetBool("shield", false);
            }
        }
        if (Input.GetButton("dodge"))
        {
            movement3D.moveSpeed *= 7;
            playerAnimator.OnDodge();
            movement3D.moveSpeed *= 0.5f;
        }
        else
        {
            anim.SetBool("Dodge", false);
        }
        if (level > 2)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (attackStateType == AttackStateType.ready)
                {
                    StartCoroutine(WaitForIt());
                }

            }
        }
    }
}
