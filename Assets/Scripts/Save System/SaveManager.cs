using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayer(PlayerData data)
    {
        SaveSystem.SavePlayer(data);
    }

    public PlayerData LoadPlayer()
    {
        return SaveSystem.LoadPlayer();
    }
}