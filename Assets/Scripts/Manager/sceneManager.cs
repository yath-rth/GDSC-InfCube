using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using Cinemachine;

public class sceneManager : MonoBehaviour
{
    public static sceneManager instance;
    public static int GameState = 0;

    [SerializeField] GameObject cube;
    [SerializeField] GameObject[] tiles;
    [SerializeField] int gameIndex = -1, mainmenuIndex = -1;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] CinemachineVirtualCamera gameCam, shopCam;

    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;

        if (highScoreText != null) highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString("D5");
    }

    public void Game()
    {
        if (GameState == 0 && Time.timeSinceLevelLoad > 2f)
        {
            if (cube != null) cube.transform.DOScale(Vector3.one * 10, .25f).OnComplete(() =>
            {
                cube.SetActive(false);

                for (int i = 0; i < tiles.Length; i++)
                {
                    tiles[i].GetComponent<Tile>().setfallerLayer(9);
                }

                Time.timeScale = 1;
                GameState = 1;
                GameManager.instance.gameStart();
            });
        }
    }

    public void MainMenu()
    {
        Debug.Log(Time.time);
        if (mainmenuIndex != -1) SceneManager.LoadScene(gameIndex);
        Time.timeScale = 1;
        GameState = 0;
    }

    public void ShopView()
    {
        if (gameCam != null && shopCam != null)
        {
            gameCam.Priority = 0;
            shopCam.Priority = 10;
        }
    }

    public void GameView()
    {
        if (gameCam != null && shopCam != null)
        {
            gameCam.Priority = 10;
            shopCam.Priority = 0;
        }
    }
}
