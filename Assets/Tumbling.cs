using UnityEngine;

public class Tumbling
{
    public float uprightForce = 30f;  // �Ͼ�� �� (��ũ)
    public float maxTiltAngle = 0.1f;  // �ִ� ��� ���� ����
    public float torqueMultiplier = 10f;  // ��ũ�� ���� ����
    private Rigidbody rb;
    

    // �����ڿ��� Rigidbody�� �޾ƿ�
    public Tumbling(Rigidbody rigidbody)
    {
        rb = rigidbody;
    }

    // ĳ���Ͱ� ���������� Ȯ���ϴ� �޼���
    public bool IsTilted()
    {
        // ĳ������ Up ���Ϳ� ���� Up ���� ���� ���� ���
        float tiltAngle = Vector3.Angle(rb.transform.up, Vector3.up);
        return tiltAngle > maxTiltAngle;
    }

    // ĳ���͸� �ڵ����� ������ ����Ű�� ���� (��ũ ���)
    public void RightCharacter()
    {
        if (IsTilted()) // �������� ���� �۵�
        {
            // ���� ����� ȸ�� ���� ���
            Vector3 uprightTorqueDirection = Vector3.Cross(rb.transform.up, Vector3.up);

            // ��ũ�� ���Ͽ� ĳ���͸� ����
            rb.AddTorque(uprightTorqueDirection * uprightForce * torqueMultiplier, ForceMode.Acceleration);
        }
    }
}
