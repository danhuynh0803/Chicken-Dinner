using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPot : MonoBehaviour
{
    // This pot mixes the items and crafts the recipes
    public Item addedItem1;
    public Item addedItem2;
    private Item createdItem;

    public Recipe[] recipes;

    public GameObject craftPanel;
    public Image item1Image, item2Image;
    public GameObject craftButton;
    public GameObject craftFlame;
    public Transform recipeSpawn;

    Animator anim;
    AudioSource audio; 

    private void Start()
    {
        craftFlame.SetActive(false);
        audio = GetComponent<AudioSource>();
        craftPanel.SetActive(false);
        anim = GetComponent<Animator>();
        /*
        foreach (Recipe recipe in recipes)
        {
            Debug.Log(recipe.itemName + " made from: " + recipe.item1.itemName + " and " + recipe.item2.itemName);
        }
        */
    }

    public void Craft()
    {
        if (addedItem1 && addedItem2)
        {
            // If the objects are the same, just don't craft it
            if (addedItem1.itemName.ToLower().Equals(addedItem2.itemName.ToLower()))
            {
                return;
            }
            else
            {
                audio.Play();
                craftFlame.SetActive(true);
                StartCoroutine(CraftWithDelay(3.0f));
                //CraftWithDelay(3.0f);
                
            }
        }
    }

    IEnumerator CraftWithDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);     
        
        // Check if we have all of the necessary ingredients
        foreach (GameObject recipeObject in GameController.instance.items)
        //foreach (Recipe recipe in recipes)
        {
            Recipe recipe = recipeObject.GetComponent<Recipe>();
            // Check if the recipe components match what is in the pot
            if (addedItem1.itemName.ToLower().Equals(recipe.item1.itemName.ToLower()) ||
                addedItem1.itemName.ToLower().Equals(recipe.item2.itemName.ToLower()))
            {
                if (addedItem2.itemName.ToLower().Equals(recipe.item1.itemName.ToLower()) ||
                    addedItem2.itemName.ToLower().Equals(recipe.item2.itemName.ToLower()))
                {
                    createdItem = recipe.GetComponent<Item>();
                    Instantiate(recipeObject, recipeSpawn.position, Quaternion.identity);
                    Debug.Log("Crafted " + createdItem.itemName);            
                    break;
                }
            }

        }

        audio.Stop();
        RemoveAddedItems();
        craftFlame.SetActive(false);
    }

    public void CraftWithNoDelay()
    {
        if (addedItem1 && addedItem2)
        {
            // If the objects are the same, just don't craft it
            if (addedItem1.itemName.ToLower().Equals(addedItem2.itemName.ToLower()))
            {
                return;
            }

            audio.Play();
            // Check if we have all of the necessary ingredients
            foreach (Recipe recipe in recipes)
            {
                // Check if the recipe components match what is in the pot
                if (addedItem1.itemName.ToLower().Equals(recipe.item1.itemName.ToLower()) ||
                    addedItem1.itemName.ToLower().Equals(recipe.item2.itemName.ToLower()))
                {
                    if (addedItem2.itemName.ToLower().Equals(recipe.item1.itemName.ToLower()) ||
                        addedItem2.itemName.ToLower().Equals(recipe.item2.itemName.ToLower()))
                    {
                        createdItem = recipe.GetComponent<Item>();
                        Debug.Log("Crafted " + createdItem.itemName);
                        RemoveAddedItems();
                        return;
                    }
                }

            }
        }
    }

    void RemoveAddedItems()
    {
        addedItem1 = null;
        addedItem2 = null;
    }

    private void Update()
    {
        // Craft when two items are added
        if (addedItem1 && addedItem2)
        {
            craftButton.SetActive(true);
        }
        else
        {
            craftButton.SetActive(false);
        }

        if (addedItem1 && addedItem2)
        {
            anim.Play("CraftingPot");
        }
        else if (addedItem1 && !addedItem2)
        {
            item1Image.sprite = addedItem1.sprite;
            anim.Play("OneItem");
        }
        else if (!addedItem1 && addedItem2)
        {
            item2Image.sprite = addedItem2.sprite;
            anim.Play("OneItem");
        }
        
        if (addedItem1 == null)
        {
            item1Image.sprite = null;
        }
        if (addedItem2 == null)
        {
            item2Image.sprite = null;
            //item2Image.sprite = addedItem2.sprite;          
        }
        
        if (!addedItem1 && !addedItem2)
        {
            anim.Play("CraftEmpty");
        }

    }

    void DisplayAddedItems()
    {
        
    }

    void CookItems()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            craftPanel.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            craftPanel.SetActive(true);
            PlayerController player = FindObjectOfType<PlayerController>();
            if (Input.GetKeyDown(KeyCode.E) && player.carriedItem && player.carriedItem.GetComponent<Recipe>() == null)
            {
                if (addedItem1 == null)
                {
                    addedItem1 = player.carriedItem.GetComponent<Item>();
                    item1Image.sprite = addedItem1.sprite;
                    player.RemoveCarriedItem();
                }
                else if (addedItem2 == null)
                {
                    addedItem2 = player.carriedItem.GetComponent<Item>();
                    item2Image.sprite = addedItem2.sprite;
                    player.RemoveCarriedItem();
                }

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            craftPanel.SetActive(false);
        }
    }

    public void RemoveItem1()
    {
        addedItem1 = null;
    }

    public void RemoveItem2()
    {
        addedItem2 = null;
    }
}
