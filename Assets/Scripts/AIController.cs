using UnityEngine;

public class AIController : MonoBehaviour
{
    public float moveSpeed = 3f;      // AI 이동 속도
    public float turnSpeed = 720f;    // AI 회전 속도
    public float changeDirectionTime = 2f;  // 방향을 변경하는 시간 간격
    public float pushForce = 5f;      // 충돌 시 밀어내는 힘

    private Rigidbody rb;
    private Vector3 randomDirection;  // 랜덤하게 설정된 이동 방향
    private float directionChangeTimer;

    Corn corn;

    void OnEnable()
    {
        corn = GetComponent<Corn>();
        rb = GetComponent<Rigidbody>();
        SetRandomDirection();
    }

    void Update()
    {
        if (corn.state == CornState.IDLE || corn.state == CornState.RUN)
        {
            // 방향 변경 타이머가 끝나면 새로운 랜덤 방향 설정
            directionChangeTimer -= Time.deltaTime;
            if (directionChangeTimer <= 0f)
            {
                SetRandomDirection();
            }

            // AI를 랜덤한 방향으로 이동
            MoveInRandomDirection();
        }
    }

    // 랜덤한 방향 설정
    void SetRandomDirection()
    {
        // 360도 중 랜덤한 각도로 이동 방향 설정
        float randomAngle = Random.Range(0f, 360f);
        randomDirection = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle)).normalized;
        directionChangeTimer = changeDirectionTime;
    }

    // 랜덤 방향으로 AI 이동
    void MoveInRandomDirection()
    {
        Vector3 movement = randomDirection * moveSpeed;

        // Rigidbody에 이동 처리
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // AI가 랜덤한 방향을 바라보도록 회전
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(randomDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

}
