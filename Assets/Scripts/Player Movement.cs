using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // movement values
    [Header("Movement Values")]
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float jumpSpeed = 10.0f;

    // references
    Rigidbody rb;
    [SerializeField] Animator animator;

    // variables
    Vector3 movementVector;
    bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        animator.SetFloat("walkSpeed", movementVector.magnitude);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        movementVector = new Vector3(inputVector.x, 0, inputVector.y);

        animator.transform.forward = movementVector.normalized;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
            rb.linearVelocity = movementVector.normalized * moveSpeed;
        }
    }
}
