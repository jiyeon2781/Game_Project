using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private Vector2 mouseInput;

    private Weapon weapon;

    [SerializeField]
    private float charSpeed = 5.0f; // ĳ���� �ӵ�
    [SerializeField]
    private Transform characterBody; // ���� ĳ����
    [SerializeField]
    private Transform cameraArm; // ���� ĳ������ ī�޶�

    // Start is called before the first frame update
    void Start()
    {
        rb = characterBody.GetComponent<Rigidbody>();
        animator = characterBody.GetComponentInChildren<Animator>();
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Attack();
    }

    private void FixedUpdate() // �̵� ���� �Լ��� FixedUpdate�� ȿ���� �� ���ٰ� ��
    {
        Move();
    }

    void Move()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // �̵� ����Ű �Է°� (���� ����, ���� ����)
        bool isWalk = moveInput.magnitude != 0; // �̵� ����Ű �Է� ���� (0���� ũ�� �Է� �߻�)
        animator.SetBool("isWalk",isWalk); // �ִϸ��̼� ����
        
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
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
    }

    
}
