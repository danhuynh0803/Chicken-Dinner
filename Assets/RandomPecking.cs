using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPecking : MonoBehaviour
{
    Animator anim;
    public float timer;
    private float timerTwo;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
                anim.Play("Peck");
                }
                if (rate < 49)
                {
                anim.Play("Peck2");
                } 
            }
            timerTwo = Random.Range(timer, timer + 1.0f);
        }
    }
