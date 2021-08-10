using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public CinemachineFreeLook TPCam;
    public Transform cam;
    public Transform groundCheck;
    public Animator animator;
    public float speed = 6f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public float turnSmoothing = 0.1f;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public float cameraScrollSensitivity = 1.0f;
    [Tooltip("x = max radius, y = min radius for zoom")]
    public Vector2 maxMinOrbits = new Vector2(25.0f, 5.0f);

    private float turnSmoothVelocity;
    private Vector3 fallVelocity;
    private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        // checks if on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            
        // character movement
        jumpMovement();
        axisMovement();
        scrollZoom();
    }

    // Using UnityInput to control axis movement and camera following
    // Takes care of rotating the camera
    void axisMovement() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f ,vertical).normalized;
        Vector3 moveDirection = Vector3.zero;
            
        if (direction.magnitude >= 0.1f) {
            animator.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,  ref turnSmoothVelocity, turnSmoothing);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        } else {
            animator.SetBool("isRunning", false);
        }
            
        controller.Move(moveDirection.normalized * speed * Time.deltaTime);
    }

    // Ground check for collision and jump input listener
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

    // Scrolling in and out will adjust the orbital heights and orbital radii of the players Third Person Camera (TPCam)
    // If we are within the user defined limits, change the heights/radii of orbits
    // If we reached a limit, and we are scrolling away from this limit, allow the change of heights/radii of orbits 
    // Height scaling will be divided by 20 as it is more sensitive to changes, I could make this user defined later to tweek in game, but I already have too many Vars.
    void scrollZoom() {
        for (int i = 0; i < TPCam.m_Orbits.Length; i++) {
            if (TPCam.m_Orbits[i].m_Radius > maxMinOrbits.y && TPCam.m_Orbits[i].m_Radius < maxMinOrbits.x) {
                TPCam.m_Orbits[i].m_Radius -= Input.mouseScrollDelta.y * cameraScrollSensitivity;
                TPCam.m_Orbits[i].m_Height -= Input.mouseScrollDelta.y * (cameraScrollSensitivity / 20);
            } else if (TPCam.m_Orbits[i].m_Radius <= maxMinOrbits.y && Input.mouseScrollDelta.y * cameraScrollSensitivity < 0) {
                TPCam.m_Orbits[i].m_Radius -= Input.mouseScrollDelta.y * cameraScrollSensitivity;
                TPCam.m_Orbits[i].m_Height -= Input.mouseScrollDelta.y * (cameraScrollSensitivity / 20);
            } else if (TPCam.m_Orbits[i].m_Radius >= maxMinOrbits.x && Input.mouseScrollDelta.y * cameraScrollSensitivity > 0) {
                TPCam.m_Orbits[i].m_Radius -= Input.mouseScrollDelta.y * cameraScrollSensitivity;
                TPCam.m_Orbits[i].m_Height -= Input.mouseScrollDelta.y * (cameraScrollSensitivity / 20);
            }
        }
    }

    void lightAttacks() {
        if (isGrounded) {
            // TODO
        }
    }

    void dodgeMovement() {
        // TODO
    }
    
}
