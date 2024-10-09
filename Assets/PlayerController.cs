using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    public float moveSpeed = 5f;      // ĳ���� �̵� �ӵ�
    public float turnSpeed = 720f;    // ĳ���� ȸ�� �ӵ�

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // �Է°� �޾ƿ��� (WASD �Ǵ� ȭ��ǥ Ű)
        float horizontal = Input.GetAxis("Horizontal"); // X�� ���� �̵� (A, D �Ǵ� �¿� ȭ��ǥ)
        float vertical = Input.GetAxis("Vertical");     // Z�� ���� �̵� (W, S �Ǵ� ���� ȭ��ǥ)

        // ���� ��ǥ �������� �̵� ���� ���
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized * moveSpeed;

        // Rigidbody�� �̵� ó�� (Y���� �߷��� ������ ����)
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // �̵� ������ ���� �� ĳ���Ͱ� �ش� ������ �ٶ󺸵��� ȸ��
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}
