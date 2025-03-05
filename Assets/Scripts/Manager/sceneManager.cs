using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    [SerializeField] int gameIndex, mainmenuIndex;

    public void Game()
    {
        if (gameIndex != -1) SceneManager.LoadScene(gameIndex);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        if (mainmenuIndex != -1) SceneManager.LoadScene(mainmenuIndex);
        Time.timeScale = 1;
    }
}
