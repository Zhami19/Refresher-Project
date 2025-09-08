using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.LightTransport;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    // movement values
    [Header("Movement Values")]
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float jumpSpeed = 10.0f;

    // references
    Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] Transform theCamera;

    // variables
    Vector3 movementVector;
    bool isGrounded = true;

    public UnityEvent OnRestore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        OnRestore.Invoke();
    }

    private void Update()
    {
        animator.SetFloat("walkSpeed", movementVector.magnitude);
        animator.SetBool("isGrounded", isGrounded);

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

        Vector3 cameraForward = theCamera.forward;
        Vector3 cameraSide = theCamera.right;

        cameraForward.y = 0;
        cameraSide.y = 0;   

        cameraForward.Normalize();
        cameraSide.Normalize();

        movementVector = (inputVector.x * cameraSide + inputVector.y * cameraForward);

        if (movementVector != Vector3.zero)
        {
            Vector3 direction = movementVector.normalized;
            transform.forward = direction;
        }
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
            rb.linearVelocity = movementVector * moveSpeed;
        }
    }
}
