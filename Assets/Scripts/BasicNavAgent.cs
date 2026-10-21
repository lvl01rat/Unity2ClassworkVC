using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasicNavAgent : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;
    public Animator anim;
    public float attackTime;
    bool isAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking && Vector3.Distance(transform.position, player.position) > 5)
        {
            agent.SetDestination(player.position);
        }

        if (isAttacking && Vector3.Distance(transform.position, player.position) < 5)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }

        anim.SetFloat("Running", agent.velocity.magnitude);
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }
}
