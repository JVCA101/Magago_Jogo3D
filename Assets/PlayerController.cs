using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarriorAnimsFREE;

public class PlayerController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private Animator animator;

    [Header("Movement settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private int jumpCount = 0;

    [Header("Camera settings")]
    [SerializeField] private float eyeSpeed = 5f;
    private Quaternion baseOrientation;
    // private Quaternion lookingDirection;
    private float mouseH = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject child = transform.GetChild(0).gameObject;
        animator = child.GetComponent<Animator>();

        baseOrientation = transform.localRotation;  
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * speed;
        float dz = Input.GetAxis("Vertical") * speed;
        Vector3 velocity = new Vector3(dx, 0, dz);
        if(dx != 0 || dz != 0)
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("Velocity", velocity.magnitude);
            // if(velocity.magnitude > 1f)
            //     transform.rotation = Quaternion.LookRotation(velocity);
        }
        else
        {
            animator.SetBool("Moving", false);
            animator.SetFloat("Velocity", 0);
        }
        transform.Translate(velocity * Time.deltaTime, Space.Self);;  
        mouseH += Input.GetAxis("Mouse X");
        
        Quaternion rotY;
        float angleY = mouseH * eyeSpeed;
        rotY = Quaternion.AngleAxis(angleY, Vector3.up);

        transform.localRotation = baseOrientation*rotY;

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount<1)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            jumpCount++;
        }
        if(transform.position.y < 0.01)
        {
            jumpCount = 0;
        }
        
    }
}
