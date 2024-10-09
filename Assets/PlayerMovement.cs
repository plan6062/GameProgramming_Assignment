using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float boostedSpeed = 10f;
    public float rotationSpeed = 120f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    
    private float currentSpeed;
    private Vector2 movementValue;
    private float lookValue;
    private bool isDashing = false;
    private bool isSpeedBoosted = false;

    private void Start()
    {
        currentSpeed = normalSpeed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>();
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

    void Update()
    {
        // 회전 적용
        transform.Rotate(0, lookValue * Time.deltaTime, 0);

        if (!isDashing)
        {
            // 이동 벡터 계산 (로컬 공간 기준)
            Vector3 movement = transform.right * movementValue.x + transform.forward * movementValue.y;
            
            // 정규화하여 대각선 이동 시 속도 보정
            if (movement.magnitude > 1f)
            {
                movement.Normalize();
            }

            // 이동 적용 (로컬 공간 기준)
            transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBoostItem") && !isSpeedBoosted)
        {
            StartCoroutine(ApplySpeedBoost());
            Destroy(other.gameObject); // 아이템 제거
        }
    }

    private IEnumerator ApplySpeedBoost()
    {
        isSpeedBoosted = true;
        currentSpeed = boostedSpeed;
        
        yield return new WaitForSeconds(3f);
        
        currentSpeed = normalSpeed;
        isSpeedBoosted = false;
    }
}