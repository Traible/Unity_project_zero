using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyController : MonoBehaviour
{
    //private float currentHealth;
    public float attackDistance = 2f; // ����������, �� ������� ���������� �����
    public float stopAttackDistance = 3f; // ����������� ����������, �� ������� ����� ������������
    float frameRate = 60f;

    public float speed = 6f;
    public float enemyLevel = 1f; // ������� �����
    public float maxHealth = 20f; // �������� �����
    public float damage = 3f;  // ����, ��������� ������
    public float enemyExperience = 0.01f; //  (���� �� ����)

    public Transform target; // ��������� �������� �����
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
            agent.SetDestination(target.position); // ������������� ���� ��� �������������
        }

        if (distanceToTarget <= attackDistance)
        {
            DealDamage(); // ������� ���� �����
        }

        if (distanceToTarget >= stopAttackDistance)
        {
            // ���������� ����� (�� ������� ��� Range DD)
        }
    }
    // ����� ��� ��������� �����
    public void TakeDamage(float damage)
    {
        //currentHealth -= damage;
        maxHealth -= damage; 
        if (maxHealth <= 0)
        {
            DieWithAnimation(); // ����� ��� �������� ����� � ��������� ������������
        }
    }
    public void DealDamage()
    {
        if (target != null)
        {
            // ����� ������ HeroTakeDamage � ����������� �����
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
    //    enemyLevel += 1f;  // ������� �����
    //    maxHealth += 15f; // �������� �����
    //    damage += 2f;  // ����, ��������� ������
    //    enemyExperience += 0.01f; //  (���� �� ����)
    //    if (enemyLevel %5 == 0) 
    //        speed += 0.5f;
    //}
    private IEnumerator ShrinkAndDestroy()
    {
        float duration = 0.5f; // ������������ �������� (� ��������)
        float elapsedTime = 0f;

        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.zero; // ���������� �� ����

        target.GetComponent<HeroController>().HeroGetExperience(enemyExperience * enemyLevel); // ���  ���� �� ��������

        while (elapsedTime < duration)
        {
            if (transform != null) // �������� �� ������������� �������
            {
                transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
            }
            yield return null;
        }
        if (transform != null) // �������� �� ������������� �������
        {
            Destroy(gameObject); // ���������� ������ ����� ��������
        }
    }
}
