using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        // doesn't work in the editor
        Application.Quit();
    }

    public void MenuGame() {
        SceneManager.LoadScene("MainMenu");
    }

    public void BeatGame() {
        SceneManager.LoadScene("WinScene");
    }

    public void HowToPlayGame() {
        SceneManager.LoadScene("Instructions");
    }
}
