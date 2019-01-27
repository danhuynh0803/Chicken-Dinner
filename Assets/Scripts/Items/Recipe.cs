using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : Item
{
    // The two components the recipes is made from
    public Item item1, item2;
    private PlayerController player;
    private bool playerInRange = false; 

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();    
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= radius)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (playerInRange)
        {
            if (interactionImage != null)
                interactionImage.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                player.GetComponent<PlayerController>().PickUpItem(this.gameObject);
                this.gameObject.SetActive(false);
            }
        }
    }
}
