using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static void SavePlayer(PlayerData Data)
    {
        string path = Path.Combine(Application.persistentDataPath, "data");

        if (Directory.Exists(Path.GetDirectoryName(path)) == false)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try
        {
            string toSave = JsonUtility.ToJson(Data, true);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(toSave);  
                }
            }

        }
        catch
        {
            Debug.LogError("Save was not completed");
        }
    }

    public static PlayerData LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath, "data");

        if (File.Exists(path))
        {
            string toLoad = "";
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    toLoad = reader.ReadToEnd();
                }
            }

            PlayerData data = JsonUtility.FromJson<PlayerData>(toLoad);
            return data;
        }
        else
        {
            Debug.LogError("Save File not found");
            return null;
        }
    }
}
