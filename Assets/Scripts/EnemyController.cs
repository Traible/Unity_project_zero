using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float speed = 5f;
    public float stopDistance = 0.5f;
    private float currentHealth;

    public float health = 10f; // �������� �����
    public float damage = 1f;  // ����, ��������� ������

    public Transform target; // ��������� �������� �����
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
            // ������������� ���� ��� �������������
            agent.SetDestination(target.position);
        }
    }
    // ����� ��� ��������� �����
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            //Die(); // ����� Die() ��� �������� �����
            DieWithAnimation(); // ����� ��� �������� ����� � ��������� ������������
        }
    }
    void Die()
    {
        Destroy(gameObject);
        // ���������� ������ �������� ����� �� �����, ��������, ����� Destroy(gameObject)
    }

    public void DieWithAnimation()
    {
        StartCoroutine(ShrinkAndDestroy());
    }
    private IEnumerator ShrinkAndDestroy()
    {
        float duration = 0.5f; // ������������ �������� (� ��������)
        float elapsedTime = 0f;

        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = Vector3.zero; // ���������� �� ����

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
            // ���������� ������ ����� ��������
            Destroy(gameObject);
        }
    }
}
