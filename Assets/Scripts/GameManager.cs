using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    player Player;
    sceneManager sceneManager;

    public bool isGameOver = false;

    [SerializeField] TMP_Text scoreText, endScreenScoreText, pauseScreenScoreText, highScoreText, coinsText;
    [SerializeField] GameObject scoreText_obj, endScreen_Obj, deathParticles, otherUI_obj, pauseScreen_obj;

    int score, highScore, coins, Allcoins, gameState = 1;

    void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;

        Player = player.instance;
        sceneManager = GetComponent<sceneManager>();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Allcoins = PlayerPrefs.GetInt("AllCoins", 0);

        if (coinsText != null) coinsText.text = coins.ToString("D3");
    }

    public sceneManager GetSceneManager()
    {
        return sceneManager;
    }

    public void addScore(int value)
    {
        score += value;

        scoreText_obj.transform.localScale = Vector3.one;
        if (scoreText != null) scoreText.text = score.ToString();
        if (scoreText_obj != null) scoreText_obj.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);

        if (score > highScore) highScore = score;
    }

    public void addCoin()
    {
        coins++;
        Allcoins++;

        if (coinsText != null) coinsText.gameObject.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);
        if (coinsText != null) coinsText.text = coins.ToString("D3");
    }

    public void GameOver()
    {
        gameState = 0;

        isGameOver = true;
        if (endScreen_Obj != null) endScreen_Obj.SetActive(true);
        if (deathParticles != null) deathParticles.SetActive(true);
        if (endScreenScoreText != null) endScreenScoreText.text = score.ToString("D5");
        if (otherUI_obj != null) otherUI_obj.SetActive(false);
        if (highScoreText != null) highScoreText.text = highScore.ToString("D5");

        PlayerPrefs.SetInt("HighScore", highScore);
    }

    public void mainMenu()
    {
        if (gameState == 0)
        {
            sceneManager.MainMenu();
        }
    }

    public void restart()
    {
        if (gameState == 0) sceneManager.Game();
    }

    public void pause()
    {
        if (gameState == 1)
        {
            pauseScreenScoreText.text = score.ToString("D5");
            otherUI_obj.SetActive(false);
            pauseScreen_obj.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void resume()
    {
        if (gameState == 0)
        {
            otherUI_obj.SetActive(true);
            pauseScreen_obj.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void pauseResume()
    {
        if(isGameOver == false)
        {
            if (gameState == 1)
            {
                pause();
                gameState = 0;
            }
            else
            {
                resume();
                gameState = 1;
            }
        }
    }
}
