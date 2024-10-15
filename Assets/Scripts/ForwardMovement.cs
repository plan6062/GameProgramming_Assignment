using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 100f;  // 최대 사정거리
    public LayerMask enemyLayer;  // 적 레이어
    public LayerMask obstacleLayer;  // 장애물 레이어

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;  // 시작 위치 저장
        Debug.Log($"Enemy Layer: {enemyLayer.value}, Obstacle Layer: {obstacleLayer.value}");
    }

    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

        // 현재 위치와 시작 위치 사이의 거리 계산
        float distanceTraveled = Vector3.Distance(transform.position, startPosition);

        // 최대 사정거리를 넘어섰는지 확인
        if (distanceTraveled >= maxDistance)
        {
            Debug.Log("최대 사정거리 도달: 총알 제거");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        HandleCollision(other.gameObject);
    }

    void HandleCollision(GameObject collidedObject)
    {
        int collisionLayer = collidedObject.layer;
        Debug.Log($"Collision/Trigger detected with layer: {collisionLayer}");

        // 충돌한 오브젝트가 Enemy 레이어에 속하는지 확인
        if (((1 << collisionLayer) & enemyLayer) != 0)
        {
            Debug.Log("적과 충돌: 적 처치 및 총알 제거");
            // GameManager에 적 처치 알림
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EnemyDefeated();
            }

            // 적 오브젝트 삭제
            Destroy(collidedObject);

            // 총알 자신도 삭제
            Destroy(gameObject);
        }
        // 충돌한 오브젝트가 Obstacle 레이어에 속하는지 확인
        else if (((1 << collisionLayer) & obstacleLayer) != 0)
        {
            Debug.Log("장애물과 충돌: 총알 제거");
            // 총알 자신만 삭제
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"알 수 없는 레이어와 충돌: {collisionLayer}");
        }
    }
}