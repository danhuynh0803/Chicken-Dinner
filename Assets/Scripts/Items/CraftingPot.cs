using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPot : MonoBehaviour
{
    // This pot mixes the items and crafts the recipes

    public GameObject addedItemsCanvas;
    [SerializeField]
    private List<Item> itemsList;

    private void Start()
    {
        itemsList = new List<Item>();
    }
    private void Update()
    {
       if (itemsList.Count <= 0)
        {
            addedItemsCanvas.SetActive(false);
        }

        if (itemsList.Count >= 0)
        {
            DisplayAddedItems();
        }
    }


    void DisplayAddedItems()
    {
        
    }
}
