using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimation : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
    anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.Play("Chicken");
        }
        if (Input.GetKey(KeyCode.A))
        {
        anim.Play("Chicken");
        }
        if (Input.GetKey(KeyCode.D))
        {
        anim.Play("ChickenLeft");
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.Play("ChickenLeft");
        }
    }
}
