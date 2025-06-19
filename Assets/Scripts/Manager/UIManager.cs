using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static bool isPlayConnected = false;
    player Player;
    sceneManager sceneManager;

    public bool isGameOver = false;

    [SerializeField] TMP_Text scoreText, endScreenScoreText, pauseScreenScoreText, highScoreText, coinsText;
    [SerializeField] GameObject scoreText_obj, endScreen_Obj, deathParticles, otherUI_obj, pauseScreen_obj, mainMenu_obj, shopMenu_obj;

    int score, highScore, coins, Allcoins, gameState = 1;

    void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    void Start()
    {
        Application.targetFrameRate = 200;
        LoginToGooglePlay();
    }

    void LoginToGooglePlay()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            isPlayConnected = true;
        }
        else isPlayConnected = false;
    }

    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;

        Player = player.instance;
        sceneManager = GetComponent<sceneManager>();

        highScore = GetPlayerScore();
        Allcoins = PlayerPrefs.GetInt("AllCoins", 0);

        if (coinsText != null) coinsText.text = coins.ToString("D2");
        if (scoreText != null) coinsText.text = score.ToString("D3");
    }

    public sceneManager GetSceneManager()
    {
        return sceneManager;
    }

    public void addScore(int value)
    {
        score += value;

        scoreText_obj.transform.localScale = Vector3.one;
        if (scoreText != null) scoreText.text = score.ToString("D3");
        //if (scoreText_obj != null) scoreText_obj.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);

        if (score > highScore) highScore = score;
    }

    public int GetPlayerScore()
    {
        int val = 0;

        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard_high_scores,
            LeaderboardStart.PlayerCentered,
            1, // Load only the player's score
            LeaderboardCollection.Public, // Specify the leaderboard collection
            LeaderboardTimeSpan.AllTime,
            (data) =>
            {
                if (data != null && data.Valid && data.Scores.Length > 0)
                {
                    foreach (UnityEngine.SocialPlatforms.IScore score in data.Scores)
                    {
                        // Match against local player
                        if (score.userID == Social.localUser.id)
                        {
                            Debug.Log($"Player Score: {score.value}");
                            Debug.Log($"Player Rank: {score.rank}");
                            val = (int)score.value;
                            break;
                        }
                    }
                }
                else
                {
                    Debug.LogError("Failed to get player score or score not valid. Using Player prefs now");
                    val = PlayerPrefs.GetInt("HighScore", 0);
                }
            }
        );

        return val;
    }

    public void addCoin()
    {
        coins++;
        Allcoins++;

        //if (coinsText != null) coinsText.gameObject.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);
        if (coinsText != null) coinsText.text = coins.ToString("D2");
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

        //PlayerPrefs.SetInt("HighScore", highScore);

        if (isPlayConnected == true)
        {
            PlayGamesPlatform.Instance.ReportScore(highScore, GPGSIds.leaderboard_high_scores, (bool success) =>
            {
                if (success) Debug.Log("Score: " + highScore + " reported successfully.");
                else Debug.Log("Failed to report score.");
            });
        }
    }

    public void gameStart()
    {
        if (mainMenu_obj != null) mainMenu_obj.SetActive(false);
        if (otherUI_obj != null) otherUI_obj.SetActive(true);
    }

    public void showLeaderboard()
    {
        if (sceneManager.GameState == 0)
        {
            if (isPlayConnected == false) LoginToGooglePlay();
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_high_scores);
        }
    }

    public void mainMenu()
    {
        if (gameState == 0)
        {
            sceneManager.MainMenu();
        }
    }

    public void shop()
    {
        if (sceneManager.GameState == 0)
        {
            sceneManager.instance.ShopView();
            if( shopMenu_obj != null) shopMenu_obj.SetActive(true);
            if (mainMenu_obj != null) mainMenu_obj.SetActive(false);
            sceneManager.GameState = 2;
        }
    }

    public void close()
    {
        if (sceneManager.GameState == 2)
        {
            sceneManager.instance.GameView();
            if (mainMenu_obj != null) mainMenu_obj.SetActive(true);
            if (shopMenu_obj != null) shopMenu_obj.SetActive(false);
            sceneManager.GameState = 0;
        }
        else if (sceneManager.GameState == 0)
        {
            Application.Quit();
        }
    }

    public void restart()
    {
        if (gameState == 0 && sceneManager.GameState == 1) sceneManager.Game();
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
        if (isGameOver == false)
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
