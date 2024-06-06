using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void PlayEasy()
    {
        PlayGame("легкий");
    }

    public void PlayMedium()
    {
        PlayGame("средний");
    }

    public void PlayHard()
    {
        PlayGame("сложный");
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
