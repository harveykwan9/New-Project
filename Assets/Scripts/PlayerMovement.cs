using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;


    public float speed = 6f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public float turnSmoothing = 0.1f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float turnSmoothVelocity;
    private Vector3 fallVelocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        // checks if on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // character movement
        jumpMovement();
        axisMovement();
    }

    void axisMovement() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f ,vertical).normalized;
        Vector3 moveDirection = Vector3.zero;

        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,  ref turnSmoothVelocity, turnSmoothing);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        controller.Move(moveDirection.normalized * speed * Time.deltaTime);
    }

// legit not working and idek why, that its not grounded.
    void jumpMovement() {
        if (isGrounded) {
            fallVelocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            fallVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        fallVelocity.y += gravity * Time.deltaTime;
        controller.Move(fallVelocity * Time.deltaTime);
    }

    void dodgeMovement() {
        // todo
    }
    
}
