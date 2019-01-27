using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSprite : MonoBehaviour
{
    public Sprite crossImage;
    public Image[] strikeImages = new Image[3];

    Child attachedChild;

    private void Start()
    {
        attachedChild = GetComponentInParent<Child>();

        for (int i = 1; i <= attachedChild.GetStrikeCount(); ++i)
        {
            strikeImages[i].sprite = null;
        }
    }

    private void Update()
    {
        for (int i = 0; i < attachedChild.GetStrikeCount(); ++i)
        {
            strikeImages[i].sprite = crossImage;
        }
    }
}
