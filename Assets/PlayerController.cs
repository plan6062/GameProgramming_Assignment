using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public float moveSpeed = 5f;      // 캐릭터 이동 속도
    public float turnSpeed = 720f;    // 캐릭터 회전 속도

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // 입력값 받아오기 (WASD 또는 화살표 키)
        float horizontal = Input.GetAxis("Horizontal"); // X축 방향 이동 (A, D 또는 좌우 화살표)
        float vertical = Input.GetAxis("Vertical");     // Z축 방향 이동 (W, S 또는 상하 화살표)

        // 월드 좌표 기준으로 이동 벡터 계산
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized * moveSpeed;

        // Rigidbody에 이동 처리 (Y축은 중력의 영향을 유지)
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // 이동 방향이 있을 때 캐릭터가 해당 방향을 바라보도록 회전
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}
