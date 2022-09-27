using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    private Animator animator;
    private Transform target;

    private bool isDead = false;
    private bool isHit; // �÷��̾�� ���� ��
    private bool isAttack; // �÷��̾ ���� ��
    private Rigidbody rigid;

    public GameObject playerObject;
    public MainPlayer player;

    public int damage; // ���� �ִ� ����
    public int HP = 100; // ���� ü��
    public Slider HPBar; // �� ü�¹�
    public Text textName; // ���� �̸�
    public float enemySpeed = 2f; // ���� ���ǵ�
    public SphereCollider collider; // �� ���� ����
    

    // Start is called before the first frame update
    void Start()
    {
        //playerAnimator = playerObject.GetComponent<Animator>();
        collider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
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
        if (player.isDie) return;
        if (target != null && !isDead) // Ÿ���� ������ ��
        {
            if (!isAttack)
            {
                Vector3 direction = transform.position - target.position;
                transform.Translate(direction.normalized * enemySpeed * Time.deltaTime); // Ÿ���� ���� �̵�
            }
            // Ÿ���� ���� ȸ��
            Vector3 dir = target.transform.position - transform.position;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5);
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
        if (player.isDie)
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Battle", false);
            return;
        }
        float distance = Vector3.Distance(playerObject.transform.position, transform.position);

        if (distance < 6.0f) // ���� ĳ���Ϳ� ���� �Ÿ��� 6 ���� �϶�
        {
            HPBar.value = HP;
            HPBar.gameObject.SetActive(true);
            textName.text = "������";
            animator.SetBool("Battle", true);

            if (distance < 1.0f)
            {
                animator.SetBool("Attack", true);
                StopCoroutine(Attack());
                StartCoroutine(Attack());
            }
            else
            {
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
        yield return new WaitForSeconds(1f);
        collider.enabled = true;
        isAttack = true;
        yield return new WaitForSeconds(1f);
        collider.enabled = false;
        isAttack = false;
        yield return new WaitForSeconds(1f);

    }

    private void SlimeDead()
    {
        if (HP <= 0)
        {
            isDead = true;
            HPBar.gameObject.SetActive(false);
            textName.text = "";
            Destroy(gameObject, 1.5f);
            animator.SetTrigger("Die");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sward") && player.isAttack && !isHit) // �÷��̾�� �¾��� ��
        {
            HP -= 35;
            animator.SetTrigger("Hit");
            isHit = true;
            Invoke("hitOut", 1f);
        }
        else if (other.gameObject.CompareTag("Player") && isAttack && !player.isHit) // �÷��̾ ������ �� 
        {
            player.playerHP -= damage;
            player.isHit = true;
            Invoke("PlayerDamagedOut", 1f);
        }
    }
    void hitOut()
    {
        isHit = false;
    }

    void PlayerDamagedOut()
    {
        player.isHit = false;
    }

    void FreezeRotation() // Enemy �ڵ� ȸ�� ����
    {
        rigid.angularVelocity = Vector3.zero; // ���� ȸ�� �ӵ�
    }

    private void FixedUpdate()
    {
        FreezeRotation();
    }
}
