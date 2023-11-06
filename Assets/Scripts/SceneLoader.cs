using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

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
}
