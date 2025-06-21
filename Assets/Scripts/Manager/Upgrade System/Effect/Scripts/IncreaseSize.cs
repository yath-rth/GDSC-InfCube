using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Item Effects/Increase Size")]
public class IncreaseSize : UpgradeItemEffects
{
    public float sizeIncreaseAmount = 0.5f; // Amount to increase the size by
    public override void Apply(UpgradeItem item)
    {
        if (item != null)
        {
            // Assuming the player has a method to increase size
            GameObject manager = GameObject.FindWithTag("Game Manager");
            if (manager != null)
            {
                manager.GetComponent<pathSpawner>().tileSize += sizeIncreaseAmount; // Increase the tile size
            }
        }
    }
}
