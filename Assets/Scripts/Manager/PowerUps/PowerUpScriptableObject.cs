using UnityEngine;

public class PowerUpScriptableObject : ScriptableObject
{
    public GameObject powerUpPrefab;
    public string powerUpName;
    public string description;
    public float duration;
    public Sprite icon;
    public int cost;
    public PowerUpEffects[] effects;
}
