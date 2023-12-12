using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jump = 5f;
    private int jumpCount = 0;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject child = transform.GetChild(0).gameObject;
        animator = child.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float dz = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        if(dx != 0 || dz != 0)
        {
            animator.SetBool("Moving", true);
            animator.SetFloat("Velocity", (dx + dz)*speed);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        transform.position += new Vector3(dx, 0, dz);

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
