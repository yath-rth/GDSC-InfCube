using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    [SerializeField] TMP_Text scoreText, endScreenScoreText, pauseScreenScoreText;
    [SerializeField] GameObject scoreText_obj, endScreen_Obj, deathParticles, otherUI_obj, pauseScreen_obj;

    int score;

    void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }

    public void addScore(int value)
    {
        score += value;

        scoreText_obj.transform.localScale = Vector3.one;
        if (scoreText != null) scoreText.text = score.ToString();
        if (scoreText_obj != null) scoreText_obj.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);
    }

    public void GameOver()
    {
        isGameOver = true;
        if (endScreen_Obj != null) endScreen_Obj.SetActive(true);
        if (deathParticles != null) deathParticles.SetActive(true);
        if (endScreenScoreText != null) endScreenScoreText.text = score.ToString("D5");
        if (otherUI_obj != null) otherUI_obj.SetActive(false);
    }

    public void pause()
    {
        pauseScreenScoreText.text = score.ToString("D5");
        otherUI_obj.SetActive(false);
        pauseScreen_obj.SetActive(true);
        Time.timeScale = 0;
    }

    public void resume()
    {
        otherUI_obj.SetActive(true);
        pauseScreen_obj.SetActive(false);
        Time.timeScale = 1;
    }
}
