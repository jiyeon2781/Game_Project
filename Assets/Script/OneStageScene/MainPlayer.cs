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
    public bool isStart; // 게임이 시작됐는지 아닌지
    public bool isAttack; // 공격하고 있는지 아닌지
    public bool isHit; // 플레이어가 맞고 있는지 아닌지
    public bool isDie = false;

    public float charSpeed = 5.0f; // 캐릭터 속도
    public Transform characterBody; // 메인 캐릭터
    public Transform cameraArm; // 메인 캐릭터의 카메라
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

    private void FixedUpdate() // 이동 관련 함수는 FixedUpdate가 효율이 더 좋다고 함
    {
        Move();
    }

    void Move()
    {
        if (isAttack || isDie || !isStart) return;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // 이동 방향키 입력값 (수평 방향, 수직 방향)
        bool isWalk = moveInput.magnitude != 0; // 이동 방향키 입력 판정 (0보다 크면 입력 발생)
        animator.SetBool("isWalk", isWalk); // 애니메이션 세팅

        if (isWalk)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            // 카메라가 바라보는 방향
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            // 카메라 오른쪽 방향
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            // 이동 방향

            characterBody.forward = moveDir; // 이동할 때 이동방향 바라보게 세팅
            transform.position += moveDir * Time.deltaTime * charSpeed; // 이동
        }
    }
    void Rotate()
    {
        if (!isStart || isDie) return;
        mouseInput.x = Input.GetAxis("Mouse X"); // 마우스 좌우 수치
        mouseInput.y = Input.GetAxis("Mouse Y"); // 마우스 위아래 수치
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        // 카메라의 원래 각도를 오일러 각으로 저장
        float x = camAngle.x - mouseInput.y;
        // 카메라의 피치 값 계산

        // 캐릭터 카메라 상하 움직임 제한 (위쪽으로 70도, 아래쪽으로 25도 이상 움직이지 못하게 제한)
        if (x < 180f) x = Mathf.Clamp(x, -1f, 70f);
        else x = Mathf.Clamp(x, 335f, 361f);

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseInput.x, camAngle.z);
        // 카메라 암 회전
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

