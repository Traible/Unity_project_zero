using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Pause_menuUI;
    public GameObject Panel_HUDUI;
    public GameObject GameOver_menuUI;
    public HeroController heroController;
    [SerializeField] public Text timeText; // Current Survival Time
    [SerializeField] public Text timeTextBest; // Best Survival Time

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume() 
    {
        //  todo
        //  foreach (Transform child in Pause_menuUI.transform) // Set a fixed size for each child object
        //  child.GetComponent<Animation>();
        Pause_menuUI.SetActive(false);
        Panel_HUDUI.SetActive(true);
        GameOver_menuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }
    void Pause()
    {
        Pause_menuUI.SetActive(true);
        Panel_HUDUI.SetActive(false);
        GameOver_menuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        // Resume();
        LoadMenu(); // Just a crutch
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowPauseMenuOnGameOver()
    {
        Pause_menuUI.SetActive(false);
        Panel_HUDUI.SetActive(false);
        GameOver_menuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        timeText.text = heroController.timeText.text; // Получить текущее время как строку
        timeTextBest.text = heroController.timeTextBest.text; // Получить лучшее время как строку
    }
}
