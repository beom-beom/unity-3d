using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class MummyController : MonoBehaviour
{
    public GameObject collision;
    public float speed = 5f;  // 용의 이동 속도
    public float attackDistance = 1f;  // 공격 거리
    public float attackRate = 1f;  // 공격 속도
    public float attackTimer = 0f;
    private SkinnedMeshRenderer meshRenderer;
    private Animator anim;
    private Color originColor;
    private Vector3 originPosition;
    private Transform playerTransform;
    private bool live = true;
    HealthSystemForDummies healthSystem;
    private Slider expSlider;
    private int acummulatedamage = 0;
    public void OnAttackCollision()
    {
        collision.SetActive(true);

    }
    void Awake()
    {
        anim = GetComponent<Animator>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originColor = meshRenderer.material.color;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // 태그가 "Player"인 오브젝트를 찾아서 Transform을 가져옴

        expSlider = GameObject.Find("exp").GetComponent<Slider>();
    }
    public void PlayerTarget()
    {
        if (playerTransform == null) return;  // 플레이어 Transform이 할당되지 않았으면 Update를 끝냄
        float distance = Vector3.Distance(transform.position, playerTransform.position);  // 현재 위치와 플레이어 위치 사이의 거리 계산
        if (healthSystem.CurrentHealth <= 0 && live == true)
        {
            anim.SetTrigger("die");
            Destroy(gameObject, 3.5f);
            expSlider.value += 3;
            live = false;

        }
        

        else if(distance<5 || acummulatedamage>0) // 그 외에는 플레이어를 추적
        {
            anim.SetBool("iswalk", true);

            Vector3 direction = (playerTransform.position - transform.position).normalized;  // 플레이어 방향으로 이동
            transform.position += direction * speed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(direction);  // 플레이어 방향으로 회전
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            if (distance <= attackDistance)  // 플레이어와 일정 거리 이내에 있으면
            {
                anim.SetBool("iswalk", false);
                anim.SetTrigger("Attack");
                if (Time.time > attackTimer)  // 공격 속도만큼 시간이 지났으면
                {
                    attackTimer = Time.time + 1f / attackRate;  // 다음 공격 시간을 공격 속도만큼 더해줌
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
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // 태그가 "Player"인 오브젝트를 찾아서 Transform을 가져옴
        anim = GetComponent<Animator>();  // 현재 오브젝트에 있는 Animator 컴포넌트 가져옴
        healthSystem = GetComponent<HealthSystemForDummies>();

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
