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
        // ���� ���� ���� ��� �ݶ��̴� Ž��
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        // �� ������Ʈ�� ���߷� ����
        foreach (Collider nearbyObject in colliders)
        {
            // ���� ������Ʈ �ڽ��� ����
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
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽��ٸ� ������ ����
        {
            explosionPosition = transform.position; // ���� ��ġ ����
            Explode(); // ���� ����
        }
    }
}
