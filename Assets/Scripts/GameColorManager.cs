using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class GameColorManager : MonoBehaviour
{
    public Material skybox, player;
    [Range(-1f, 1f)] public float playerTint = 0f;
    public List<colorData> colors = new List<colorData>();
    Color playerColor;

    private void Awake()
    {
        setColor();
    }

    void setColor()
    {
        int colorIndex = Random.Range(0, colors.Count);

        playerColor = Color.Lerp(colors[colorIndex].color1, colors[colorIndex].color2, 0.5f);
        playerColor = new Color(playerColor.r - playerTint, playerColor.g - playerTint, playerColor.b - playerTint, playerColor.a);
        Debug.Log(playerColor);
        player.SetColor("_BaseColor", playerColor);

        skybox.SetColor("_SkyColor", colors[colorIndex].color1);
        skybox.SetColor("_HorizonColor", colors[colorIndex].color2);
    }
}

[System.Serializable]
public class colorData
{
    public Color color1;
    public Color color2;
}
