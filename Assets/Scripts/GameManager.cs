using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
    }
}
