using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Item Effects/Decrease Speed")]
public class DecreaseSpeed : UpgradeItemEffects
{
    public float speedDecreaseAmount = 0.5f; // Amount to decrease the speed by

    public override void Apply(UpgradeItem item)
    {
        if (item != null)
        {
            // Assuming the player has a method to decrease speed
            player player = GameObject.FindWithTag("Player").GetComponent<player>();
            if (player != null)
            {
                player.DecreaseSpeed(speedDecreaseAmount);
            }
        }
    }
}
