using UnityEngine;

public class AIController : MonoBehaviour
{
    public float moveSpeed = 3f;      // AI �̵� �ӵ�
    public float turnSpeed = 720f;    // AI ȸ�� �ӵ�
    public float changeDirectionTime = 2f;  // ������ �����ϴ� �ð� ����
    public float pushForce = 5f;      // �浹 �� �о�� ��

    private Rigidbody rb;
    private Vector3 randomDirection;  // �����ϰ� ������ �̵� ����
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
            // ���� ���� Ÿ�̸Ӱ� ������ ���ο� ���� ���� ����
            directionChangeTimer -= Time.deltaTime;
            if (directionChangeTimer <= 0f)
            {
                SetRandomDirection();
            }

            // AI�� ������ �������� �̵�
            MoveInRandomDirection();
        }
    }

    // ������ ���� ����
    void SetRandomDirection()
    {
        // 360�� �� ������ ������ �̵� ���� ����
        float randomAngle = Random.Range(0f, 360f);
        randomDirection = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle)).normalized;
        directionChangeTimer = changeDirectionTime;
    }

    // ���� �������� AI �̵�
    void MoveInRandomDirection()
    {
        Vector3 movement = randomDirection * moveSpeed;

        // Rigidbody�� �̵� ó��
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // AI�� ������ ������ �ٶ󺸵��� ȸ��
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(randomDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

}
