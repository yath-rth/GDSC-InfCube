using UnityEngine;
public abstract class UpgradeItemEffects : ScriptableObject
{
   public abstract void Apply(UpgradeItem item);
}


/*
    * This script defines an abstract class UpgradeItemEffects that inherits from ScriptableObject.
    * It is used to create upgrade effects that can be applied to upgrade items in a game.
    * The Apply method must be implemented by any class that inherits from UpgradeItemEffects.
*/

// Example of a derived class that implements the Apply method
/* 

using UnityEngine;

[CreateAssetMenu(menuName = <<Name of the effect>>)] 
public class <<Name of the effect>> : UpgradeItemEffects
{
    public override void Apply(UpgradeItem item)
    {
        // Implement the effect logic here    
    }
}

//Example of a upgrade affecting the damage stat

using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade Effects/Damage Increase", order = 0)]
public class DamageIncreaseEffect : UpgradeItemEffects
{
    public StatValue stat; // The stat to be affected by this upgrade
    public Stats[] statsToAffect; // Array of stats to be affected by this upgrade
    public override void Apply(UpgradeItem item)
    {
        foreach (Stats Stat in statsToAffect)
        {
            Stat.unlockUpgrade(new StatValue(StatTypes.damage, stat.value)); // Ensure the damage stat is unlocked
        }

        Debug.Log($"Increased damage by {stat.value} for {item.data.itemName}");
    }
}

*/