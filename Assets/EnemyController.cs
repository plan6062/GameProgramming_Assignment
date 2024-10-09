using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;      // AI �̵� �ӵ�
    public float turnSpeed = 720f;    // AI ȸ�� �ӵ�
    public float changeDirectionTime = 2f;  // ������ �����ϴ� �ð� ����

    private Rigidbody rb;
    private Vector3 randomDirection;  // �����ϰ� ������ �̵� ����
    private float directionChangeTimer;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        SetRandomDirection();
    }

    void Update()
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
