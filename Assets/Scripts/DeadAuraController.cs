using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAuraController : MonoBehaviour
{
    public float auraRadius = 3f; // ������ ����
    public float auraDamagePerSecond = 1f; // ���� ����

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, auraRadius);

        foreach (Collider collider in colliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.TakeDamage(auraDamagePerSecond * Time.deltaTime); // ������� ���� � �������
            }
        }
    }
}