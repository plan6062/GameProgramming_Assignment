using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // ���� ĳ������ Transform
    public Vector3 offset;    // ĳ���Ϳ� ī�޶� ������ �Ÿ� (������)
    public float minDistance = 0.5f; // ī�޶� ĳ���Ϳ� ������ �� �ִ� �ּ� �Ÿ�
    public LayerMask obstacleMask;   // ��ֹ��� ������ ���̾� ����ũ

    private Vector3 desiredPosition;

    void OnEnable()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // ī�޶� ��ġ�� ��ǥ ��ġ ��� (�⺻ ������ ��ġ)
        desiredPosition = target.position + offset;

        // ī�޶�� ĳ���� ���̿� ��ֹ��� �ִ��� Raycast�� Ȯ��
        RaycastHit hit;
        if (Physics.Raycast(target.position, (desiredPosition - target.position).normalized, out hit, offset.magnitude, obstacleMask))
        {
            // ��ֹ��� ���� ���, ��ֹ� ��ġ�� ī�޶� �� ������ ��ġ
            float distanceToObstacle = Vector3.Distance(target.position, hit.point) - 0.1f; // ��¦ ������ ��
            transform.position = target.position + (desiredPosition - target.position).normalized * Mathf.Max(distanceToObstacle, minDistance);
        }
        else
        {
            // ��ֹ��� ���� ���, �⺻ ��ġ�� ī�޶� �̵�
            transform.position = desiredPosition;
        }
    }
}
