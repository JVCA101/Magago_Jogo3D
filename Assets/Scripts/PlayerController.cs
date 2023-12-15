using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WarriorAnimsFREE;

public class PlayerController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private Animator animator;

    [Header("Movement settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private int jumpCount = 0;
    private bool isGrounded = true;

    [Header("Camera settings")]
    [SerializeField] private float eyeSpeed = 5f;
    private Quaternion baseOrientation;
    // private Quaternion lookingDirection;
    private float mouseH = 0;

    [Header("Health settings")]
    private float health = 100f;
    public Image healthBar;


    // Start is called before the first frame update
    void Start()
    {
        GameObject child = transform.GetChild(0).gameObject;
        animator = child.GetComponent<Animator>();

        baseOrientation = transform.localRotation;

        // Depois retirar, está sendo chamado no ScenesController.StartGame()
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * speed * (Input.GetKey(KeyCode.LeftControl) ? 2 : 1);
        float dz = Input.GetAxis("Vertical") * speed * (Input.GetKey(KeyCode.LeftControl) ? 2 : 1);
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
        transform.Translate(velocity * Time.deltaTime, Space.Self);
        mouseH += Input.GetAxis("Mouse X");
        
        Quaternion rotY;
        float angleY = mouseH * eyeSpeed;
        rotY = Quaternion.AngleAxis(angleY, Vector3.up);

        transform.localRotation = baseOrientation*rotY;

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount<1)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetInteger("Jumping", 1);
            animator.SetInteger("Trigger Number", 1);
            animator.SetTrigger("Trigger");
            jumpCount++;
            isGrounded = false;
        }
        //! Problema com o pulo, transform.position.y <= 0.1f
        else if(!isGrounded && GetComponent<Rigidbody>().velocity.y <= 0 && transform.position.y <=0.1f)
        {
            jumpCount = 0;
            animator.SetInteger("Jumping", 0);
            animator.SetInteger("Trigger Number", 1);
            animator.SetTrigger("Trigger");
            isGrounded = true;
        }
        else if(!isGrounded && GetComponent<Rigidbody>().velocity.y < 0)
        {
            animator.SetInteger("Jumping", 2);
            animator.SetInteger("Trigger Number", 1);
            animator.SetTrigger("Trigger");
        }

        // if(Input.GetKeyDown(KeyCode.Q))
        //     TakeDamage();
    }

    void TakeDamage(float damage = 10f)
    {
        health -= damage;
        healthBar.fillAmount = health / 100f;
        if(health <= 0)
        {
            // animator.SetInteger("Trigger Number", 2);
            // animator.SetTrigger("Trigger");
            // animator.SetBool("Dead", true);
            ScenesController.GameOver();
        }
    }

}
