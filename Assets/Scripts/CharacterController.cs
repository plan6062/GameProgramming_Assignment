using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;      // 캐릭터 이동 속도
    public float jumpForce = 5f;      // 점프 힘
    public float turnSpeed = 300f;    // 캐릭터 회전 속도
    public Rigidbody rb;
    public bool isGrounded;           // 캐릭터가 땅에 있는지 확인

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;     // 회전을 고정시켜 넘어지지 않게 설정
    }

    void Update()
    {
        // 입력값 받아오기
        float horizontal = Input.GetAxis("Horizontal"); // A, D 회전
        float vertical = Input.GetAxis("Vertical");     // W, S 이동

        // 캐릭터 이동
        Vector3 movement = transform.forward * vertical * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z); // y축은 중력 영향을 유지

        // A, D에 따라 캐릭터 좌우 회전
        transform.Rotate(Vector3.up * horizontal * turnSpeed * Time.deltaTime);

        // 점프 처리
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  // 위쪽으로 힘을 가해 점프
            isGrounded = false;
        }
    }

    // 바닥과의 충돌 감지
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
