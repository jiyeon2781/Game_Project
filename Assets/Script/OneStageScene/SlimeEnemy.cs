using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeEnemy : MonoBehaviour
{
    private Animator playerAnimator;
    private Animator animator;
    private GameObject playerObject;
    private Transform target;

    private bool deadFlag = false;
    private bool attackFlag = false;

    private Rigidbody rigid;


    [SerializeField]
    private int SlimeHP = 100;
    [SerializeField]
    private Slider HPBar;
    [SerializeField]
    private Text textName;
    [SerializeField]
    private float enemySpeed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("MainPlayer");
        rigid = playerObject.GetComponent<Rigidbody>();
        playerAnimator = playerObject.GetComponent<Animator>();
        animator = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.25f); // Ÿ���� ������ ���Դ��� �ֱ������� �˻�
    }

    // Update is called once per frame
    void Update()
    {
        HPMark();
        SlimeDead();
        TargetConfirm();
    }

    private void TargetConfirm() // Ÿ�� ����
    {
        if (target != null && !deadFlag) // Ÿ���� ������ ��
        {
            if (!attackFlag)
            {
                Vector3 direction = transform.position - target.position;
                transform.Translate(direction.normalized * enemySpeed * Time.deltaTime); // Ÿ���� ���� �̵�
            }
            // Ÿ���� ���� ȸ��
            Debug.Log("hi");
            Vector3 dir = target.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5);
            //transform.LookAt(target.transform.position);
        }
    }

    private void UpdateTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 6f); // ���� �Ÿ� �� �ݶ��̴� Ȯ��
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Player")
                {
                    target = colliders[i].gameObject.transform; // Ÿ�� ����
                    break;
                }
                else target = null;
            }
        }

    }

    private void HPMark()
    {
        float distance = Vector3.Distance(playerObject.transform.position,transform.position);
        HPBar.value = SlimeHP;

        if (distance < 6.0f) // ���� ĳ���Ϳ� ���� �Ÿ��� 6 ���� �϶�
        {
            HPBar.gameObject.SetActive(true);
            textName.text = "Slime";
            animator.SetBool("Battle", true);

            if (distance < 1.0f)
            {
                attackFlag = true;
                animator.SetBool("Attack", true);
            }
            else
            {
                attackFlag = false;
                animator.SetBool("Attack", false);
            }
        }
        else if (distance >= 6.0f)
        {
            HPBar.gameObject.SetActive(false);
            textName.text = "";
            animator.SetBool("Battle", false);
        }
    }

    private void SlimeDead()
    {
        if (SlimeHP <= 0) {
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
            SlimeHP -= 35;
            Debug.Log("���Ͱ� �¾Ҿ�!");
            animator.SetTrigger("Hit");
        }
    }
}
