using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Animator playerAnimator;
    private Animator animator;
    private GameObject playerObject;
    private Transform target;

    private bool deadFlag = false;
    private bool attackFlag = false;

    private Rigidbody rigid;


    public int HP = 100;
    public Slider HPBar;
    public Text textName;
    public float enemySpeed = 2f;
    public SphereCollider collider;


    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("MainPlayer");
        rigid = playerObject.GetComponent<Rigidbody>();
        playerAnimator = playerObject.GetComponent<Animator>();
        collider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.25f); // 타겟이 범위에 들어왔는지 주기적으로 검사
    }

    // Update is called once per frame
    void Update()
    {
        HPMark();
        SlimeDead();
        TargetConfirm();
    }

    private void TargetConfirm() // 타겟 설정
    {
        if (target != null && !deadFlag) // 타겟이 존재할 때
        {
            if (!attackFlag)
            {
                Vector3 direction = transform.position - target.position;
                transform.Translate(direction.normalized * enemySpeed * Time.deltaTime); // 타겟을 향해 이동
            }
            // 타겟을 향해 회전
            Debug.Log("hi");
            Vector3 dir = target.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5);
            //transform.LookAt(target.transform.position);
        }
    }

    private void UpdateTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f); // 일정 거리 안 콜라이더 확인
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Player")
                {
                    target = colliders[i].gameObject.transform; // 타겟 저장
                    break;
                }
                else target = null;
            }
        }

    }

    private void HPMark()
    {
        float distance = Vector3.Distance(playerObject.transform.position, transform.position);
        HPBar.value = HP;

        if (distance < 6.0f) // 메인 캐릭터와 적의 거리가 6 이하 일때
        {
            HPBar.gameObject.SetActive(true);
            textName.text = "Slime";
            animator.SetBool("Battle", true);

            if (distance < 1.0f)
            {
                attackFlag = true;
                animator.SetBool("Attack", true);
                StopCoroutine(Attack());
                StartCoroutine(Attack());
            }
            else
            {
                attackFlag = false;
                animator.SetBool("Attack", false);
                StopCoroutine(Attack());
            }
        }
        else if (distance >= 6.0f)
        {
            HPBar.gameObject.SetActive(false);
            textName.text = "";
            animator.SetBool("Battle", false);
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.2f);
        collider.enabled = true;
        yield return new WaitForSeconds(1f);
        collider.enabled = false;
        yield return new WaitForSeconds(0.5f);

    }

    private void SlimeDead()
    {
        if (HP <= 0)
        {
            deadFlag = true;
            HPBar.gameObject.SetActive(false);
            textName.text = "";
            Destroy(gameObject, 1.5f);
            animator.SetTrigger("Die");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sward") && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            HP -= 35;
            Debug.Log("몬스터가 맞았어!");
            animator.SetTrigger("Hit");
        }
    }
}
