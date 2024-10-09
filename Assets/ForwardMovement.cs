using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 100f;  // 최대 사정거리
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;  // 시작 위치 저장
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

        // 현재 위치와 시작 위치 사이의 거리 계산
        float distanceTraveled = Vector3.Distance(transform.position, startPosition);

        // 최대 사정거리를 넘어섰는지 확인
        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 "Enemy" 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 적 오브젝트 삭제
            Destroy(collision.gameObject);

            // 총알 자신도 삭제
            Destroy(gameObject);
        }
    }
}