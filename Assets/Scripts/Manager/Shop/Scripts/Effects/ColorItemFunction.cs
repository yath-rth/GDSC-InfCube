using UnityEngine;

[CreateAssetMenu(menuName = "Shop Item Effects/Change Player Color")]
public class ColorItemFunction : ShopItemEffects
{
    public Material player;
    public override void Apply(ShopItemScriptableIObject item)
    {
        if (item != null)
            player.SetColor("_BaseColor", item.itemImageTint);
    }
}
