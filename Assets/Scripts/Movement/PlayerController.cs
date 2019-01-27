using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float inputX;
    private float inputY;
    private bool isCarryingItem;
    public bool isInDialog;
    public GameObject dialogPanel;
    public Child child;
    private Rigidbody2D rigidbody;
    public float speedX;
    public float speedY;
    public GameObject carriedItem;
    public GameObject carriedItemCanvas;
    public Transform minBound, maxBound;
    public UITimer timer;
    Animator anim;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isInDialog)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
            rigidbody.velocity = new Vector3(speedX * inputX, speedY * inputY, 0f);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                timer.factor = 1f;
                SetDialog(false, null);
                Time.timeScale = 1f;
            }
        }
        
        // Bound player movement
        //BoundMovement();

        if (carriedItem == null)
        {
            carriedItemCanvas.SetActive(false);
        }
        else
        {
            carriedItemCanvas.SetActive(true);
        }

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

    public void PickUpItem(GameObject item)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Chicken"))
        {
        anim.Play("Peck");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("ChickenLeft"))
        {
        anim.Play("Peck2");
        }
        carriedItem = item;
        carriedItemCanvas.SetActive(true);
        carriedItemCanvas.GetComponentInChildren<Image>().sprite = item.GetComponent<Item>().sprite;
    }

    public void RemoveCarriedItem()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Chicken"))
        {
            anim.Play("Peck");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("ChickenLeft"))
        {
            anim.Play("Peck2");
        }

        carriedItem = null;
        carriedItemCanvas.SetActive(false);
        carriedItemCanvas.GetComponentInChildren<Image>().sprite = null;
    }

    public void SetDialog(bool toggle, GameObject panel)
    {
        isInDialog = toggle;
        if(toggle)
        {
            rigidbody.velocity = new Vector3(0f, 0f, 0f);
            dialogPanel = panel;
        }
        else
        {
            dialogPanel.SetActive(false);
            dialogPanel = null;
        }
    }
}
