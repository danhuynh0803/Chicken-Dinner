using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealChicken : MonoBehaviour
{
    public float timer;
    private float timerTwo;

    // Start is called before the first frame update
    void Start()
    {
        timerTwo = timer;
        
    }

    // Update is called once per frame
    void Update()
    {
        timerTwo -= Time.deltaTime;
        if (timerTwo < 0)
        {
            float rate = UnityEngine.Random.Range(0.0f, 100.0f);
            if (rate > 49)
            {
                float rateTwo = UnityEngine.Random.Range(0.0f, 100.0f);
                if (rateTwo > 49)
                {
                SoundController.Play((int)SFX.RealOne);
                }
                if (rateTwo < 49)
                {
                SoundController.Play((int)SFX.RealTwo);
                } 
            }

            timerTwo = Random.Range(timer, timer + 5.0f);
        }
    }
}
