using UnityEngine;

public class Elastic
{
    // �⺻ ƨ��� ���� ���
    public float baseBounceForce = 3f;

    // ���� �ٴ��� ���̾� �̸� ����
    public string[] excludedLayers = { "Wall", "Floor" };
    private int[] excludedLayerIndexes;
    private Rigidbody rb;

    // �����ڿ��� Rigidbody�� �޾ƿ�
    public Elastic(Rigidbody rigidbody, string[] excludedLayers)
    {
        rb = rigidbody;
        this.excludedLayers = excludedLayers;

        // ������ ���̾���� �ε��� ��������
        excludedLayerIndexes = new int[excludedLayers.Length];
        for (int i = 0; i < excludedLayers.Length; i++)
        {
            excludedLayerIndexes[i] = LayerMask.NameToLayer(excludedLayers[i]);
        }
    }

    // �浹 �� ź�� ȿ���� ó���ϴ� �޼���
    public void HandleCollision(Collision collision)
    {
        // �浹�� ������Ʈ�� �� �Ǵ� �ٴ����� Ȯ��
        if (!IsExcludedLayer(collision.gameObject.layer))
        {
            // �浹�� ��� ������Ʈ�� Rigidbody ��������
            Rigidbody otherRigidbody = collision.rigidbody;

            // ��밡 Rigidbody�� ������ ���� ���� �۵�
            if (otherRigidbody != null)
            {
                // �浹 �ӵ��� ��� (��� �ӵ�)
                Vector3 relativeVelocity = collision.relativeVelocity;
                float impactSpeed = relativeVelocity.magnitude;

                // �浹 �ӵ��� ���� ƨ��� ���� ���
                float bounceForce = baseBounceForce * impactSpeed;

                // ƨ��� ������ ���
                Vector3 bounceDirection = GetBounceDirection(otherRigidbody);

                // ��� ������Ʈ�� ƨ��� ���� ���ϱ�
                ApplyBounceForce(otherRigidbody, bounceDirection, bounceForce);
            }
        }
    }

    // ������ ���̾����� Ȯ���ϴ� �޼���
    private bool IsExcludedLayer(int layer)
    {
        foreach (int excludedLayer in excludedLayerIndexes)
        {
            if (layer == excludedLayer)
            {
                return true;
            }
        }
        return false;
    }

    // ƨ��� ������ ����ϴ� �޼���
    private Vector3 GetBounceDirection(Rigidbody otherRigidbody)
    {
        return (otherRigidbody.transform.position - rb.transform.position).normalized;
    }

    // ƨ��� ���� ���ϴ� �޼���
    private void ApplyBounceForce(Rigidbody otherRigidbody, Vector3 bounceDirection, float bounceForce)
    {
        otherRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
    }
}
