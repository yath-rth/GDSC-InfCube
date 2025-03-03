using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject scoreText_obj;

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
        if(scoreText_obj != null) scoreText_obj.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);
    }
}
