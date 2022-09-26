using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private bool isJump = false;
    private Rigidbody rb;
    private Animator animator;

    [SerializeField]
    private float jumpHeight = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Hello~");
            if (!isJump)
            {
                //rigidBody�� ���� ���ϴ� �Լ�
                // ���ڴ� ���ʴ�� ����, ���� ������� ���� ������ ǥ��
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                animator.SetTrigger("Jump");
                isJump = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        
        if (collision.gameObject.CompareTag("Floor")) isJump = false;
    }

}
