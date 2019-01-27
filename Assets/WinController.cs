using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{

    public int score;
    public GameObject yipee;
    public GameObject hooray;
    public float timerOne;
    public float timerTwo;
    private float sameOne;
    private float sameTwo;

    // Start is called before the first frame update
    void Start()
    {
        score =  PlayerPrefs.GetInt("Score");
        sameOne = timerOne;
        sameTwo = timerTwo;
        if (score > 3)
        {
            yipee.SetActive(true);
            hooray.SetActive(true);
        }
        if (score > 9)
        {
            sameOne--;
        }
        if (score > 10)
        {
            sameTwo--;
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
