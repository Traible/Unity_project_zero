using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject enemyPrefab; // Enemy Prefab
    public Transform target; // Transformation of the main character
    public int counter = 0; // Counter of spawned enemies

    public float enemyLevel = 1f; // Enemy level
    public float maxHealth = 20f; // Enemy health
    public float damage = 3f;  // Damage dealt by enemy
    public float speed = 6f; // Enemy speed
    public float enemyExperience = 0.01f; // experience per kill

    private float spawnInterval = 5f; // Spawn interval 5 секунд
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
            // Set the enemy's characteristics
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
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.name == "MainScene")
            {
                enemyLevel += 1f;
                maxHealth += 10f;
                damage += 2f;
                enemyExperience += 0.01f;
                if (enemyLevel % 5 == 0)
                    speed += 0.5f;
            }
            else
            {
                enemyLevel += 1f;
                maxHealth += 5f;
                enemyExperience += 0.04f;
                damage += 1f;
                if (enemyLevel % 3 == 0)
                    speed += 1f;
            }
        }
    }
}