using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAuraController : MonoBehaviour
{
    public float auraRadius = 2f; // Aura radius
    public float auraDamagePerSecond = 6f; // Aura Damage
    [SerializeField] public ParticleSystem auraParticleSystem;

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, auraRadius);

        HeroController hero = GetComponent<HeroController>();
       
        // HPS
        if (hero != null)
        {
            hero.Heal(auraDamagePerSecond * Time.deltaTime); // Calling a method to treat the hero
        }

        foreach (Collider collider in colliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();

            // DPS
            if (enemy != null)
            {
                enemy.TakeDamage(auraDamagePerSecond * Time.deltaTime); // Deal damage per second
            }
        }
    }

    public void UpdateAuraAttributes()
    {
        // Update aura attributes according to hero level
        auraDamagePerSecond += 0.5f; // Increased damage / healing based on level
        if (auraRadius <  8f)
            IncreaseAuraSize(); // Increasing the size of the Particle System (Aura)
    }
    void IncreaseAuraSize()
    {
        auraRadius += 0.1f; // Increase in aura radius depending on level
        var shapeModule = auraParticleSystem.shape;
        shapeModule.radius = auraRadius; // Update the Radius parameter
    }
}