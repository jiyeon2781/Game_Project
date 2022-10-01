using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPlayer : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private Vector2 mouseInput;

    public Weapon weapon;
    public bool isStart; // ������ ���۵ƴ��� �ƴ���
    public bool isAttack; // �����ϰ� �ִ��� �ƴ���
    public bool isHit; // �÷��̾ �°� �ִ��� �ƴ���
    public bool isDie = false;

    public float charSpeed = 5.0f; // ĳ���� �ӵ�
    public Transform characterBody; // ���� ĳ����
    public Transform cameraArm; // ���� ĳ������ ī�޶�
    public int playerHP = 100;
    public Slider slider;
    public GameManager manager;

    MeshRenderer[] meshs;

    // Start is called before the first frame update
    void Start()
    {
        rb = characterBody.GetComponent<Rigidbody>();
        animator = characterBody.GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Attack();
        Health();
        Dead();
        Clear();
    }

    private void FixedUpdate() // �̵� ���� �Լ��� FixedUpdate�� ȿ���� �� ���ٰ� ��
    {
        Move();
    }

    void Move()
    {
        if (isAttack || isDie || !isStart) return;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // �̵� ����Ű �Է°� (���� ����, ���� ����)
        bool isWalk = moveInput.magnitude != 0; // �̵� ����Ű �Է� ���� (0���� ũ�� �Է� �߻�)
        animator.SetBool("isWalk", isWalk); // �ִϸ��̼� ����

        if (isWalk)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            // ī�޶� �ٶ󺸴� ����
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            // ī�޶� ������ ����
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            // �̵� ����

            characterBody.forward = moveDir; // �̵��� �� �̵����� �ٶ󺸰� ����
            transform.position += moveDir * Time.deltaTime * charSpeed; // �̵�
        }
    }
    void Rotate()
    {
        if (!isStart || isDie) return;
        mouseInput.x = Input.GetAxis("Mouse X"); // ���콺 �¿� ��ġ
        mouseInput.y = Input.GetAxis("Mouse Y"); // ���콺 ���Ʒ� ��ġ
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        // ī�޶��� ���� ������ ���Ϸ� ������ ����
        float x = camAngle.x - mouseInput.y;
        // ī�޶��� ��ġ �� ���

        // ĳ���� ī�޶� ���� ������ ���� (�������� 70��, �Ʒ������� 25�� �̻� �������� ���ϰ� ����)
        if (x < 180f) x = Mathf.Clamp(x, -1f, 70f);
        else x = Mathf.Clamp(x, 335f, 361f);

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseInput.x, camAngle.z);
        // ī�޶� �� ȸ��
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && isStart)
        {
            animator.SetTrigger("Attack");
            isAttack = true;
            weapon.Use();
            Invoke("AttackOut", 0.5f);
        }
    }

    void AttackOut()
    {
        isAttack = false;
    }

    void Health()
    {
        if (playerHP <= 0)
        {
            isDie = true;
        }
    }

    void Dead()
    {
        if (!isDie) return;
        animator.SetTrigger("isDie");
        manager.gameOver();

    }

    void Clear()
    {
        if (manager.enemyCount <= 0 && isStart)
        {
            isStart = false;
            animator.SetTrigger("doClear");
        }
    }

}

