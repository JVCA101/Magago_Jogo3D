using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarriorAnimsFREE;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 5f;
    private int jumpCount = 0;
    [SerializeField] private Animator animator;

    private WarriorController warriorController;
    // Start is called before the first frame update
    void Start()
    {
        GameObject child = transform.GetChild(0).gameObject;
        animator = child.GetComponent<Animator>();
        warriorController = GetComponent<WarriorController>();
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
        }
        else
        {
            animator.SetBool("Moving", false);
            animator.SetFloat("Velocity", 0);
        }
        transform.position += (velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount<1)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jump, ForceMode.Impulse);
            jumpCount++;
        }
        if(transform.position.y < 0.01)
        {
            jumpCount = 0;
        }
    }
}
