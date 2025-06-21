using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager instance; // Singleton instance of the PowerUpManager

    public PowerUpScriptableObject[] powerUps;
    public PowerUpScriptableObject activePowerUp;
    public GameObject activePowerUpGameObject { get; private set; } // The currently active power-up GameObject

    float spawnedTime = 0f;

    void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    public void Update()
    {
        if(activePowerUp != null && activePowerUpGameObject != null)
        {
            // Check if the power-up duration has expired
            if (Time.time - spawnedTime > activePowerUp.duration) RemoveActivePowerUp();
        }
    }

    public void SpawnPowerUp(Vector3 position)
    {
        if (activePowerUp == null)
        {
            if (powerUps.Length == 0)
            {
                Debug.LogWarning("No power-ups available to spawn.");
                return;
            }

            int randomIndex = Random.Range(0, powerUps.Length);
            activePowerUp = powerUps[randomIndex];
            activePowerUpGameObject = Instantiate(activePowerUp.powerUpPrefab, position, Quaternion.identity, transform);//Can be configured to use a object pool instead of instantiating every time

            activePowerUpGameObject.name = activePowerUp.powerUpName;

            foreach (PowerUpEffects effect in activePowerUp.effects)
            {
                effect.ApplyEffect(activePowerUp);
            }

            spawnedTime = Time.time; // Record the time when the power-up was spawned
        }
    }
    
    public void RemoveActivePowerUp()
    {
        if (activePowerUp != null)
        {
            foreach (PowerUpEffects effect in activePowerUp.effects)
            {
                effect.RemoveEffect(activePowerUp);
            }

            Destroy(activePowerUpGameObject);
            activePowerUp = null;
            activePowerUpGameObject = null;
        }
    }
}
