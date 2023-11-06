using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyController : MonoBehaviour
{
    //private float currentHealth;
    public float attackDistance = 2f; // Distance at which the attack begins
    public float stopAttackDistance = 3f; // Minimum distance at which the attack stops
    float frameRate = 60f;

    public float speed = 6f; // Enemy speed
    public float enemyLevel = 1f; // Enemy level
    public float maxHealth = 20f; // Enemy health
    public float damage = 3f;  // Damage dealt by enemy
    public float enemyExperience = 0.01f; // experience per kill

    public Transform target; // Transformation of the main character
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Update()
    {
        frameRate = 1f / Time.deltaTime;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (target != null)
        {
            agent.SetDestination(target.position); // Set the target for pursuit
        }

        if (distanceToTarget <= attackDistance)
        {
            DealDamage(); // Deal damage to the hero
        }

        if (distanceToTarget >= stopAttackDistance)
        {
            // todo Stop the attack (for the future for Range DD)
        }
    }
    public void TakeDamage(float damage) // Method for taking damage
    {
        maxHealth -= damage; 
        if (maxHealth <= 0)
        {
            DieWithAnimation(); // Method for removing an enemy with a disappearing animation
        }
    }
    public void DealDamage()
    {
        if (target != null)
        {
            // Calling the HeroTakeDamage method in the hero controller
            if (Vector3.Distance(transform.position, target.position) <= stopAttackDistance)
                target.GetComponent<HeroController>().HeroTakeDamage(damage / frameRate);
        }
    }
    public void DieWithAnimation()
    {
        StartCoroutine(ShrinkAndDestroy());
    }
    private IEnumerator ShrinkAndDestroy()
    {
        float duration = 0.5f; // Animation duration (in seconds)
        float elapsedTime = 0f;

        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.zero; // Reducing the model size to zero

        target.GetComponent<HeroController>().HeroGetExperience(enemyExperience * enemyLevel); // We give experience for killing

        while (elapsedTime < duration)
        {
            if (transform != null) // Checking for the existence of an object
            {
                transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        if (transform != null) // Checking for the existence of an object
        {
            Destroy(gameObject); // Destroying an object after animation
        }
    }
}
