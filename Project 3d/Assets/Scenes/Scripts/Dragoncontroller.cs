using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragoncontroller : MonoBehaviour
{ 
    public float speed = 5f;  // 용의 이동 속도
    public float attackDistance = 1f;  // 공격 거리
    public float attackRate = 1f;  // 공격 속도
    public float attackTimer = 0f;
    private int count = 0;
    private Animator anim;
    private Transform playerTransform;  // 플레이어 Transform
    private DragonAnimator dragonAnimator;  // 용 애니메이터
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
        Vector3 direction = (playerTransform.position - transform.position).normalized;  // 플레이어 방향으로 이동

        Quaternion targetRotation = Quaternion.LookRotation(direction);  // 플레이어 방향으로 회전
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
        float distance = Vector3.Distance(transform.position, playerTransform.position);  // 현재 위치와 플레이어 위치 사이의 거리 계산
        if (playerTransform == null) return;  // 플레이어 Transform이 할당되지 않았으면 Update를 끝냄

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

        else if ((distance > attackDistance && distance < 20) || acummulatedamage > 0)  // 그 외에는 플레이어를 추적
        {
            anim.SetBool("iswalk", true);

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    Vector3 direction = (playerTransform.position - transform.position).normalized;  // 플레이어 방향으로 이동
                    transform.position += direction * speed * Time.deltaTime;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);  // 플레이어 방향으로 회전
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
                }
                    if (distance <= attackDistance)  // 플레이어와 일정 거리 이내에 있으면
            {
                anim.SetBool("iswalk", false);
                    dragonAnimator.OnAttack();

                    Vector3 directions = playerTransform.position - transform.position;
                    directions.y = 0f; // y축 회전을 고려하지 않습니다.

                    // 드래곤이 플레이어를 바라보는 회전값을 계산합니다.
                    Quaternion targetRotations = Quaternion.LookRotation(directions);

                    // 드래곤의 회전을 부드럽게 보간합니다.
                    float rotationSpeed = 5f; // 회전 속도 조절을 위한 변수
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotations, rotationSpeed * Time.deltaTime);
                    if (Time.time > attackTimer)  // 공격 속도만큼 시간이 지났으면
                {
                    attackTimer = Time.time + 1f / attackRate;  // 다음 공격 시간을 공격 속도만큼 더해줌
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
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;  // 태그가 "Player"인 오브젝트를 찾아서 Transform을 가져옴
            dragonAnimator = GetComponent<DragonAnimator>();  // 현재 오브젝트에 있는 Animator 컴포넌트 가져옴
            anim = GetComponent<Animator>();  // 현재 오브젝트에 있는 Animator 컴포넌트 가져옴
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
