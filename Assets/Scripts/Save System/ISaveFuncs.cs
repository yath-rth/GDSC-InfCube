using UnityEngine;
using System.Collections.Generic;
using System.IO;

public interface ISaveFuncs
{
    void LoadData(PlayerData data);
    void SaveData(PlayerData data); 
}