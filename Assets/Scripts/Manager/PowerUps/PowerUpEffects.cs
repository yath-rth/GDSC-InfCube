using UnityEngine;

public abstract class PowerUpEffects : ScriptableObject
{
    public abstract void ApplyEffect(PowerUpScriptableObject powerUp);
    public abstract void RemoveEffect(PowerUpScriptableObject powerUp);   
}
