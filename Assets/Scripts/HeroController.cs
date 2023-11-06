using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HeroController : MonoBehaviour
{
    [SerializeField] public UnityEngine.UI.Image HealthBarImage; // Link to the image of the health bar in the interface
    [SerializeField] public UnityEngine.UI.Image ExperienceBarImage; // Link to the image of the experience bar in the interface
    [SerializeField] public Text timeText; // Current Survival Time
    [SerializeField] public Text timeTextBest; // Better survival time
    private DeadAuraController auraController;
    private Animator AnimatorHero;
    private NavMeshAgent navMeshAgent;
    private PauseController pauseController;
    private bool isWalking = false;
    private float currentHealth = 30f;
    private float maxHealth = 50f;
    public float currentExperience = 0f;
    public float maxExperience = 5f;
    public float HeroLevel = 1f;
    public float currentTime = 0f;
    public float bestTime = 0f;
    private float startTime;
    private float lastCheckTime = 0f;
    private float checkInterval = 0.5f; // Check interval in seconds

    // Start is called before the first frame update
    void Start()
    {
        AnimatorHero = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        auraController = FindObjectOfType<DeadAuraController>();
        pauseController = FindObjectOfType<PauseController>();
        startTime = Time.time; // Initializing time when starting the game
        currentTime = 0f;
        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        string formattedBestTime = string.Format("{0}:{1:00}", (int)bestTime / 60, (int)bestTime % 60);
        timeTextBest.text = "Best: " + formattedBestTime;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime >= checkInterval)  // Optimization
        {
            currentTime = Time.time - startTime; // Get the current time in seconds
            string formattedTime = string.Format("{0}:{1:00}", (int)currentTime / 60, (int)currentTime % 60);
            timeText.text = "Time: " + formattedTime;
            if (currentTime >= bestTime)
            {
                bestTime = currentTime; // Update best time
                timeTextBest.text = "Best: " + formattedTime;
                PlayerPrefs.SetFloat("BestTime", bestTime);
                PlayerPrefs.Save();
            }
            lastCheckTime = Time.time;
        }

        UpdateHealthBar();
        UpdateExperienceBar();
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Target for NavMeshAgent
            {
                navMeshAgent.SetDestination(hit.point);
                isWalking = true;
                AnimatorHero.SetBool("IsWalking", true);
            }
        }

        //else if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space))
        //{
        //    // Alternative Attack
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
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // We increase health, but not more than the maximum value
    }
    void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth; // Calculate health percentage (from 0.0 to 1.0)
        HealthBarImage.fillAmount = healthPercentage; // Set health bar image fill
    }
    void UpdateExperienceBar()
    {
        float experiencePercentage = currentExperience / maxExperience; // Calculate experience percentage (from 0.0 to 1.0)
        ExperienceBarImage.fillAmount = experiencePercentage; // Set the experience bar image fill
    }
    public void HeroTakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Time.timeScale = 0f;
            pauseController.ShowPauseMenuOnGameOver();
            //if (currentTime > bestTime)
            //{
            //    bestTime = currentTime;
            //    PlayerPrefs.SetFloat("BestTime", bestTime);
            //    PlayerPrefs.Save();
            //}
            // todo Handling a hero's death
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
            // todo lvlup other mechanics
        }
    }
}