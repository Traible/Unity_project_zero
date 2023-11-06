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
    [SerializeField] public Text timeText; // Очки (время)
    [SerializeField] public Text timeTextBest; // Очки (рекорд / время)

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
        //foreach (Transform child in Pause_menuUI.transform) // Установите фиксированный размер для каждого дочернего объекта
        //  child.GetComponent<Animation>();
        Pause_menuUI.SetActive(false);
        Panel_HUDUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }
    void Pause()
    {
        Pause_menuUI.SetActive(true);
        Panel_HUDUI.SetActive(false);
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowPauseMenuOnGameOver()
    {
        Panel_HUDUI.SetActive(false);
        GameOver_menuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
}
