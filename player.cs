using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gpt_player : MonoBehaviour
{
    // ĳ���� �̵� �ӵ�
    public float moveSpeed = 5f;

    // ĳ���� ������
    public float jumpForce = 5f;

    // ĳ���� �޸��� �ӵ�
    public float runSpeed = 10f;

    // ĳ���� ��� ��
    public float health = 3.0f;

    // ĳ���� �̵� ����
    private Vector3 moveDirection = Vector3.zero;

    // ĳ���� ���� ���� ����
    private bool isJumping;

    // ĳ���� ������ٵ�
    private Rigidbody rb;

    public bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJumping = false;
        isGrounded = true;
    }

    void Update()
    {
        // �̵� ������ ���
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // �̵� ������ ����ȭ
        moveDirection.Normalize();

        // �̵� ���⿡ �̵� �ӵ��� ���Ͽ� ĳ���͸� �̵�
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // �޸���
        if (Input.GetKeyDown(KeyCode.E))
        {
            moveSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            moveSpeed = 5f;
        }

        Jump();

        // ȸ��
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0f, mouseX*2.0f, 0f);
        // ���
        if (health <= 0)
        {
            // ���� ���� ó��
            Debug.Log("Game Over");
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
        }
    }

    void Jump()
    {
        //�����̽� Ű�� ������ ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //�ٴڿ� ������ ������ ����
            if (!isJumping)
            {
                //print("���� ���� !");
                isJumping = true;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            //���߿� ���ִ� �����̸� �������� ���ϵ��� ����
            else
            {
                //print("���� �Ұ��� !");
                return;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isJumping = false;
        }
        // �浹�� ��ü�� Enemy1�� ��� ��� -0.5
        if (collision.gameObject.tag == "Enemy")
        {
            health -= 0.5f;
        }
        // �浹�� ��ü�� Enemy2�� ��� ��� -1
        else if (collision.gameObject.tag == "Enemy2")
        {
            health -= 1f;
        }
        //�ٴڿ� ������
        if (collision.gameObject.CompareTag("ground"))
        {
            //������ ������ ���·� ����
            isJumping = false;
        }
    }
}
