using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    Text timerText;
    public static float timer;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
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
            timerText.text = Mathf.Ceil(timer).ToString();
        }

        if (timer <= 0)
        {
            // When timer is 0, dont display it anymore
            timerText.text = "";
        }
    }
}
