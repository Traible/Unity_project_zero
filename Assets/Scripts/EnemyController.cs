using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyController : MonoBehaviour
{
    //private float currentHealth;
    public float attackDistance = 2f; // Расстояние, на котором начинается атака
    public float stopAttackDistance = 3f; // Минимальное расстояние, на котором атака прекращается
    float frameRate = 60f;

    public float speed = 6f;
    public float enemyLevel = 1f; // Уровень врага
    public float maxHealth = 20f; // Здоровье врага
    public float damage = 3f;  // Урон, наносимый врагом
    public float enemyExperience = 0.01f; //  (опыт за килл)

    public Transform target; // Трансформ главного героя
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Update()
    {
        frameRate = 1f / Time.deltaTime;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (target != null)
        {
            agent.SetDestination(target.position); // Устанавливаем цель для преследования
        }

        if (distanceToTarget <= attackDistance)
        {
            DealDamage(); // Наносим урон герою
        }

        if (distanceToTarget >= stopAttackDistance)
        {
            // Прекращаем атаку (на будущее для Range DD)
        }
    }
    // Метод для получения урона
    public void TakeDamage(float damage)
    {
        //currentHealth -= damage;
        maxHealth -= damage; 
        if (maxHealth <= 0)
        {
            DieWithAnimation(); // метод для удаления врага с анимацией исчезновения
        }
    }
    public void DealDamage()
    {
        if (target != null)
        {
            // Вызов метода HeroTakeDamage в контроллере героя
            if (Vector3.Distance(transform.position, target.position) <= stopAttackDistance)
                target.GetComponent<HeroController>().HeroTakeDamage(damage / frameRate);
        }
    }
    public void DieWithAnimation()
    {
        StartCoroutine(ShrinkAndDestroy());
    }

    //public void IncreaseLevelEnemy()
    //{
    //    enemyLevel += 1f;  // Уровень врага
    //    maxHealth += 15f; // Здоровье врага
    //    damage += 2f;  // Урон, наносимый врагом
    //    enemyExperience += 0.01f; //  (опыт за килл)
    //    if (enemyLevel %5 == 0) 
    //        speed += 0.5f;
    //}
    private IEnumerator ShrinkAndDestroy()
    {
        float duration = 0.5f; // Длительность анимации (в секундах)
        float elapsedTime = 0f;

        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.zero; // Уменьшение до нуля

        target.GetComponent<HeroController>().HeroGetExperience(enemyExperience * enemyLevel); // Даём  опыт за убийство

        while (elapsedTime < duration)
        {
            if (transform != null) // Проверка на существование объекта
            {
                transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        if (transform != null) // Проверка на существование объекта
        {
            Destroy(gameObject); // Уничтожаем объект после анимации
        }
    }
}
