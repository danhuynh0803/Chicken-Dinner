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
    public bool isMainMenu;
    public List<Item> items = new List<Item>();

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


    // Give each child a random ingredient from the list
    private void RandomizeChildrenItems()
    {
        Debug.Log("Randomize children items");
        // Get all child objects and randomize their items
        Child[] children = FindObjectsOfType<Child>();

        foreach (Child child in children)
        {
            child.desiredItem = items[Random.Range(0, items.Count-1)];
        }
    }

    private void Start()
    {
        UITimer.SetDisplayTimer(levelTime);
        RandomizeChildrenItems();
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
        
    }

    public void GameOver()
    {
        // Open GameOver panel and display score
        MenuEvents.instance.OpenWinPanel();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void TogglePausePanel()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }

    public void IncreamentScore(int score)
    {
        this.score += score;
        scoreText.text = "Score: " + score;
    }
}
