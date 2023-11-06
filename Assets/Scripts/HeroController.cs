using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HeroController : MonoBehaviour
{
    [SerializeField] public UnityEngine.UI.Image HealthBarImage; // Ссылка на изображение полосы здоровья в интерфейсе
    [SerializeField] public UnityEngine.UI.Image ExperienceBarImage; // Ссылка на изображение полосы опыта в интерфейсе
    [SerializeField] public Text timeText; // Очки (время)
    [SerializeField] public Text timeTextBest; // Очки (рекорд / время)
    private DeadAuraController auraController;
    private Animator AnimatorHero;
    private NavMeshAgent navMeshAgent;
    private PauseController pauseController;
    private bool isWalking = false;
    private float currentHealth = 30f;
    private float maxHealth = 50f;
    public float currentExperience = 0f;
    public float maxExperience = 10f;
    public float HeroLevel = 1f;
    public float currentTime = 0f;
    public float bestTime = 0f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        AnimatorHero = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        auraController = FindObjectOfType<DeadAuraController>();
        pauseController = FindObjectOfType<PauseController>();
        startTime = Time.time;
        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        startTime = Time.time; // Инициализация времени при старте игры
        currentTime = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time - startTime; // Получаем текущее время в секундах
        string formattedTime = string.Format("{0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
        timeText.text = "Time: " + formattedTime;
        if (currentTime >= bestTime)
            timeTextBest.text = "Best: " + formattedTime;
        UpdateHealthBar();
        UpdateExperienceBar();
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Target for NavMeshAgent
                navMeshAgent.SetDestination(hit.point);
                isWalking = true;
                AnimatorHero.SetBool("IsWalking", true);
            }
        }
        //else if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space))
        //{
        //    // Атака
        //    AnimatorHero.SetTrigger("Attack");
        //}
        if (isWalking && !navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    isWalking = false;
                    AnimatorHero.SetBool("IsWalking", false);
                }
            }
            else
            {
                AnimatorHero.SetBool("IsWalking", true);
            }
        }
    }
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // Увеличиваем здоровье, но не больше максимального значения
    }
    void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth; // Вычислите процент здоровья (от 0.0 до 1.0)
        HealthBarImage.fillAmount = healthPercentage; // Установите заполнение изображения полосы здоровья
    }
    void UpdateExperienceBar()
    {
        float experiencePercentage = currentExperience / maxExperience; // Вычислите процент опыта (от 0.0 до 1.0)
        ExperienceBarImage.fillAmount = experiencePercentage; // Установите заполнение изображения полосы опыта
    }
    public void HeroTakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Заглушка, т.к. во реализации урона врагов хп улетало в минус огромные числа
            Time.timeScale = 0f;
            pauseController.ShowPauseMenuOnGameOver();
            if (currentTime > bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);
                PlayerPrefs.Save();
            }
            // Обработка смерти героя
            // Можете добавить здесь код для обработки смерти героя
        }
    }
    public void HeroGetExperience(float ExpPerkill)
    {
        currentExperience += ExpPerkill;
        if (currentExperience >= maxExperience)
        {
            currentExperience = currentExperience - maxExperience;
            maxExperience *= 1.2f;
            {
                HeroLevel++;
                auraController.UpdateAuraAttributes();
                currentHealth += 10f;
                maxHealth += 20f;
            }
            // todo lvlup
        }
    }

}