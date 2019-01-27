using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    Text whiteText, blackText;
    public static float timer;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        whiteText = GetComponent<Text>();
    }

    public static void SetDisplayTimer(float newTime)
    {
        timer = newTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            whiteText.text = Mathf.Ceil(timer).ToString();
        }

        if (timer <= 0)
        {
            GameController gameController = FindObjectOfType<GameController>();
            // When timer is 0, dont display it anymore
            score = gameController.score;
            PlayerPrefs.SetInt("Score", score);
            whiteText.text = "";
            gameController.LoadWinScreen();
        }
    }
}
