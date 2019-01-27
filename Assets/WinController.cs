using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinController : MonoBehaviour
{

    public int score;
    public GameObject yipee;
    public GameObject hooray;
    public float timerOne;
    public float timerTwo;
    private float sameOne;
    private float sameTwo;
    public Text scoreText;
    public GameObject veggieTwo;
    public GameObject wormTwo;

    // Start is called before the first frame update
    void Start()
    {
        score =  PlayerPrefs.GetInt("Score");
        scoreText.text = "" + score;
        sameOne = timerOne;
        sameTwo = timerTwo;
        if (score > 3)
        {
            yipee.SetActive(true);
            hooray.SetActive(true);
        }
        if (score > 7)
        {
            sameOne--;
        }
        if (score > 8)
        {
            sameTwo--;
        }
        if (score > 9)
        {
            veggieTwo.SetActive(true);
        }
        if (score > 10)
        {
            wormTwo.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (score > 4)
        {
            timerOne -= Time.deltaTime;
            if (timerOne < 0)
            {
            yipee.SetActive(false);
            yipee.SetActive(true);
            timerOne = sameOne;
            }
        }
                if (score > 5)
        {
            timerTwo -= Time.deltaTime;
            if (timerTwo < 0)
            {
            hooray.SetActive(false);
            hooray.SetActive(true);
            timerTwo = sameTwo;
            }
        }
    }
}
