using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public GameController gameController;
    public string name;
    public Item desiredItem;
    public float radius;
    public GameObject itemCanvas;
    public float canvasStayTimer = 1.5f;
    public GameObject dialogPanel;
    public int score;
    private bool playerInRange;
    private float canvasTimer;
    private GameObject player;
    

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= radius)
        {
            playerInRange = true;
            canvasTimer = canvasStayTimer;
        }
        else
        {
            playerInRange = false;
        }

        if (playerInRange)
        {
            itemCanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && dialogPanel.activeSelf == false)
            {
                dialogPanel.SetActive(true);
                UpdateDialogCanvas();
            }
            if (Input.GetKeyDown(KeyCode.Space) && dialogPanel.activeSelf == false)
            {
                if (player.GetComponent<PlayerController>().carriedItem != null)
                    RecieveItem(player.GetComponent<PlayerController>().carriedItem);
            }
        }
        else
        {
            canvasTimer -= Time.deltaTime;
        }

        if (canvasTimer <= 0)
        {
            itemCanvas.SetActive(false);
        }
    }

    private void UpdateDialogCanvas()
    {
        
    }

    private void RecieveItem(Item item)
    {
        if (desiredItem.name.Equals(item.name))
        {
            player.GetComponent<PlayerController>().RemoveCarriedItem();
            IncreamentScore();
        }
    }

    private void IncreamentScore()
    {
        gameController.GetComponent<GameController>().IncreamentScore(score);
    }
}
