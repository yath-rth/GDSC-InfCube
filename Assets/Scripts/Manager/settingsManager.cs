using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class settingsManager : MonoBehaviour
{
    public static settingsManager instance;

    public List<Settings> modes;

    void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
    }
}