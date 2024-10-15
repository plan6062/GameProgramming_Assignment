using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float boostedSpeed = 10f;
    public float reducedSpeed = 2.5f;
    public float rotationSpeed = 120f;
    public float dashForce = 20f;
    public float dashDuration = 0.2f;
    public float speedEffectDuration = 3f;

    public LayerMask speedBoostLayer;
    public LayerMask speedReduceLayer;

    private float currentSpeed;
    private Vector2 movementValue;
    private float lookValue;
    private bool isDashing = false;
    private bool isSpeedEffectActive = false;

    private Rigidbody rb;

    private void Start()
    {
        currentSpeed = normalSpeed;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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

    void FixedUpdate()
    {
        transform.Rotate(0, lookValue * Time.fixedDeltaTime, 0);

        if (!isDashing)
        {
            Vector3 movement = transform.right * movementValue.x + transform.forward * movementValue.y;
            if (movement.magnitude > 1f)
            {
                movement.Normalize();
            }
            rb.velocity = movement * currentSpeed;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        Vector3 dashDirection = transform.forward * dashForce;
        rb.AddForce(dashDirection, ForceMode.Impulse);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isSpeedEffectActive)
        {
            if (speedBoostLayer == (speedBoostLayer | (1 << other.gameObject.layer)))
            {
                StartCoroutine(ApplySpeedEffect(boostedSpeed));
                Destroy(other.gameObject);
            }
            else if (speedReduceLayer == (speedReduceLayer | (1 << other.gameObject.layer)))
            {
                StartCoroutine(ApplySpeedEffect(reducedSpeed));
                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator ApplySpeedEffect(float newSpeed)
    {
        isSpeedEffectActive = true;
        float originalSpeed = currentSpeed;
        currentSpeed = newSpeed;

        yield return new WaitForSeconds(speedEffectDuration);

        currentSpeed = originalSpeed;
        isSpeedEffectActive = false;
    }
}