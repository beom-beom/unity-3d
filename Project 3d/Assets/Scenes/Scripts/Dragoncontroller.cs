using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragoncontroller : MonoBehaviour
{ 
    public float speed = 5f;  // ���� �̵� �ӵ�
    public float attackDistance = 1f;  // ���� �Ÿ�
    public float attackRate = 1f;  // ���� �ӵ�
    public float attackTimer = 0f;
    private int count = 0;
    private Animator anim;
    private Transform playerTransform;  // �÷��̾� Transform
    private DragonAnimator dragonAnimator;  // �� �ִϸ�����
    Transform target;
    public ParticleSystem particleObject;
    private  GameObject player;
    HealthSystemForDummies healthSystem;
    private bool cmt = true;
    private HealthSystemForDummies playerhealthSystem;
    private int acummulatedamage=0;
    public void TakeDamage(int damage, Vector3 hitDirection)
    {
        acummulatedamage += damage;
        if (acummulatedamage ==100)
        {
            anim.SetTrigger("hitted");
            acummulatedamage = 0;
        }
        healthSystem.AddToCurrentHealth(-damage);
    }
    private void Flame()
    {
        dragonAnimator.OnFlame();
        particleObject.Play();
        Vector3 direction = (playerTransform.position - transform.position).normalized;  // �÷��̾� �������� �̵�

        Quaternion targetRotation = Quaternion.LookRotation(direction);  // �÷��̾� �������� ȸ��
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Flame"))
        {
            particleObject.Stop();

        }
      
        
    }
    IEnumerator WaitForIt()
    {
        yield return new WaitForSeconds(3.0f);
    }
    private void settingbool()
    {
        GameObject.Find("End").GetComponent<end_scrpit>().dragon_dead = false;

    }
    public void PlayerTarget()
{

    if ((playerhealthSystem.CurrentHealth != 0))
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);  // ���� ��ġ�� �÷��̾� ��ġ ������ �Ÿ� ���
        if (playerTransform == null) return;  // �÷��̾� Transform�� �Ҵ���� �ʾ����� Update�� ����

        if (distance < 150 && distance > 120 && cmt == true)
        {
            anim.Play("Scream");
            cmt = false;
        }
        if (healthSystem.CurrentHealth <= 0)
        {
            anim.SetTrigger("die");
                Invoke("settingbool", 2.0f);       
            Destroy(gameObject, 2.1f);
        }
        else if (distance >= 20 && distance < 100)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                anim.SetBool("iswalk", false);
            }
            StartCoroutine(WaitForIt());
            Flame();
        }

        else if ((distance > attackDistance && distance < 20) || acummulatedamage > 0)  // �� �ܿ��� �÷��̾ ����
        {
            anim.SetBool("iswalk", true);

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    Vector3 direction = (playerTransform.position - transform.position).normalized;  // �÷��̾� �������� �̵�
                    transform.position += direction * speed * Time.deltaTime;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);  // �÷��̾� �������� ȸ��
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
                }
                    if (distance <= attackDistance)  // �÷��̾�� ���� �Ÿ� �̳��� ������
            {
                anim.SetBool("iswalk", false);
                    dragonAnimator.OnAttack();

                    Vector3 directions = playerTransform.position - transform.position;
                    directions.y = 0f; // y�� ȸ���� ������� �ʽ��ϴ�.

                    // �巡���� �÷��̾ �ٶ󺸴� ȸ������ ����մϴ�.
                    Quaternion targetRotations = Quaternion.LookRotation(directions);

                    // �巡���� ȸ���� �ε巴�� �����մϴ�.
                    float rotationSpeed = 5f; // ȸ�� �ӵ� ������ ���� ����
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotations, rotationSpeed * Time.deltaTime);
                    if (Time.time > attackTimer)  // ���� �ӵ���ŭ �ð��� ��������
                {
                    attackTimer = Time.time + 1f / attackRate;  // ���� ���� �ð��� ���� �ӵ���ŭ ������
                }
            }
        }

    }
    else 
    {
        particleObject.Stop();

    }
}
    void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // �±װ� "Player"�� ������Ʈ�� ã�Ƽ� Transform�� ������
            dragonAnimator = GetComponent<DragonAnimator>();  // ���� ������Ʈ�� �ִ� Animator ������Ʈ ������
            anim = GetComponent<Animator>();  // ���� ������Ʈ�� �ִ� Animator ������Ʈ ������
        healthSystem = GetComponent<HealthSystemForDummies>();
        player = GameObject.Find("Player");
        playerhealthSystem = player.GetComponent<HealthSystemForDummies>();
        GameObject.Find("End").GetComponent<end_scrpit>().dragon_dead = true;
    }
    void Update()
        {
            if (count == 0)
            {
                Invoke("PlayerTarget", 3.0f);
                count++;
            }
            PlayerTarget();
        }
    
}
