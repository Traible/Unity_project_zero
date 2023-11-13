using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject Leaderboards_menuUI;
    public GameObject Main_menuUI;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
    public void SceneLoad(int index)
    {
        // todo PlayerPrefs.SetFloat("BestTime", FindObjectOfType<HeroController>().bestTime); // Save best time before loading new scene
        SceneManager.LoadScene(index);
    }
    public void QuitGameWithMainMenu()
    {
        Application.Quit();
    }
    public void BackFromLeaderboard()
    {
        Leaderboards_menuUI.SetActive(false);
        Main_menuUI.SetActive(true);
    }
    public void Leaderboard()
    {
        Main_menuUI.SetActive(false);
        Leaderboards_menuUI.SetActive(true);
    }
}
