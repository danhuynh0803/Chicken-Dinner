using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenAnimation : MonoBehaviour
{
    Animator anim;
    private Vector3 original;
    public Vector3 flip;
    public Image food;
    public RectTransform fliped;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        original = food.GetComponent<RectTransform>().localPosition;
        flip = fliped.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKey(KeyCode.LeftArrow))
        {
            food.GetComponent<RectTransform>().localPosition = original; 
            anim.Play("Chicken");
        }
        if (Input.GetKey(KeyCode.A))
        {
            food.GetComponent<RectTransform>().localPosition = original;
            anim.Play("Chicken");
        }
        if (Input.GetKey(KeyCode.D))
        {
            food.GetComponent<RectTransform>().localPosition = flip;
            anim.Play("ChickenLeft"); //is actually chicken right
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            food.GetComponent<RectTransform>().localPosition = flip;
            anim.Play("ChickenLeft");
        }
    }
    void OnMouseOver () 
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Chicken"))
        {
        anim.Play("Peck");
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("ChickenLeft"))
        {
        anim.Play("Peck2");
        }   
        }
    }
}
