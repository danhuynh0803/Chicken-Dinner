using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject pauseMenu;
    public int score;
    public Text scoreText;
    public Text scoreText2;
    public bool isMainMenu;
    public List<GameObject> items = new List<GameObject>();

    public float levelTime;

    private void Awake()
    {
         if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
        //DontDestroyOnLoad(gameObject);
    }

    public int GetIndexOfItem(Item item)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            if (item.itemName.ToLower().Equals(items[i].GetComponent<Item>().itemName.ToLower()))
            {
                return i;
            }
        }
        return -1;
    }

    // Give each child a random ingredient from the list
    private void RandomizeChildrenItems()
    {
        // Get all child objects and randomize their items
        Child[] children = FindObjectsOfType<Child>();

        /* 
        foreach (Child child in children)
        {
            child.desiredItem = items[Random.Range(0, items.Count-1)];
        }
        */
    }

    private void Start()
    {
        UITimer.SetDisplayTimer(levelTime);
        //RandomizeChildrenItems();
    }

    private void Update()
    {
        if(!isMainMenu)
        {
            TogglePausePanel();
        }

        levelTime -= Time.deltaTime;
        if (levelTime <= 0) 
        {
            GameOver();   
        }

        scoreText.text = "Score: " + this.score;
        scoreText2.text = "Score: " + this.score;
    }

    public void GameOver()
    {
        // Open GameOver panel and display score
        MenuEvents.instance.OpenWinPanel();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene("WinScene");
    }

    private void TogglePausePanel()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }

    public void IncreamentScore()
    {
        //this.score += score;
        this.score++;

    }
}
