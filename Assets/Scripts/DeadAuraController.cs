using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAuraController : MonoBehaviour
{
    public float auraRadius = 2f; // Радиус ауры
    public float auraDamagePerSecond = 6f; // Урон ауры
    [SerializeField] public ParticleSystem auraParticleSystem;

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, auraRadius);

        HeroController hero = GetComponent<HeroController>();
       
        // HPS
        if (hero != null)
        {
            hero.Heal(auraDamagePerSecond * Time.deltaTime); // Вызываем метод для лечения героя
        }

        foreach (Collider collider in colliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();

            // DPS
            if (enemy != null)
            {
                enemy.TakeDamage(auraDamagePerSecond * Time.deltaTime); // Наносим урон в секунду
            }
        }
    }

    public void UpdateAuraAttributes()
    {
        // Обновите атрибуты ауры в соответствии с уровнем героя
        auraDamagePerSecond += 0.5f; // Увеличение урона / лечения в зависимости от уровня
        if (auraRadius <  8f)
            IncreaseAuraSize(); // Увеличение размера Particle System
    }
    void IncreaseAuraSize()
    {
        auraRadius += 0.1f; // Увеличение радиуса ауры в зависимости от уровня
        var shapeModule = auraParticleSystem.shape;
        shapeModule.radius = auraRadius; // Обновляем параметр Radius
    }
}