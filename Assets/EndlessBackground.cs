using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessBackground : MonoBehaviour
{
    public GameObject Endless;
    public float timer;
    public float timerTwo;

    // Start is called before the first frame update
    void Start()
    {
     Instantiate(Endless);
     timerTwo = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
        Instantiate(Endless);
        timer = timerTwo;
        }
    }
}
