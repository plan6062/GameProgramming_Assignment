using System.Data;
using UnityEngine;

public class Corn : MonoBehaviour // ������ ������Ʈ
{
    public CornState state;
    Rigidbody rb;
    bool isGround;
    float pushForce = 3f;     // �浹 �� ���� ���� ũ��
    float upwardForce = 0.3f; // Y�� �������� ���� �߰� ��
    float explosionForce = 3f; //���� ��
    float explosionRadius = 7f; // ���� �ݰ�
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor")) //���߿� ���ִ� �ٴڿ� ���� ���
        {
            isGround = true;
            state = CornState.IDLE;
            /*if (state == CornState.FLOAT) {
                state = CornState.IDLE;
                isGround = true;
            }
            if (state == CornState.BLOW)
            {
                state = CornState.FALL; //���߿� ���ư��� �Ѿ���
                WakeUp();
                isGround = true;
            }*/
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Corn") && isGround)
        {
            //isGround = false;
            state = CornState.FLOAT;

            // �浹 �ݴ� ���� ���
            Vector3 pushDirection = collision.transform.position - transform.position;

            // �ݴ� ���� + �ణ �������� ���� �߰�
            Vector3 forceDirection = (pushDirection.normalized + Vector3.up * upwardForce).normalized;

            // �浹 ��ü�� �� ���� (ForceMode.Impulse�� �������� ���� ����)
            collision.rigidbody.AddForce(forceDirection * pushForce, ForceMode.Impulse);
        }

        if (state != CornState.POP && collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) //������ ���� ���
        {
            state = CornState.POP;
            PopMesh.SetActive(true);
            CornMesh.SetActive(false);
            Explode(); //���� ����
        }
    }

    void Explode()
    {
        Debug.Log("Explode!!");
        PopEffect.SetActive(true);
        // ���� ���� ���� ��� �ݶ��̴� Ž��
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, explosionRadius);

        // �� ������Ʈ�� ���߷� ����
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
                // ���� ���� ���� ���
                Vector3 explosionDir = (rb.transform.position - transform.position).normalized;

                // Y������ �������� ���� (1.5f�� ������, ���ϴ� ������ ���� ����)
                explosionDir.y += 1.5f; // Y�� ������ �߰� (����: 1.5)

                // ������ ���߷� ����
                rb.AddForce(explosionDir * explosionForce, ForceMode.Impulse);
            }
        }

        GetComponent<AIController>().enabled = false;

        // ���� �ݶ��̴� ����
        Collider currentCollider = GetComponent<Collider>();
        if (currentCollider != null)
        {
            Destroy(currentCollider);
        }

        // ���ο� SphereCollider �߰� �� ����
        SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.center = new Vector3(0, 1.25f, 0); // Y������ 1.25��ŭ �ø�
        sphereCollider.radius = 2f; // �������� 2�� ����\
    }

    void WakeUp()
    {
        //�Ѿ��� ���¿��� �Ͼ�� �ִϸ��̼� �߰�
        state = CornState.IDLE;
    }
}
