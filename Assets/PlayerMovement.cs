using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float dashSpeed = 10f; // 대시 속도
    public float dashDuration = 0.2f; // 대시 지속 시간
    private Vector2 movementValue;
    private float lookValue;
    private bool isDashing = false;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>() * speed;
    }

    public void OnLook(InputValue value)
    {
        lookValue = value.Get<Vector2>().x * rotationSpeed;
    }

    public void OnDash(InputValue value)
    {
        if (value.isPressed && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    void Start()
    {
        // Start 메서드는 그대로 유지
    }

    void Update()
    {
        if (!isDashing)
        {
            transform.Translate(
                movementValue.x * Time.deltaTime, 0, movementValue.y * Time.deltaTime
            );
        }

        transform.Rotate(0, lookValue * Time.deltaTime, 0);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        Vector3 dashDirection = transform.forward * dashSpeed;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            transform.Translate(dashDirection * Time.deltaTime, Space.World);
            yield return null;
        }

        isDashing = false;
    }
}