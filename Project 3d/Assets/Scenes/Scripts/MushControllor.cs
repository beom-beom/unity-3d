using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MushControllor : MonoBehaviour
{
    public GameObject collision;
    public float speed = 2f;  // ���� �̵� �ӵ�
    public float attackDistance = 1.5f;  // ���� �Ÿ�
    public float attackRate = 1f;  // ���� �ӵ�
    public float attackTimer = 0f;
    private SkinnedMeshRenderer meshRenderer;
    private Animator anim;
    private Color originColor;
    private Vector3 originPosition;
    private Transform playerTransform;
    private bool live=true;
    HealthSystemForDummies healthSystem;
    private int acummulatedamage = 0;

    private Slider expSlider;
    public void OnAttackCollision()
    {
        collision.SetActive(true);

    }
    void Awake()
    {
        anim = GetComponent<Animator>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originColor = meshRenderer.material.color;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // �±װ� "Player"�� ������Ʈ�� ã�Ƽ� Transform�� ������
        healthSystem = GetComponent<HealthSystemForDummies>();
        expSlider = GameObject.Find("exp").GetComponent<Slider>();
    }
    public void PlayerTarget()
    {
        if (playerTransform == null) return;  // �÷��̾� Transform�� �Ҵ���� �ʾ����� Update�� ����
        float distance = Vector3.Distance(transform.position, playerTransform.position);  // ���� ��ġ�� �÷��̾� ��ġ ������ �Ÿ� ���
        if (healthSystem.CurrentHealth <= 0 && live==true)
        {
            anim.SetTrigger("die");
            Destroy(gameObject, 1.2f);
            expSlider.value += 3;
            live = false;

        }
        

        else if(distance<5 ||acummulatedamage>0 )  // �� �ܿ��� �÷��̾ ����
        {
            anim.SetBool("iswalk", true);

            Vector3 direction = (playerTransform.position - transform.position).normalized;  // �÷��̾� �������� �̵�
            transform.position += direction * speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(direction);  // �÷��̾� �������� ȸ��
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            if (distance <= attackDistance)  // �÷��̾�� ���� �Ÿ� �̳��� ������
            {
           
                anim.SetBool("iswalk", false);
                anim.SetTrigger("Attack");
                if (Time.time > attackTimer)  // ���� �ӵ���ŭ �ð��� ��������
                {
                    attackTimer = Time.time + 1f / attackRate;  // ���� ���� �ð��� ���� �ӵ���ŭ ������
                }
            }
        }
    }
    public void TakeDamage(int damage, Vector3 hitDirection)
    {
        acummulatedamage += damage;

        anim.SetTrigger("hitted");
            healthSystem.AddToCurrentHealth(-damage);
            StartCoroutine("OnHitColor");
        
    }
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // �±װ� "Player"�� ������Ʈ�� ã�Ƽ� Transform�� ������
        anim = GetComponent<Animator>();  // ���� ������Ʈ�� �ִ� Animator ������Ʈ ������
    }

    private IEnumerator OnHitColor()
    {
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.color = originColor;
    }
    void FixedUpdate()
    {

        PlayerTarget();
    }
}
