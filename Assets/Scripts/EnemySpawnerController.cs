using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject enemyPrefab; // Префаб противника
    public Transform target; // Трансформ главного героя

    public float spawnInterval = 1f; // Интервал спавна 5f
    private float nextSpawnTime = 0f;

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
        
        Vector3 spawnPosition = new Vector3(Random.Range(15f, 25f), 0f, Random.Range(130f, 135f)); // Установите свои координаты спавна
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Устанавливаем характеристики врага
        //EnemyController.health = 100f;
        //EnemyController.damage = enemyDamage;
        //EnemyController.speed = enemySpeed;
    }
}