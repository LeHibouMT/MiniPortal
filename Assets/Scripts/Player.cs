using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody m_Rb;
    public CameraController cameracontroller;
    // Jumping parameters
    public float aJumpForce; // Jumping force
    public Vector3 boxSize; // hitBox to detect ground
    public float maxDistance; // set hitBox position
    public LayerMask layermask; // ground layer
    public bool isGrounded;
    public bool mustJump;
    // Moving parameters
    [SerializeField] float m_TranslationSpeed; //in m/s
    public Vector3 forwardVector;
    public Vector3 sideVector;
    public Vector3 movementVector;

    void Awake() 
    {
        m_Rb = GetComponent<Rigidbody>();
        aJumpForce = 175f;
        boxSize = new Vector3(1f, 0.2f, 1f);
        maxDistance = 1f;
        m_TranslationSpeed = 5f;
        forwardVector = new Vector3(0f, 0f, 0f);
        sideVector = new Vector3(0f, 0f, 0f);
        movementVector = new Vector3(0f, 0f, 0f);
    }

    private bool GroundCheck()
    {
        if (Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, layermask))
        {
            isGrounded = true;
            return true;
        }
        else
        {
            isGrounded = false;
            return false;
        }
    }

    // for debugging and set GroundCheck properly
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }

    void Update()
    {
        // Moving
        // forward
        if (Input.GetKey(KeyCode.Z))
        {
            forwardVector = cameracontroller.transform.forward * m_TranslationSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.Z))
            {
                forwardVector = new Vector3(0.0f, 0.0f, 0.0f);
            }
            else
            {
                forwardVector = -cameracontroller.transform.forward * m_TranslationSpeed;
            }
        }
        // lateral
        if (Input.GetKey(KeyCode.Q))
        {
            sideVector = -cameracontroller.transform.right * m_TranslationSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.Q))
            {
                sideVector = new Vector3(0.0f, 0.0f, 0.0f);
            }
            else
            {
                sideVector = cameracontroller.transform.right * m_TranslationSpeed;
            }
        }
        movementVector = forwardVector + sideVector;
        // Grounded
        if (GroundCheck())
        {
            // Jumping
            if (Input.GetKey(KeyCode.Space))
            {
                mustJump = true;
            }
            m_Rb.velocity = movementVector;
            forwardVector = new Vector3(0.0f, 0.0f, 0.0f); // reset vector
            sideVector = new Vector3(0.0f, 0.0f, 0.0f); // reset vector
        }
        // in the air
        else
        {
            m_Rb.AddForce(movementVector / 4 - transform.up, ForceMode.Force);
            forwardVector = new Vector3(0.0f, 0.0f, 0.0f); // reset vector
            sideVector = new Vector3(0.0f, 0.0f, 0.0f); // reset vector
        }
    }

    void FixedUpdate()
    {
        if (mustJump)
        {
            m_Rb.AddForce(aJumpForce * transform.up, ForceMode.Impulse);
            mustJump = false;
        }
    }
}
