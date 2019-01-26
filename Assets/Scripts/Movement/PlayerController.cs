using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float inputX;
    private float inputY;
    private bool isCarryingItem;
    private Rigidbody2D rigidbody;
    public float speedX;
    public float speedY;
    public Item carriedItem;
    public GameObject carriedItemCanvas;
    public Transform minBound, maxBound;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        rigidbody.velocity = new Vector3(speedX * inputX, speedY * inputY, 0f);

        // Bound player movement
        //BoundMovement();

    }

    private void BoundMovement()
    {
        if (transform.position.x <= minBound.position.x)
        {
            transform.position = new Vector3(minBound.position.x, transform.position.y, transform.position.y);
        }
        else if (transform.position.x >= maxBound.position.x)
        {
            transform.position = new Vector3(maxBound.position.x, transform.position.y, transform.position.y);
        }

        if (transform.position.y <= minBound.position.y)
        {
            transform.position = new Vector3(transform.position.x, minBound.position.y, minBound.position.y);
        }
        else if (transform.position.y >= maxBound.position.y)
        {
            transform.position = new Vector3(transform.position.x, maxBound.position.y, maxBound.position.y);
        }
    }

    public void PickUpItem(Item item)
    {
        if(!isCarryingItem)
        {
            carriedItem = item;
            carriedItemCanvas.GetComponentInChildren<Image>().sprite = item.sprite;
        }
    }

    public void RemoveCarriedItem()
    {
        carriedItem = null;
    }
}
