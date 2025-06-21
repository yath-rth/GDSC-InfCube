using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<ShopItemScriptableIObject> AllShopItems; // List of shop items which are available in the shop
    public List<ShopItemScriptableIObject> ShopItemsInstances{ get; private set; } // List of instanced items and can be accesed from other scripts
    [SerializeField] private GameObject shopTile;// Prefab for each shop item tile
    [SerializeField] private Transform tileParent; // Parent transform where shop tiles will be instantiated and the content of the scroll rect
    public int playerCurrency;// Player's current currency amount

    public static ShopManager Instance { get; private set; } // Singleton instance of the ShopManager

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //To first check if the scroll rect has been assigned a content container
        if (tileParent == null)
        {
            Debug.LogError("Tile Parent is not assigned in the ShopManager.");
            return;
        }

        for (int i = 0; i < AllShopItems.Count; i++)
        {
            GameObject tile = Instantiate(shopTile, tileParent);
            if (tile != null)//Null check to avoid null pointer error
            {
                if (AllShopItems[i] != null)
                {
                    ShopItemsInstances.Add(Instantiate(AllShopItems[i]));
                    tile.GetComponent<ShopItemTile>().setupTile(ShopItemsInstances[i], PurchaseItem);
                }
            }
        }
    }

    public void PurchaseItem(ShopItemScriptableIObject item)
    {
        if (item.itemQuantity <= 0)
        {
            Debug.Log("Item is out of stock.");
            if (item.isPurchased)
            {
                Debug.Log("Item is already purchased but out of stock.");
                foreach (ShopItemEffects effect in item.effects)
                {
                    effect.Apply(item); // Invoke all the effects associated with the item
                }
            }

            return;
        }
        
        if (item.isPurchased)
        {
            Debug.Log("Item is already purchased and out of stock.");
        
            foreach (ShopItemEffects effect in item.effects)
            {
                effect.Apply(item); // Invoke all the effects associated with the item
            }
        
            return;
        }
        
        // Assuming we have a method to check player's currency
        if (item.itemPrice <= playerCurrency)
        {
            item.isPurchased = true;// Mark the item as purchased
            playerCurrency -= item.itemPrice; // Deduct the item's price from player's currency
            item.itemQuantity--;// Decrease the item's quantity

            Debug.Log($"Purchased {item.itemName} for {item.itemPrice} coins.");

            foreach (ShopItemEffects effect in item.effects)
            {
                effect.Apply(item); // Invoke all the effects associated with the item
            }
        }
        else
        {
            Debug.Log("Not enough currency to purchase this item.");
        }
    }
}