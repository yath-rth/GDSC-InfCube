using UnityEngine;

[DefaultExecutionOrder(100)]
public class UpgradeUiBehaviour : MonoBehaviour
{
    UpgradeManager manager;

    [SerializeField] private GameObject upgradeUiTile;// Prefab for each shop item tile
    [SerializeField] private Transform tileParent; // Parent transform where shop tiles will be instantiated and the content of the scroll rect
    [SerializeField] private bool randomize = false;
    [SerializeField] private int count = 0; // Number of items to display in the shop UI

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        manager = UpgradeManager.Instance; // Get the instance of UpgradeManager
        if (manager == null)
        {
            Debug.LogError("UpgradeManager instance is not found. Please ensure it is initialized before UpgradeUiBehaviour.");
            return;
        }
        if (randomize)
        {
            // Shuffle the items in AllShopItems list
            for (int i = 0; i < count; i++)
            {
                GameObject tile = Instantiate(upgradeUiTile, tileParent);
                if (tile != null)//Null check to avoid null pointer error
                {
                    int a = Random.Range(0, manager.AllUpgradeItems.Count);
                    if (manager.AllUpgradeItems[a] != null)
                    {
                        tile.GetComponent<UpgradeItemTile>().setupTile(manager.AllUpgradeItems[a], manager.PurchaseItem);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < manager.AllUpgradeItems.Count; i++)
            {
                GameObject tile = Instantiate(upgradeUiTile, tileParent);
                if (tile != null && manager != null)//Null check to avoid null pointer error
                {
                    if (manager.AllUpgradeItems[i] != null)
                    {
                        tile.GetComponent<UpgradeItemTile>().setupTile(manager.AllUpgradeItems[i], manager.PurchaseItem);
                    }
                }
            }
        }

        Time.timeScale = 0f;
    }

    void OnDisable()
    {
        // Clear the tiles when the UI is disabled
        foreach (Transform child in tileParent)
        {
            Destroy(child.gameObject);
        }
    }
}

