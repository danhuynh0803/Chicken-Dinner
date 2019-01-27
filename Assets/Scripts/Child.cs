using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Child : MonoBehaviour
{
    //public GameController gameController;
    public string name;
    public float itemDecayTimer;
    private float decayTimer;
    public float startItemTimer; 
    private float newItemTimer = 5.0f;
    private float privNewItemTimer;
    [SerializeField]
    private Item desiredItem;
    public float radius;
    public GameObject itemCanvas;
    public float canvasStayTimer = 1.5f;
    public GameObject dialogPanel;
    public int score;
    private bool playerInRange;
    private float canvasTimer;
    public GameObject player;
    private int strikes;
    private bool isHatingYou;
    public bool isEmo;
    public bool isNerdy;
    public bool isGirl;
    public bool isBaby;
    public GameObject loveParticle;
    public Image timerImage;
    public TextMeshProUGUI dialogText;
    public GameObject babyImage1, babyImage2;
    public UITimer timer;
    public float factor;
    public int GetStrikeCount()
    {
        return strikes;
    }

    private void Start()
    {
        factor = 1f;
        decayTimer = itemDecayTimer;

        privNewItemTimer = startItemTimer;
        ///WantNewItem();
        player = FindObjectOfType<PlayerController>().gameObject;
        //itemCanvas.GetComponentInChildren<Image>().sprite = desiredItem.sprite;
        
    }

    void WantNewItem()
    {
        // Child is struck out and no longer wants food
        if (strikes >= 3)
        {
            return;
        }

        if(isEmo)
        {
         SoundController.Play((int)SFX.EmoHungry);
        }
        if(isNerdy)
        {
         SoundController.Play((int)SFX.NerdyHungry);
        }
        if(isGirl)
        {
         SoundController.Play((int)SFX.GirlHungry);
        }
        if(isBaby)
        {
         SoundController.Play((int)SFX.BabyCheep);
        }  

        GameObject newItem =
            GameController.instance.items[Random.Range(0, GameController.instance.items.Count - 1)];

        desiredItem = newItem.GetComponent<Item>();

        //Debug.Log("Get new item " + desiredItem);
        //itemCanvas.GetComponentInChildren<Image>().sprite = desiredItem.sprite;

        decayTimer = itemDecayTimer;
        privNewItemTimer = Random.Range(newItemTimer, 10.0f);
    }

    void Update()
    {
        // Remove canvas when child is not desiring an item
        if (desiredItem != null)
        {
            itemCanvas.SetActive(true);
            
            //Debug.Log("have item " + desiredItem.itemName);
            decayTimer -= Time.deltaTime * factor;
            if (decayTimer <= 0)
            {
                desiredItem = null;
                privNewItemTimer = newItemTimer;
                strikes++;
            }
        }
        else
        {
            itemCanvas.SetActive(false);
            privNewItemTimer -= Time.deltaTime;
        }
        
        if (privNewItemTimer <= 0)
        {
            WantNewItem();
        }    

        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        if (Vector2.Distance(transform.position, player.transform.position) <= radius)
        {
            playerInRange = true;
            canvasTimer = canvasStayTimer;
        }
        else
        {
            playerInRange = false;
        }

        if (playerInRange && !isHatingYou)
        {
            //itemCanvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space) && dialogPanel.activeSelf == false)
            {
                dialogPanel.SetActive(true);
                UpdateDialogCanvas();
            }

            if (Input.GetKeyDown(KeyCode.E) && dialogPanel.activeSelf == false)
            {
                if (player.GetComponent<PlayerController>().carriedItem != null)
                {
                    RecieveItem(player.GetComponent<PlayerController>().carriedItem.GetComponent<Item>());
                }
            }

        }
        else
        {
            canvasTimer -= Time.deltaTime;
        }

    }

    private void LateUpdate()
    {
        if (desiredItem)
        {
            timerImage.fillAmount = decayTimer / itemDecayTimer;

            if (timerImage.fillAmount < 0.3f)
            {
                timerImage.color = Color.red;
            }
            else if (timerImage.fillAmount < 0.65f)
            {
                timerImage.color = Color.yellow;
            }
            else
            {
                timerImage.color = Color.green;
            }
        }
    }

    private void UpdateDialogCanvas()
    {
        timer.factor = 0f;
        Time.timeScale = 0f;
        player.GetComponent<PlayerController>().SetDialog(true, dialogPanel);
        if (isEmo)
        {
            if (desiredItem)
            {
                dialogText.text =
                   DialogController.instance.GetItemDialogForChickType(Chicks.Emo,
                   GameController.instance.GetIndexOfItem(desiredItem));

                SoundController.Play((int)SFX.MaleHello);
            }
            else if (strikes >= 3)
            {
                dialogText.text = ".............";
            }
            else
            {
                dialogText.text = "Leave me alone, I just ate!";
            }
        }
        else if (isNerdy)
        {
            if (desiredItem)
            {
                dialogText.text =
                    DialogController.instance.GetItemDialogForChickType(Chicks.Nerd,
                    GameController.instance.GetIndexOfItem(desiredItem));

                SoundController.Play((int)SFX.MaleHello);
            }
            else if (strikes >= 3)
            {
                dialogText.text = "I’m busy, could you leave me alone?";
            }
            else
            {
                dialogText.text = "I have received enough nutrition for now.";
            }

        }
        else if (isGirl)
        {
            if (desiredItem)
            {
                dialogText.text =
                    DialogController.instance.GetItemDialogForChickType(Chicks.Girl,
                    GameController.instance.GetIndexOfItem(desiredItem));

                SoundController.Play((int)SFX.FemaleHello);
            }
            else if (strikes >= 3)
            {
                dialogText.text = "Did you need something? You keep bothering me...";
            }
            else
            {
                dialogText.text = "Hey I can't eat too much, gotta watch my calories!";
            }       

        }
        else if (isBaby)
        {
            SoundController.Play((int)SFX.BabyCheep);
            if (desiredItem)
            {
                babyImage1.SetActive(true);
                babyImage2.SetActive(true);
                if (desiredItem.GetComponent<Recipe>().item1.itemName.ToLower().Equals("veggies"))
                {
                    babyImage1.GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, babyImage1.GetComponent<RectTransform>().localScale.z);
                }
                if (desiredItem.GetComponent<Recipe>().item1.itemName.ToLower().Equals("rice"))
                {
                    babyImage1.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, babyImage1.GetComponent<RectTransform>().localScale.z);
                }
                if (desiredItem.GetComponent<Recipe>().item2.itemName.ToLower().Equals("veggies"))
                {
                    babyImage2.GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f , babyImage1.GetComponent<RectTransform>().localScale.z);
                }
                if (desiredItem.GetComponent<Recipe>().item2.itemName.ToLower().Equals("rice"))
                {
                    babyImage2.GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, babyImage1.GetComponent<RectTransform>().localScale.z);
                }
                babyImage1.GetComponent<Image>().sprite = desiredItem.GetComponent<Recipe>().item1.sprite;
                babyImage2.GetComponent<Image>().sprite = desiredItem.GetComponent<Recipe>().item2.sprite;     
            }
            else if (strikes >= 3)
            {
                dialogText.text = "*Angry chirp noises*";
            }
            else
            {
                babyImage1.SetActive(false);
                babyImage2.SetActive(false);
            }
        }     
    }

    private void RecieveItem(Item item)
    {
        if(desiredItem != null)
        {
            if (desiredItem.itemName.ToLower().Equals(item.itemName.ToLower()))
            {
                player.GetComponent<PlayerController>().RemoveCarriedItem();
                IncreamentScore();
                desiredItem = null;
                itemCanvas.SetActive(false);
                privNewItemTimer = newItemTimer;
                GameObject particle =Instantiate(loveParticle, transform);
                particle.SetActive(true);
                Destroy(particle, 3f);
            }
            else
            {
                //strikes++;
                player.GetComponent<PlayerController>().RemoveCarriedItem();
                privNewItemTimer = newItemTimer;
                if (strikes <= 2)
                {
                SoundController.Play((int)SFX.BadChoice);    
                }
                if (strikes >= 3)
                {
                    SoundController.Play((int)SFX.ThreeStrikes);
                    isHatingYou = true;
                }
            }
        }
        //Debug.Log("desiredItem " + desiredItem.itemName);
        //Debug.Log("carriedItem " + item.itemName);
       
    }

    private void IncreamentScore()
    {
       float rate = UnityEngine.Random.Range(0.0f, 100.0f);
       if (rate > 19.0f)
       {
        SoundController.Play((int)SFX.GoodChoice);
        }
        else
        {
        if (isGirl)
        {
        SoundController.Play((int)SFX.FemaleYay);
        }
        if (isBaby)
        {
        SoundController.Play((int)SFX.BabyCheep);
        }
        else
        {
        SoundController.Play((int)SFX.GoodChoice);
        }
        }
        GameController.instance.IncreamentScore();
    }
}
