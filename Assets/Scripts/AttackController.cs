using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public static bool isAttacking = false;
    public static bool damage = true;

    private AudioSource attackSound;

    // Start is called before the first frame update
    void Start()
    {
        attackSound = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy" && isAttacking && damage)
        {
            collision.gameObject.GetComponent<GhostController>().TakeDamage();
            attackSound.Play();
            damage = false;
        }
    }
}
