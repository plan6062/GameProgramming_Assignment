using System.Data;
using UnityEngine;

public class Corn : MonoBehaviour // 옥수수 오브젝트
{
    public CornState state;
    Rigidbody rb;
    bool isGround;
    float pushForce = 3f;     // 충돌 시 가할 힘의 크기
    float upwardForce = 0.3f; // Y축 방향으로 가할 추가 힘
    float explosionForce = 3f; //폭발 힘
    float explosionRadius = 7f; // 폭발 반경
    public GameObject CornMesh;
    public GameObject PopMesh;
    public GameObject PopEffect;

    private void OnEnable()
    {
        state = CornState.IDLE;
        isGround = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        CornMesh.SetActive(true);
        PopMesh.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor")) //공중에 떠있다 바닥에 닿은 경우
        {
            isGround = true;
            state = CornState.IDLE;
            /*if (state == CornState.FLOAT) {
                state = CornState.IDLE;
                isGround = true;
            }
            if (state == CornState.BLOW)
            {
                state = CornState.FALL; //폭발에 날아가면 넘어짐
                WakeUp();
                isGround = true;
            }*/
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Corn") && isGround)
        {
            //isGround = false;
            state = CornState.FLOAT;

            // 충돌 반대 방향 계산
            Vector3 pushDirection = collision.transform.position - transform.position;

            // 반대 방향 + 약간 위쪽으로 힘을 추가
            Vector3 forceDirection = (pushDirection.normalized + Vector3.up * upwardForce).normalized;

            // 충돌 객체에 힘 적용 (ForceMode.Impulse로 순간적인 힘을 가함)
            collision.rigidbody.AddForce(forceDirection * pushForce, ForceMode.Impulse);
        }

        if (state != CornState.POP && collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) //적에게 닿은 경우
        {
            state = CornState.POP;
            PopMesh.SetActive(true);
            CornMesh.SetActive(false);
            Explode(); //팝콘 폭발
        }
    }

    void Explode()
    {
        Debug.Log("Explode!!");
        PopEffect.SetActive(true);
        // 폭발 범위 내의 모든 콜라이더 탐색
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, explosionRadius);

        // 각 오브젝트에 폭발력 적용
        foreach (Collider nearbyObject in colliders)
        {
            Corn c = nearbyObject.GetComponent<Corn>();
            if (c != null)
            {
                c.state = CornState.BLOW;
            }

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 폭발 방향 벡터 계산
                Vector3 explosionDir = (rb.transform.position - transform.position).normalized;

                // Y축으로 보정값을 더함 (1.5f는 보정량, 원하는 값으로 조정 가능)
                explosionDir.y += 1.5f; // Y축 보정량 추가 (예시: 1.5)

                // 보정된 폭발력 적용
                rb.AddForce(explosionDir * explosionForce, ForceMode.Impulse);
            }
        }

        GetComponent<AIController>().enabled = false;

        // 기존 콜라이더 제거
        Collider currentCollider = GetComponent<Collider>();
        if (currentCollider != null)
        {
            Destroy(currentCollider);
        }

        // 새로운 SphereCollider 추가 및 설정
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.center = new Vector3(0, 1.25f, 0); // Y축으로 1.25만큼 올림
        sphereCollider.radius = 2f; // 반지름을 2로 설정\
    }

    void WakeUp()
    {
        //넘어진 상태에서 일어나는 애니메이션 추가
        state = CornState.IDLE;
    }
}
