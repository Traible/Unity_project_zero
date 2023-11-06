using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAuraController : MonoBehaviour
{
    public float auraRadius = 2f; // ������ ����
    public float auraDamagePerSecond = 6f; // ���� ����
    [SerializeField] public ParticleSystem auraParticleSystem;

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, auraRadius);

        HeroController hero = GetComponent<HeroController>();
       
        // HPS
        if (hero != null)
        {
            hero.Heal(auraDamagePerSecond * Time.deltaTime); // �������� ����� ��� ������� �����
        }

        foreach (Collider collider in colliders)
        {
            EnemyController enemy = collider.GetComponent<EnemyController>();

            // DPS
            if (enemy != null)
            {
                enemy.TakeDamage(auraDamagePerSecond * Time.deltaTime); // ������� ���� � �������
            }
        }
    }

    public void UpdateAuraAttributes()
    {
        // �������� �������� ���� � ������������ � ������� �����
        auraDamagePerSecond += 0.5f; // ���������� ����� / ������� � ����������� �� ������
        if (auraRadius <  8f)
            IncreaseAuraSize(); // ���������� ������� Particle System
    }
    void IncreaseAuraSize()
    {
        auraRadius += 0.1f; // ���������� ������� ���� � ����������� �� ������
        var shapeModule = auraParticleSystem.shape;
        shapeModule.radius = auraRadius; // ��������� �������� Radius
    }
}