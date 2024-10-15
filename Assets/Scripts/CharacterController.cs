using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;      // ĳ���� �̵� �ӵ�
    public float jumpForce = 5f;      // ���� ��
    public float turnSpeed = 300f;    // ĳ���� ȸ�� �ӵ�
    public Rigidbody rb;
    public bool isGrounded;           // ĳ���Ͱ� ���� �ִ��� Ȯ��

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;     // ȸ���� �������� �Ѿ����� �ʰ� ����
    }

    void Update()
    {
        // �Է°� �޾ƿ���
        float horizontal = Input.GetAxis("Horizontal"); // A, D ȸ��
        float vertical = Input.GetAxis("Vertical");     // W, S �̵�

        // ĳ���� �̵�
        Vector3 movement = transform.forward * vertical * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z); // y���� �߷� ������ ����

        // A, D�� ���� ĳ���� �¿� ȸ��
        transform.Rotate(Vector3.up * horizontal * turnSpeed * Time.deltaTime);

        // ���� ó��
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  // �������� ���� ���� ����
            isGrounded = false;
        }
    }

    // �ٴڰ��� �浹 ����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
