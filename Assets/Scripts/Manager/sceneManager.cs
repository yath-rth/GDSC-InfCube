using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class sceneManager : MonoBehaviour
{
    [SerializeField] int gameIndex, mainmenuIndex;
    [SerializeField] TMP_Text highScoreText;

    private void Awake()
    {
        if (highScoreText != null) highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString("D5");
    }

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
