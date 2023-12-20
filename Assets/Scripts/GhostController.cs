using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public enum State { WANDER, FOLLOW, ATTACK };

    [Header("GameObjects")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    private Animator animator;

    [Header("Settings")]
    public State state;
    public int health = 2;

    private float timePassed = 10f;
    private float timeToWander = 10f;
    private float distanceToPlayer;
    private AudioSource spookSound;
    private AudioSource attackSound;
    private AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        state = State.WANDER;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        spookSound = GetComponents<AudioSource>()[0];
        attackSound = GetComponents<AudioSource>()[1];
        deathSound = GetComponents<AudioSource>()[2];
        distanceToPlayer = 20f;
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.WANDER:
                if(timePassed > timeToWander)
                {
                    Wander();
                    timePassed = 0f;
                }
                else
                    timePassed += Time.deltaTime;
                animator.SetBool("Walk", false);
                break;
            case State.FOLLOW:
                agent.SetDestination(player.transform.position);
                animator.SetBool("Walk", true);
                break;
            case State.ATTACK:
                Attack();
                break;
        }

        checkState();

    }

    private void checkState()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 2f)
            state = State.ATTACK;
        else if(Vector3.Distance(transform.position, player.transform.position) < distanceToPlayer)
        {
            if(state == State.WANDER)
            {
                spookSound.Play();
                animator.SetTrigger("Noticed Player");
            }
            state = State.FOLLOW;
        }
        else
            state = State.WANDER;
    }

    void Wander()
    {
        float x = Random.Range(-50f, 50f);
        float z = Random.Range(-50f, 50f);
        Vector3 target = new Vector3(x, 0, z) + transform.position;
        animator.SetBool("Walk", true);
        agent.SetDestination(target);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        animator.SetBool("Walk", false);
        attackSound.Play();
    }

    public void TakeDamage()
    {
        health--;
        if(health <= 0)
        {
            animator.SetTrigger("Died");
            deathSound.Play();
            agent.isStopped = true;
        }
    }

}
