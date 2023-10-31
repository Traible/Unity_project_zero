using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f;
    public float stopDistance = 0.5f;
    private float currentHealth;

    public float health = 10f; // Здоровье врага
    public float damage = 1f;  // Урон, наносимый врагом

    public Transform target; // Трансформ главного героя
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = health;
        //target = GameObject.FindGameObjectWithTag("Hero").transform;
    }

    // Start is called before the first frame update
    void Update()
    {
        //if (Vector3.Distance(transform.position, player.position) > 0.5f) // > stopDistance
        //{
            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //}
        if (target != null)
        {
            // Устанавливаем цель для преследования
            agent.SetDestination(target.position);
        }
    }
    // Метод для получения урона
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Die(); // метод Die() для удаления врага
            DieWithAnimation(); // метод для удаления врага с анимацией исчезновения
        }
    }
    void Die()
    {
        Destroy(gameObject);
        // Реализуйте логику удаления врага из сцены, например, через Destroy(gameObject)
    }

    public void DieWithAnimation()
    {
        StartCoroutine(ShrinkAndDestroy());
    }
    private IEnumerator ShrinkAndDestroy()
    {
        float duration = 0.5f; // Длительность анимации (в секундах)
        float elapsedTime = 0f;

        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.zero; // Уменьшение до нуля

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
            // Уничтожаем объект после анимации
            Destroy(gameObject);
        }
    }
}
