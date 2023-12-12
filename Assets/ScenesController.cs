using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    
    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void Victory()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void MainGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
