using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gpt_player : MonoBehaviour
{
    // 캐릭터 이동 속도
    public float moveSpeed = 5f;

    // 캐릭터 점프력
    public float jumpForce = 5f;

    // 캐릭터 달리기 속도
    public float runSpeed = 10f;

    // 캐릭터 목숨 수
    public float health = 3.0f;

    // 캐릭터 이동 방향
    private Vector3 moveDirection = Vector3.zero;

    // 캐릭터 점프 가능 여부
    private bool isJumping;

    // 캐릭터 리지드바디
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
        // 이동 방향을 계산
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // 이동 방향을 정규화
        moveDirection.Normalize();

        // 이동 방향에 이동 속도를 곱하여 캐릭터를 이동
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // 달리기
        if (Input.GetKeyDown(KeyCode.E))
        {
            moveSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            moveSpeed = 5f;
        }

        Jump();

        // 회전
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0f, mouseX*2.0f, 0f);
        // 목숨
        if (health <= 0)
        {
            // 게임 오버 처리
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
        //스페이스 키를 누르면 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //바닥에 있으면 점프를 실행
            if (!isJumping)
            {
                //print("점프 가능 !");
                isJumping = true;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            //공중에 떠있는 상태이면 점프하지 못하도록 리턴
            else
            {
                //print("점프 불가능 !");
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
        // 충돌한 객체가 Enemy1일 경우 목숨 -0.5
        if (collision.gameObject.tag == "Enemy")
        {
            health -= 0.5f;
        }
        // 충돌한 객체가 Enemy2일 경우 목숨 -1
        else if (collision.gameObject.tag == "Enemy2")
        {
            health -= 1f;
        }
        //바닥에 닿으면
        if (collision.gameObject.CompareTag("ground"))
        {
            //점프가 가능한 상태로 만듦
            isJumping = false;
        }
    }
}
