using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject enemyPrefab; // Префаб противника
    public Transform target; // Трансформ главного героя
    public int counter = 0;

    public float enemyLevel = 1f; // Уровень врага
    public float maxHealth = 20f; // Здоровье врага
    public float damage = 3f;  // Урон, наносимый врагом
    public float speed = 6f; // Скорость врага
    public float enemyExperience = 0.01f; // опыт за килл

    private float spawnInterval = 5f; // Интервал спавна 5f
    private float nextSpawnTime = 0f;
    public float spawnRangeX = 5f;
    public float spawnRangeZ = 5f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        counter++;
        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-spawnRangeX, spawnRangeX),0f,transform.position.z + Random.Range(-spawnRangeZ, spawnRangeZ));
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        if (enemyController != null)
        {
            // Устанавливаем характеристики врага
            enemyController.speed = speed;
            enemyController.enemyLevel = enemyLevel;
            enemyController.maxHealth = maxHealth;
            enemyController.damage = damage;
            enemyController.enemyExperience = enemyExperience;
        }
        if (counter % 5 == 0) 
        {
            IncreaseLevelEnemy();
            if (spawnInterval > 0.05f)
                spawnInterval -= 0.05f;
        }
        void IncreaseLevelEnemy()
        {
            enemyLevel += 1f;  // Уровень врага
            maxHealth += 10f; // Здоровье врага
            damage += 2f;  // Урон, наносимый врагом
            enemyExperience += 0.01f; //  (опыт за килл)
            if (enemyLevel % 5 == 0)
                speed += 0.5f;
        }
    }
}