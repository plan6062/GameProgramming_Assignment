using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionForce = 1000f;
    public float explosionRadius = 5f;
    public Vector3 explosionPosition;

    void OnEnable()
    {
        explosionPosition = gameObject.transform.position;
    }

    void Explode()
    {
        Debug.Log("Explode!!");
        // 폭발 범위 내의 모든 콜라이더 탐색
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        // 각 오브젝트에 폭발력 적용
        foreach (Collider nearbyObject in colliders)
        {
            // 폭발 오브젝트 자신을 제외
            /*if (nearbyObject.gameObject == gameObject)
                continue;*/

            //Debug.Log(nearbyObject.name);
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바를 누르면 폭발
        {
            explosionPosition = transform.position; // 폭발 위치 설정
            Explode(); // 폭발 실행
        }
    }
}
