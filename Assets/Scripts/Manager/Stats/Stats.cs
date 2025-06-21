using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats Object", order = 0)]
public class Stats : ScriptableObject
{
    public List<StatValue> defaultStats = new List<StatValue>();
    public List<StatValue> appliedUpgrades = new List<StatValue>();

    public int getStat(StatTypes stat)
    {
        foreach (StatValue item in defaultStats)
        {
            if (item.stat == stat)
            {
                int value = item.value;

                foreach (StatValue upgrade in appliedUpgrades)
                {
                    if (upgrade.stat == stat)
                    {
                        value += upgrade.value;
                    }
                }

                return value;
            }
        }

        Debug.LogError("Not found stat: " + stat + " in " + name);
        return -1;
    }

    public void setStat(StatTypes stat, int value)
    {
        foreach (StatValue item in defaultStats)
        {
            if (item.stat == stat)
            {
                item.value = value;
                return;
            }
        }

        Debug.LogError("Stat not found: " + stat);
    }

    public void unlockUpgrade(StatValue statUpgrade)
    {
        if (!appliedUpgrades.Contains(statUpgrade)) appliedUpgrades.Add(statUpgrade);
    }

    public void resetUpgrades()
    {
        appliedUpgrades.Clear();
    }

    void OnDisable()
    {
        resetUpgrades();
    }
}

public enum StatTypes
{
    hitpoints,
    speed,
    damage,
    maxhitpoints,
    attackCooldown,
}

[System.Serializable]
public class StatValue
{
    public StatTypes stat;
    public int value;
    public StatValue(StatTypes stat, int value)
    {
        this.stat = stat;
        this.value = value;
    }
}
