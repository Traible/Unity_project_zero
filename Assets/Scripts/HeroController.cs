using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    private Animator AnimatorHero;
    private NavMeshAgent navMeshAgent;
    private bool isWalking = false;
    private float health = 100f;
    // Start is called before the first frame update
    void Start()
    {
        AnimatorHero = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Установите цель для NavMeshAgent
                navMeshAgent.SetDestination(hit.point);
                isWalking = true;
                AnimatorHero.SetBool("IsWalking", true);
            }
        }
        else if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space))
        {
            // Атака
            AnimatorHero.SetTrigger("Attack");
        }
        if (isWalking && !navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    isWalking = false;
                    AnimatorHero.SetBool("IsWalking", false);
                }
            }
            else
            {
                AnimatorHero.SetBool("IsWalking", true);
            }
        }
    }
}